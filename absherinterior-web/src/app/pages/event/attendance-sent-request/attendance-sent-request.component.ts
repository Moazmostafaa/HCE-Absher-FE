import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { ActivatedRoute } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import { EventModel } from "../../../models/event/event.model";
import { PendingRequestsModel } from "../../../models/event/pending-requests.model";
import { SearchWithEventIdModel } from "../../../models/event/search-with-event.model";
import { SentRequestsModel } from "../../../models/event/sent-requests.model";
import { BasicUserModel } from "../../../models/User/basic-user.model";
import { EventService } from "../../../services/event.service";
import { ToastrService } from "../../../services/toastr.service";
import { UserService } from "../../../services/user.service";
import { EntityNames } from "../../../shared/Entity-Names";

@Component({
  selector: "ngx-attendance-sent-request",
  templateUrl: "./attendance-sent-request.component.html",
  styleUrls: ["./attendance-sent-request.component.scss"],
})
export class AttendanceSentRequestComponent implements OnInit {
  dir = "ltr";
  constructor(
    private eventService: EventService,
    private userService: UserService,
    private route: ActivatedRoute,
    private toastrService: ToastrService,
    private translate: TranslateService
  ) {
    if (translate.currentLang == "ar") this.dir = "rtl";
  }
  eventId: string;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  users: BasicUserModel[] = [];
  sentRequestsRecords: number = 0;
  sentRequests: SentRequestsModel[] = [];
  columns: string[] = ["name", "invitationDate"];
  selectedUsers: BasicUserModel[] = [];
  searchModel: SearchWithEventIdModel = {
    eventId: this.route.snapshot.paramMap.get("id"),
    PageNumber: 1,
    PageSize: environment.DEFAULT_PAGE_SIZE,
    status: 0
  };
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild("autoInput") query;
  @Input() event: EventModel;
  ngOnInit() {
    this.sentRequestsSearch();
  }
  sentRequestsSearch(page?: PageEvent) {
    if (page) {
      this.searchModel.PageNumber = page.pageIndex + 1;
      this.searchModel.PageSize = page.pageSize;
    }
    this.eventService.getSentRequests(this.searchModel).subscribe((res) => {
      this.sentRequests = res.entity.entities;
      this.sentRequestsRecords = res.entity.totalRecords;
    });
  }
  reject(userId) {
    this.eventService
      .rejectRequest(userId, this.event.eventId)
      .subscribe((res) => {
        this.sentRequests = this.sentRequests.filter(
          (x) => x.receiver.userId != userId
        );
        this.toastrService.Reject(EntityNames.Invitation);
      });
  }
  onChange() {
    if (
      this.query.nativeElement.value &&
      this.query.nativeElement.value.length > 2
    ) {
      this.userService
        .autoCompletesearchUser({
          query: this.query.nativeElement.value,
          PageNumber: 1,
          PageSize: 50,
        })
        .subscribe((res) => {
          this.users = res.entity.entities.filter(
            (item) => !this.selectedUsers.some((c) => item.userId === c.userId)
          );
        });
    }
  }
  onSelectionChange(user: BasicUserModel) {
    this.query.nativeElement.value = "";
    let exists = this.selectedUsers.some((x) => x.userId == user.userId);
    if (!exists) this.selectedUsers.push(user);
  }
  removeUser(userId: string) {
    this.selectedUsers = this.selectedUsers.filter((x) => x.userId != userId);
  }
  inviteSelected() {
    let model = {
      eventId: this.event.eventId,
      invited: this.selectedUsers.map((x) => ({ userId: x.userId })),
    };
    this.eventService.sendInvitations(model).subscribe((res) => {
      this.toastrService.Create(EntityNames.Invitation);
      this.selectedUsers = [];
      this.sentRequestsSearch();
    });
  }
}
