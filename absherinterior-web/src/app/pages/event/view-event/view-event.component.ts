import { Component, OnInit, TemplateRef } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import {
  EventModel,
  EventPrivacyTypeEnum,
  EventStatusEnum,
  EventTypeEnum,
} from "../../../models/event/event.model";
import { EventService } from "../../../services/event.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { NbDialogRef, NbDialogService } from "@nebular/theme";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { DatePipe } from "@angular/common";
import { BasicFileModel } from "../../../models/attachment/basic.file.model";
import { LookupsService } from "../../../services/lookups.service";
import { dateLessThanToday } from "../../../shared/validators/dates.validator";

@Component({
  selector: "ngx-view-event",
  templateUrl: "./view-event.component.html",
  styleUrls: ["./view-event.component.scss"],
})
export class ViewEventComponent implements OnInit {
  dir = "ltr";
  constructor(
    private eventService: EventService,
    private route: ActivatedRoute,
    private router: Router,
    private translate: TranslateService,
    private toastrService: ToastrService,
    private dialogService: NbDialogService,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private lookupSerivce: LookupsService,
  ) {
    if (translate.currentLang == "ar") this.dir = "rtl";
  }
  eventId: string;
  event: EventModel;
  categoryName = "";
  eventForm: FormGroup;
  files: BasicFileModel[] = [];
  ngOnInit() {
    this.initPostponeForm();
    this.event = this.initEvent();
    this.eventId = this.route.snapshot.paramMap.get("id");
    this.getCurrentEvent(this.eventId);
  }
  getCurrentEvent(eventId: string) {
    this.eventService.getEventById(eventId).subscribe((res) => {
      this.event = res.entity;
      this.eventService
        .getEventCategoryById(this.event.eventCategoryId)
        .subscribe((categoryResponse) => {
          this.categoryName = categoryResponse.entity.name;
        });
      this.getEventFiles();
    });
  }
  getEventFiles() {
    if (this.event.eventFiles) {
      for (let attach = 0; attach < this.event.eventFiles.length; attach++) {
        this.lookupSerivce
          .getAttachmentById(this.event.eventFiles[attach].attachmentId)
          .subscribe((res) => {
            let file: BasicFileModel = {
              id: res.entity.attachmentId,
              fileData: res.entity.fileData,
              extension: res.entity.extention,
            };
            this.files.push(file);
          });
      }
    }
  }
  initPostponeForm() {
    this.eventForm = this.formBuilder.group({
      eventId: [""],
      regStartDate: [null, [dateLessThanToday]],
      regEndDate: [null, [dateLessThanToday]],
      startDate: ["", [Validators.required, dateLessThanToday]],
      endDate: ["", [Validators.required, dateLessThanToday]],
    });
  }
  get fc() {
    return this.eventForm.controls;
  }
  initEvent(): EventModel {
    return {
      eventId: null,
      eventName: null,
      desc: null,
      startDate: null,
      endDate: null,
      creationDate: null,
      eventAgenda: null,
      eventCategoryId: null,
      eventDuration: null,
      eventLocation: null,
      eventFiles: null,
      eventType: null,
      hasLimtedSeats: null,
      isEventOwner: false,
      numberOfInvitedUsers: null,
      numberOfRegisteredUsers: null,
      numberOfSeats: null,
      privacyType: null,
      status: null,
      userInfo: null,
      regEndDate: null,
      regStartDate: null,
      remainingDaysForRegistration: null,
    };
  }
  getEventType(type: number) {
    return EventTypeEnum[type];
  }
  getEventStatus(status: number) {
    return EventStatusEnum[status];
  }
  getEventPrivacy(privacy: number) {
    return EventPrivacyTypeEnum[privacy];
  }
  postpone(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog, {
      closeOnEsc: false,
      closeOnBackdropClick: false,
    });
  }
  submitPostpone(dialog: TemplateRef<any>) {
    if (this.eventForm.invalid) {
      return;
    }
    let model = {
      eventId: this.event.eventId,
      startDate: this.datePipe.transform(
        new Date(this.eventForm.value.startDate),
        "yyyy-MM-dd"
      ),
      endDate: this.datePipe.transform(
        new Date(this.eventForm.value.endDate),
        "yyyy-MM-dd"
      ),
      regStartDate:
        this.eventForm.value.regStartDate == null
          ? null
          : this.datePipe.transform(new Date(this.eventForm.value.regStartDate), "yyyy-MM-dd"),
      regEndDate:
        this.eventForm.value.regEndDate == null
          ? null
          : this.datePipe.transform(new Date(this.eventForm.value.regEndDate), "yyyy-MM-dd"),
    };
    this.eventService.postponeEvent(model).subscribe((result) => {
      this.getCurrentEvent(this.event.eventId);
      this.toastrService.Postpone(EntityNames.Event);
      this.eventForm.reset();
    });
  }
  async cancel() {
    let title = await this.translate
      .get("CancelTitle", { entity: this.event.eventName })
      .toPromise();
    let body = await this.translate
      .get("CancelMessage", { entity: this.event.eventName })
      .toPromise();
    let buttonText = await this.translate.get("Cancel").toPromise();
    this.dialogService
      .open(ConfirmDialogComponent, {
        closeOnBackdropClick: false,
        context: {
          title: `${title}`,
          body: `${body}?`,
          buttonText: `${buttonText}`,
        },
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.eventService
            .cancelEvent(this.event.eventId)
            .subscribe((result) => {
              this.router.navigate(["/pages/event"]);
              this.toastrService.Cancel(EntityNames.Event);
            });
        }
      });
  }
}
