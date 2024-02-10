import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { ActivatedRoute } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import {
  ReplyStatus,
  SearchWithEventIdModel,
} from "../../../models/event/search-with-event.model";
import { BasicUserModel } from "../../../models/User/basic-user.model";
import { EventService } from "../../../services/event.service";
import { ToastrService } from "../../../services/toastr.service";
@Component({
  selector: 'ngx-rejected-requests',
  templateUrl: './rejected-requests.component.html',
  styleUrls: ['./rejected-requests.component.scss']
})
export class RejectedRequestsComponent implements OnInit {

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
  users: BasicUserModel[] = [];
  totalRecords: number = 0;
  pageSizeOptions: number[] = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = ["number", "name"];
  @ViewChild(MatPaginator) paginator: MatPaginator;

  private get replyStatusEnum(): typeof ReplyStatus {
    return ReplyStatus;
  }
  ngOnInit() {
    this.searchModel = {
      PageNumber: 1,
      PageSize: environment.DEFAULT_PAGE_SIZE,
      eventId: this.route.snapshot.paramMap.get("id"),
      status: this.replyStatusEnum.Rejected,
    };
    this.search();
  }
  search(page?: PageEvent) {
    if (page) {
      this.searchModel.PageNumber = page.pageIndex + 1;
      this.searchModel.PageSize = page.pageSize;
      this.searchModel.status = this.replyStatusEnum.Rejected;
    }
    // accepted in function name but it's geting rejected "same end point"
    this.eventService
      .getAcceptedRequestToAttend(this.searchModel)
      .subscribe((res) => {
        this.users = res.entity.entities;
        this.totalRecords = res.entity.totalRecords;
      });
  }
}
