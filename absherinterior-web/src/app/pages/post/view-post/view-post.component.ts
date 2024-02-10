import { UserImagePipe } from "./../../../shared/pipes/user-image.pipe";
import { Component, Input, OnInit } from "@angular/core";
import { PostService } from "../../../services/post.service";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { DatePipe } from "@angular/common";
import { PostModel } from "../../../models/post/post.model";
import { PostTypesEnum } from "../../../Enums/post-types.enum";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { FileService } from "../../../services/file.service";
@Component({
  selector: "ngx-view-post",
  templateUrl: "./view-post.component.html",
  styleUrls: ["./view-post.component.scss"],
})
export class ViewPostComponent implements OnInit {
  @Input() idFromShared = null;
  postId: string;
  profileAttachmentId: string;
  post: PostModel;
  fileData: string;
  extention: string;
  dir = "ltr";
  constructor(
    private postService: PostService,
    private route: ActivatedRoute,
    private router: Router,
    private dialogService: NbDialogService,
    private toastr: NbToastrService,
    private translate: TranslateService,
    private userImage: UserImagePipe
  ) {
    if (translate.currentLang == "ar") this.dir = "rtl";
  }

  ngOnInit(): void {
    this.postId = this.idFromShared || this.route.snapshot.paramMap.get("id");
    this.post = this.initPost();
    this.getCurrentPost(this.postId);
  }
  getCurrentPost(postId: string) {
    this.postService.getPostById(postId).subscribe((res) => {
      this.post = res.entity;
      this.profileAttachmentId = res.entity.userInfo.profileAttachmentId;
    });
  }

  initPost(): PostModel {
    return {
      postId: null,
      postType: null,
      postText: null,
      creationDate: null,
      sharePostId: null,
      noOfValidDays: null,
      numberOfLikes: null,
      numberOfComments: null,
      userInfo: null,
      isPostOwner: null,
      shareType: null,
      shareKnowledgeCenterId: null,
      postFiles: null,
      poll: null,
    };
  }
  async delete(id, title: string) {
    let postTitle = await this.translate
      .get("DeleteTitle", { entity: title })
      .toPromise();
    let body = await this.translate
      .get("DeleteMessage", { entity: title })
      .toPromise();

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
            this.router.navigate(["/pages/post"]);

            this.toastr.info("Post deleted successfuly", "Delete");
          });
        }
      });
  }
  getPostType(type) {
    return PostTypesEnum[type];
  }
  getVotePercent(voteValue: number) {
    if (this.post.poll.votersNumber == 0) return 0;
    return (voteValue * 100) / this.post.poll.votersNumber;
  }
}
