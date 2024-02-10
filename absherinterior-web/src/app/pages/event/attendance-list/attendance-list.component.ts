import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { ActivatedRoute } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import { AttendanceListModel } from "../../../models/event/attendance-list.model";
import { SearchWithEventIdModel } from "../../../models/event/search-with-event.model";
import { EventService } from "../../../services/event.service";
import { ToastrService } from "../../../services/toastr.service";

@Component({
  selector: "ngx-attendance-list",
  templateUrl: "./attendance-list.component.html",
  styleUrls: ["./attendance-list.component.scss"],
})
export class AttendanceListComponent implements OnInit {
  dir = "ltr";
  constructor(
    private eventService: EventService,
    private route: ActivatedRoute,
    private translate: TranslateService,
    private toastrService: ToastrService
  ) {
    if (translate.currentLang == "ar") this.dir = "rtl";
  }
  searchModel: SearchWithEventIdModel;
  attendaceList: AttendanceListModel[] = [];
  totalRecords: number = 0;
  pageSizeOptions: number[] = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "name",
    "confirmationDate"
  ];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  ngOnInit() {
    this.searchModel = {
      PageNumber: 1,
      PageSize: environment.DEFAULT_PAGE_SIZE,
      eventId: this.route.snapshot.paramMap.get("id"),
      status: 0
    };
    this.search();
  }
  search(page?: PageEvent) {
    if (page) {
      this.searchModel.PageNumber = page.pageIndex + 1;
      this.searchModel.PageSize = page.pageSize;
    }
    this.eventService.getAttendanceList(this.searchModel).subscribe((res) => {
      this.attendaceList = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
}
