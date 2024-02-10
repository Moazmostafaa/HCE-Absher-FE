import { DatePipe } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormGroup,
  Validators,
  FormArray,
  FormControl,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { BasicFileModel } from "../../../models/attachment/basic.file.model";
import { CategoryModel } from "../../../models/category/category.model";
import { AddEventModel } from "../../../models/event/add-event.model";
import { EditEventModel } from "../../../models/event/edit-event.model";
import {
  EventTypeEnum,
  EventPrivacyTypeEnum,
} from "../../../models/event/event.model";
import { FileModuleEnum } from "../../../models/file-module";
import { CategoryService } from "../../../services/category.service";
import { EventService } from "../../../services/event.service";
import { FileService } from "../../../services/file.service";
import { LookupsService } from "../../../services/lookups.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { WhiteSpaceValidator } from "../../../shared/validators/white-space.validator";
import {
  dateLessThanToday,
  DateValidators,
} from "../../../shared/validators/dates.validator";

@Component({
  selector: "ngx-edit-event",
  templateUrl: "./edit-event.component.html",
  styleUrls: ["./edit-event.component.scss"],
})
export class EditEventComponent implements OnInit {
  constructor(
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private eventService: EventService,
    private categoryService: CategoryService,
    private lookupSerivce: LookupsService,
    private router: Router,
    private toaster: ToastrService,
    private datePipe: DatePipe,
    private route: ActivatedRoute,
    private translate: TranslateService
  ) {}
  eventForm: FormGroup;
  files: BasicFileModel[] = [];
  isLoading = false;
  types = ["Physical", "Online"];
  privacies = ["Public", "Private"];
  seatDisabled = true;
  eventId: string;
  categories: CategoryModel[] = [];
  isSubmitted: boolean = false;

  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.eventId = params["id"];
    });
    this.initForm();
    this.categoryService
      .getCategoriesForEvent({
        PageNumber: 1,
        PageSize: 1000,
      })
      .subscribe((res) => {
        this.categories = res.entity.entities;
      });
    this.eventService.getEventById(this.eventId).subscribe((res) => {
      let event = res.entity;
      this.eventForm.patchValue({
        eventName: event.eventName,
        desc: event.desc,
        eventLocation: event.eventLocation,
        startDate: new Date(event.startDate),
        endDate: new Date(event.endDate),
        eventAgenda: event.eventAgenda,
        eventType: event.eventType,
        eventCategoryId: event.eventCategoryId,
        hasLimtedSeats: event.hasLimtedSeats,
        numberOfSeats: event.numberOfSeats,
        eventDuration: event.eventDuration,
        privacyType: event.privacyType,
        regStartDate: new Date(event.regStartDate),
        regEndDate: new Date(event.regEndDate),
        eventFiles: event.eventFiles.map((file) => file.attachmentId),
      });
      event.hasLimtedSeats
        ? this.enableSeats(event.numberOfSeats)
        : this.disableSeats();
      if (event.eventFiles) {
        let filesLoaded = 0;
        this.isLoading = true;
        for (let attach = 0; attach < event.eventFiles.length; attach++) {
          this.lookupSerivce
            .getAttachmentById(event.eventFiles[attach].attachmentId)
            .subscribe((res) => {
              let file: BasicFileModel = {
                id: res.entity.attachmentId,
                fileData: res.entity.fileData,
                extension: res.entity.extention,
              };
              this.files.push(file);
              this.eventFiles.push(new FormControl(res.entity.attachmentId));
              filesLoaded++;
              this.isLoading = false;
            }),
            () => (this.isLoading = false);
        }
      }
    });
  }
  initForm() {
    this.eventForm = this.formBuilder.group(
      {
        eventName: [
          "",
          [
            Validators.required,
            Validators.minLength(3),
            WhiteSpaceValidator.noWhiteSpace,
          ],
        ],
        desc: [
          "",
          [
            Validators.required,
            Validators.minLength(6),
            WhiteSpaceValidator.noWhiteSpace,
          ],
        ],
        eventLocation: [
          "",
          [
            Validators.required,
            Validators.minLength(3),
            WhiteSpaceValidator.noWhiteSpace,
          ],
        ],
        regStartDate: [null, [dateLessThanToday]],
        regEndDate: [null, [dateLessThanToday]],
        startDate: ["", [Validators.required, dateLessThanToday]],
        endDate: ["", [Validators.required, dateLessThanToday]],
        eventAgenda: ["", [Validators.required, Validators.minLength(6)]],
        eventType: [EventTypeEnum.Online, [Validators.required]],
        eventCategoryId: ["", Validators.required],
        hasLimtedSeats: [false],
        numberOfSeats: [null],
        eventDuration: [0, Validators.required],
        privacyType: [EventPrivacyTypeEnum.Public, Validators.required],
        eventFiles: this.formBuilder.array([]),
      },
      {
        validator: Validators.compose([
          DateValidators.dateLessThan("regStartDate", "regEndDate", {
            regStartDateLessThanRegEndDate: true,
          }),
          DateValidators.dateLessThan("startDate", "endDate", {
            startDateLessThanEndDate: true,
          }),
          DateValidators.dateLessThan("regStartDate", "startDate", {
            regStartDateLessThanStartDate: true,
          }),
          DateValidators.dateLessThan("regEndDate", "endDate", {
            regEndDateLessThanEndDate: true,
          }),
        ]),
      }
    );
  }
  onSubmit() {
    this.isSubmitted = true;
    if (this.eventForm.invalid) {
      return;
    }
    let event = this.eventForm.value;
    let model: EditEventModel = {
      eventId: this.eventId,
      eventName: event.eventName,
      desc: event.desc,
      eventLocation: event.eventLocation,
      startDate: this.datePipe.transform(
        new Date(event.startDate),
        "yyyy-MM-dd"
      ),
      endDate: this.datePipe.transform(new Date(event.endDate), "yyyy-MM-dd"),
      eventAgenda: event.eventAgenda,
      eventCategoryId: event.eventCategoryId,
      eventDuration: event.eventDuration,
      eventType: event.eventType,
      hasLimtedSeats: event.hasLimtedSeats,
      numberOfSeats: event.numberOfSeats,
      privacyType: event.privacyType,
      regStartDate:
        event.regStartDate == null
          ? null
          : this.datePipe.transform(new Date(event.regStartDate), "yyyy-MM-dd"),
      regEndDate:
        event.regEndDate == null
          ? null
          : this.datePipe.transform(new Date(event.regEndDate), "yyyy-MM-dd"),
      eventFiles: event.eventFiles,
    };
    this.eventService.editEvent(model).subscribe((res) => {
      if (res.status == 200) {
        this.toaster.Update(EntityNames.Event);
        this.router.navigate(["/pages/event"]);
      }
    });
  }
  deleteAttachment(attachmentId) {
    this.eventFiles.removeAt(
      this.eventFiles.value.findIndex((file) => file == attachmentId)
    );
    this.fileService.deleteFile(attachmentId).toPromise();
    this.files = this.files.filter((c) => c.id != attachmentId);
  }
  onSelectFile(event) {
    if (event.target.files && event.target.files.length > 0) {
      this.isLoading = true;
      let filesLoaded = 0;
      let files = event.target.files;
      for (let i = 0; i < event.target.files.length; i++) {
        var readerProfile = new FileReader();
        readerProfile.onload = (event: any) => {
          this.fileService
            .uploadVariousfile(files[i], (+FileModuleEnum.Post).toString())
            .subscribe(
              (res) => {
                this.files.push({
                  id: res.entity,
                  fileData: event.target.result,
                  extension: "." + files[i].name.split(".").pop(),
                  file: files[i],
                });
                this.eventFiles.push(new FormControl(res.entity));
                filesLoaded++;
                this.isLoading = filesLoaded == files.length ? false : true;
              },
              () => {
                this.translate
                  .get("errorUploadImageType")
                  .subscribe((message) => {
                    this.translate.get("error").subscribe((title) => {
                      this.toaster.danger(message, title);
                      filesLoaded++;
                      this.isLoading = false;
                    });
                  });
              }
            );
        };
        readerProfile.readAsDataURL(event.target.files[i]);
      }
    }
  }
  get eventFiles() {
    return this.eventForm.controls["eventFiles"] as FormArray;
  }
  get fc() {
    return this.eventForm.controls;
  }
  setCheckedStatus(checked) {
    if (checked.target.checked) {
      this.enableSeats(null);
    } else {
      this.disableSeats();
    }
  }
  enableSeats(value) {
    if (value) this.eventForm.get("numberOfSeats").setValue(value);
    this.eventForm.get("numberOfSeats").setValidators([Validators.required]);
    this.eventForm.get("numberOfSeats").updateValueAndValidity();
    this.eventForm.controls["numberOfSeats"].enable();
  }
  disableSeats() {
    this.eventForm.get("numberOfSeats").setValue(null);
    this.eventForm.get("numberOfSeats").clearValidators();
    this.eventForm.get("numberOfSeats").updateValueAndValidity();
    this.eventForm.controls["numberOfSeats"].disable();
  }
}
