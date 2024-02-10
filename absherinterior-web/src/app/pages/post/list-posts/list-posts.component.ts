
import { Component, OnInit, ViewChild, TemplateRef } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import { PostTypesEnum } from "../../../Enums/post-types.enum";

import { postSearchModel } from "../../../models/post/post-search.model";
import { PostModel } from "../../../models/post/post.model";
import { PostService } from "../../../services/post.service";
import { UserService } from "../../../services/user.service";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";

@Component({
  selector: "ngx-list-posts",
  templateUrl: "./list-posts.component.html",
  styleUrls: ["./list-posts.component.scss"],
})
export class ListPostsComponent implements OnInit {
  dir = "ltr";

  active = true;
  userId;
  blockPeriod = 0;
  userBlockLawId;
  laws = [];
  lang = localStorage.getItem("language")
    ? localStorage.getItem("language")
    : "en";
  constructor(
    private postService: PostService,
    private dialogService: NbDialogService,
    private userService: UserService,
    private toastr: NbToastrService,
    private translation: TranslateService
  ) {    if (translation.currentLang == "ar") this.dir = "rtl";
}
  posts = [];
  displayedColumns: string[] = [
    "owner",
    "title",
    "type",
    "CreationDate",
    "shareKnowledgeCenterId",
    "isShared",
    "actions",
    "Activate",
  ];
  totalCount: number = 0;
  searchModel = {
    PageSize: 10,
    PageNumber: 1,
  };
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit() {
    this.search();
    this.getBlockLaws();
    this.postService.getPosts(this.searchModel).subscribe((res: any) => {
      this.posts = res.entity.entities;
      // console.log(this.posts);

    });
  }
  getBlockLaws() {
    this.userService.getBlockLaws().subscribe((res: any) => {
      this.laws = res.entity.entities;
    });
  }
  async deacivate(ref) {
    let deactivatedMessage = await this.translation.get("User deactivated successfuly").toPromise();

    if (this.blockPeriod < 1) {
      var result  = await this.translation.get(deactivatedMessage).toPromise();
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
        this.toastr.success(deactivatedMessage);
        this.search();
      });
  }
  async changeStatus(element, dialog) {
    let activatedMessage = await this.translation.get("User activated successfuly").toPromise();

    this.userId = element.userId;
    if (element.isActive) {
      element.isActive = false;
      this.userService
        .userActivation({ userId: element.userId })
        .subscribe((res) => {
          element.isActive = true;
          this.toastr.success(activatedMessage);

          this.search();
        });
    } else {
      element.isActive = true;
      this.userBlockLawId = null;
      this.viewDialog(dialog);
    }
    // console.log("user : ", element.id);
  }
  viewDialog(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog);
  }
  async delete(id, title: string) {
    let deletedSuccessfuly =  await this.translation.get("deletedSuccessfuly").toPromise();
    let deleted =  await this.translation.get("Delete").toPromise();

    let postTitle =  await this.translation.get("DeleteTitle", { entity: title }).toPromise();
    let body =  await this.translation.get("DeleteMessage", { entity: title }).toPromise();
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `${postTitle}`,
          body: `${body}`,
        },
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.postService.deletePost(id).subscribe((result) => {
            this.posts = this.posts.filter((x) => x.postId != id);
            this.toastr.info(deletedSuccessfuly, deleted);
          });
        }
      });
  }
  search(page?: PageEvent) {
    if (page) {
      this.searchModel.PageNumber = page.pageIndex + 1;
      this.searchModel.PageSize = page.pageSize;
    }
    this.postService.getPosts(this.searchModel).subscribe((res: any) => {
      this.posts = res.entity.entities;
      this.totalCount = res.entity.totalRecords;
    });
  }
  getPostType(type) {
    return PostTypesEnum[type].toString();
  }
}
