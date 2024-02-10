import { Component, Input, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { ActivatedRoute } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import { SearchWithEventIdModel } from "../../../models/event/search-with-event.model";
import { PendingRequestsModel } from "../../../models/event/pending-requests.model";
import { EventService } from "../../../services/event.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { EventModel } from "../../../models/event/event.model";
import { BasicUserModel } from "../../../models/User/basic-user.model";
import { UserService } from "../../../services/user.service";

@Component({
  selector: "attendance-pending-requests",
  templateUrl: "./attendance-pending-requests.component.html",
  styleUrls: ["./attendance-pending-requests.component.scss"],
})
export class AttendancePendingRequestsComponent implements OnInit {
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
  pendingRequestsRecords: number = 0;
  pendingRequests: PendingRequestsModel[] = [];
  pendingRequestsColumns: string[] = ["name", "invitationDate", "actions"];
  selectedUsers: BasicUserModel[] = [];
  pendingRequestsSearchModel: SearchWithEventIdModel = {
    eventId: this.route.snapshot.paramMap.get("id"),
    PageNumber: 1,
    PageSize: environment.DEFAULT_PAGE_SIZE,
    status: 0
  };
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild("autoInput") query;
  @Input() event: EventModel;
  ngOnInit() {
    this.pendingRequestsSearch();
  }
  pendingRequestsSearch(page?: PageEvent) {
    if (page) {
      this.pendingRequestsSearchModel.PageNumber = page.pageIndex + 1;
      this.pendingRequestsSearchModel.PageSize = page.pageSize;
    }
    this.eventService
      .getPendingRequests(this.pendingRequestsSearchModel)
      .subscribe((res) => {
        this.pendingRequests = res.entity.entities;
        this.pendingRequestsRecords = res.entity.totalRecords;
      });
  }
  accept(userId) {
    this.eventService.acceptRequest(userId, this.event.eventId).subscribe((res) => {
      this.pendingRequests = this.pendingRequests.filter(
        (x) => x.sender.userId != userId
      );
      this.toastrService.Accept(EntityNames.Invitation);
    });
  }
  reject(userId) {
    this.eventService.rejectRequest(userId, this.event.eventId).subscribe((res) => {
      this.pendingRequests = this.pendingRequests.filter(
        (x) => x.sender.userId != userId
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
  inviteSelected(){
    let model = {
      eventId: this.event.eventId,
      invited: this.selectedUsers.map((x) => ({ userId: x.userId })),
    }
    this.eventService.sendInvitations(model).subscribe((res) => {
      this.toastrService.Create(EntityNames.Invitation);
      this.selectedUsers = [];
      this.pendingRequestsSearch();
    });
  }
}
