import { Component, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";

import { UserSearchModel } from "../../../models/User/user-search.model";
import { UserModel } from "../../../models/User/user.model";
import { ToastrService } from "../../../services/toastr.service";
import { UserService } from "../../../services/user.service";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { EntityNames } from "../../../shared/Entity-Names";

@Component({
  selector: "ngx-list-users",
  templateUrl: "./list-users.component.html",
  styleUrls: ["./list-users.component.scss"],
})
export class ListUsersComponent implements OnInit {
  active = true;
  blockPeriod = 0;
  userBlockLawId;
  laws = [];
  lang = localStorage.getItem("language");
  constructor(
    private userService: UserService,
    private dialogService: NbDialogService,
    private toastr: ToastrService,
    private translate: TranslateService
  ) {
    this.getBlockLaws();
    this.lang = translate.currentLang;
  }
  userId;
  users: UserModel[] = [];
  displayedColumns: string[] = [
    "Name",
    "Role",
    "Email",
    "Phone",
    "actions",
    "Activate",
  ];
  totalCount: number = 0;
  searchModel: UserSearchModel = {
    PageSize: 10,
    PageNumber: 1,
  };
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit() {
    this.search();
  }
  getBlockLaws() {
    this.userService.getBlockLaws().subscribe((res: any) => {
      this.laws = res.entity.entities;
    });
  }
  viewDialog(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog);
  }
  async deacivate(ref) {
    if (this.blockPeriod < 1) {
      var result  = await this.translate.get("Please select a valid block period").toPromise();
      this.toastr.warning("", result);
      return;
    }
    this.userService
      .userDeActivation({
        userId: this.userId,
        userBlockLawId: this.userBlockLawId,
        blockPeriod: this.blockPeriod,
      })
      .subscribe((res) => {
        ref.close();
        this.toastr.Deactivate(EntityNames.User);
        this.search();
      });
  }
  changeStatus(element, dialog) {
    this.userId = element.userId;
    if (element.isActive) {
      element.isActive = false;
      this.userService
        .userActivation({ userId: element.userId })
        .subscribe((res) => {
          element.isActive = true;
          this.toastr.Activate(EntityNames.User);
        });
    } else {
      element.isActive = true;
      this.userBlockLawId = null;
      this.viewDialog(dialog);
    }
    // console.log("user : ", element.id);
  }

  async delete(id: string, name: string) {
    name = await this.translate.get(name).toPromise();
    let title = await this.translate.get("DeleteTitle", { entity: name }).toPromise();
    let body = await this.translate.get("DeleteMessage", { entity: name }).toPromise();
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `${title}`,
          body: `${body}?`,
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.userService.deleteUser(id).subscribe((result) => {
            this.users = this.users.filter((x) => x.userId != id);
            this.toastr.Delete(EntityNames.User);
          });
        }
      });
  }
  search(page?: PageEvent) {
    if (page) {
      this.searchModel.PageNumber = page.pageIndex + 1;
      this.searchModel.PageSize = page.pageSize;
    }
    this.userService.searchUser(this.searchModel).subscribe((res) => {
      if (res.isSuccess) {
        this.users = res.entity.entities;
        this.totalCount = res.entity.totalRecords;
        // for (let u = 0; u < this.users.length; u++) {
        //   this.users[u]["isActive"] = this.users[u]["isBlocked"] ? false : true;
        // }
      }
    });
  }
  onSearchChange(key) {
    // if (key.length >= 3 || key.length == 0) this.search();
  }
}
