import { UserService } from './../../../services/user.service';
import { PostService } from "./../../../services/post.service";
import { Component,ViewChild, OnInit ,TemplateRef} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { environment } from "../../../../environments/environment";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";

@Component({
  selector: "ngx-post-comments",
  templateUrl: "./post-comments.component.html",
  styleUrls: ["./post-comments.component.scss"],
})
export class PostCommentsComponent implements OnInit {
  dir = "ltr";

  constructor(
    private postService: PostService,
    private route: ActivatedRoute,
    private toastr: NbToastrService,
    private translation: TranslateService,
    private dialogService: NbDialogService,
    private userService: UserService,

  ) { if (translation.currentLang == "ar") this.dir = "rtl";}
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  commentsData : [];
  commentsCount: number;
  userId;
  blockPeriod = 0;
  userBlockLawId;
  laws = [];
  ngOnInit() {
    this.getComments();
    this.getBlockLaws();

  }
  getComments(){
    this.postService
      .getCommentsByPostById(this.commentsModel)
      .subscribe((res: any) => {
        // console.log("before pagination",res);
        this.commentsData = res.entity.entities
        this.commentsCount = res.entity.totalRecords
      });

  }
  viewDialog(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog);
  }
  commentsModel = {
    PostId: this.route.snapshot.paramMap.get("id"),
    PageSize: 10,
    PageNumber: 1,
  };
  paginatorChange(page?: PageEvent) {
    if (page) {
      this.commentsModel.PageNumber = page.pageIndex + 1;
      this.commentsModel.PageSize = page.pageSize;
    }

    this.getComments()
  }
  // deactivate and activate user
  getBlockLaws() {
    this.userService.getBlockLaws().subscribe((res: any) => {
      this.laws = res.entity.entities;
    });
  }
  async deacivate(ref) {
    let deactivatedMessage = await this.translation.get("User deactivated successfuly").toPromise();

    if (this.blockPeriod < 1) {
      var result  = await this.translation.get("Please select a valid block period").toPromise();
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
        this.getComments();

      });
  }
 async  changeStatus(commentUserId,isActive, dialog) {
    this.userId = commentUserId;
    let activatedMessage = await this.translation.get("User activated successfuly").toPromise();

    if (isActive) {
      this.userService
        .userActivation({ userId: commentUserId })
        .subscribe((res) => {
          this.toastr.success(activatedMessage);

          this.getComments();
        });
    } else {
      this.userBlockLawId = null;
      this.viewDialog(dialog);
    }

  }


  async delete(id, author: string) {
    let authorName =  await this.translation.get("DeleteComment", { entity: author }).toPromise();
    let body =  await this.translation.get("DeleteCommentMessage", { entity: author }).toPromise();
    let Delete =  await this.translation.get("Delete").toPromise();
    let deletedSuccessfuly =  await this.translation.get("deletedSuccessfuly").toPromise();
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `${authorName}`,
          body: `${body}`,
        },
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.postService.deleteComment(id).subscribe((result) => {
            this.toastr.info(deletedSuccessfuly, Delete);
            this.getComments();

          });
        }
      });
  }
}
