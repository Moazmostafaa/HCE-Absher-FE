import { DatePipe } from "@angular/common";
import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import { CategoryModel } from "../../../models/category/category.model";
import { EventSearchModel } from "../../../models/event/event-search.model";
import {
  EventModel,
  EventPrivacyTypeEnum,
  EventStatusEnum,
  EventTypeEnum,
} from "../../../models/event/event.model";
import { CategoryService } from "../../../services/category.service";
import { EventService } from "../../../services/event.service";
import { ToastrService } from "../../../services/toastr.service";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { EntityNames } from "../../../shared/Entity-Names";

@Component({
  selector: "ngx-list-events",
  templateUrl: "./list-events.component.html",
  styleUrls: ["./list-events.component.scss"],
})
export class ListEventsComponent implements OnInit {
  dir = "ltr";
  constructor(
    private eventService: EventService,
    private categoryService: CategoryService,
    private dialogService: NbDialogService,
    private toastrService: ToastrService,
    private translate: TranslateService,
    public datePipe: DatePipe
  ) {
    if (translate.currentLang == "ar") this.dir = "rtl";
  }
  active = true;
  events: EventModel[] = [];
  categories: CategoryModel[] = [];
  statuses = ["Not started", "Started", "Finished"];
  totalRecords: number = 0;
  searchModel: EventSearchModel;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "name",
    "startDate",
    "endDate",
    "registrationStartDate",
    "registrationEndDate",
    "eventType",
    "eventStatus",
    "eventPrivacy",
    "actions",
  ];
  @ViewChild(MatPaginator) paginator: MatPaginator;

  todaysDate: string;
  ngOnInit() {
    this.todaysDate = this.datePipe.transform(new Date(), "yyyy-MM-dd");
    this.searchModel = {
      PageNumber: 1,
      PageSize: environment.DEFAULT_PAGE_SIZE,
      searchBy: {
        categId: null,
        eventStatus: null,
      },
    };
    this.search();
    this.categoryService
      .getCategoriesForEvent({
        PageNumber: 1,
        PageSize: 1000,
      })
      .subscribe((res) => {
        this.categories = res.entity.entities;
      });
  }
  onSearchChange() {
    this.searchModel.PageNumber = 1;
    this.search();
  }
  search(page?: PageEvent) {
    if (page) {
      this.searchModel.PageNumber = page.pageIndex + 1;
      this.searchModel.PageSize = page.pageSize;
    }
    if (this.searchModel.searchBy?.fromDate) {
      this.searchModel.searchBy.fromDate = this.datePipe.transform(
        new Date(this.searchModel.searchBy.fromDate),
        "yyyy-MM-dd"
      );
    }
    if (this.searchModel.searchBy?.toDate) {
      this.searchModel.searchBy.toDate = this.datePipe.transform(
        new Date(this.searchModel.searchBy.toDate),
        "yyyy-MM-dd"
      );
    }
    this.eventService.searchEvents(this.searchModel).subscribe((res) => {
      this.events = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    let title = await this.translate
      .get("DeleteTitle", { entity: name })
      .toPromise();
    let body = await this.translate
      .get("DeleteMessage", { entity: name })
      .toPromise();
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `${title}`,
          body: `${body}?`,
        },
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.eventService.deleteEvent(id).subscribe((result) => {
            this.events = this.events.filter((x) => x.eventId != id);
            this.toastrService.Delete(EntityNames.Event);
          });
        }
      });
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
  fromDateChanged(date) {
    this.searchModel.searchBy.fromDate = date;
    this.search();
  }
  toDateChanged(date) {
    this.searchModel.searchBy.toDate = date;
    this.search();
  }
}
