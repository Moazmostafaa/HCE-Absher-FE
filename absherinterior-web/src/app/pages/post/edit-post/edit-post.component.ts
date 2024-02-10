import { Base64ImagePipe } from './../../../shared/pipes/base64-image.pipe';
import { Component, OnInit } from "@angular/core";

import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { DomSanitizer } from "@angular/platform-browser";
import { ActivatedRoute, Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { BasicFileModel } from "../../../models/attachment/basic.file.model";
import { FileModuleEnum } from "../../../models/file-module";
import { PostModel } from "../../../models/post/post.model";
import { UpdatePostModel } from "../../../models/post/update-post.model";
import { FileService } from "../../../services/file.service";
import { LookupsService } from "../../../services/lookups.service";
import { PostService } from "../../../services/post.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { Base64VideoPipe } from '../../../shared/pipes/base64-video.pipe';

@Component({
  selector: "ngx-edit-post",
  templateUrl: "./edit-post.component.html",
  styleUrls: ["./edit-post.component.scss"],
})
export class EditPostComponent implements OnInit {
  constructor(
    private postService: PostService,
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private toaster: ToastrService,
    private lookupSerivce: LookupsService,
    private translate: TranslateService,
    private base64ImagePipe: Base64ImagePipe,
    private base64VideoPipe: Base64VideoPipe,
  ) {
    this.Days.length = 7;
  }
  Types = ["Text", "Photo", "Video", "Poll"];
  currentType = 1;
  Days = [];
  submitted = false;
  isSubmitted=false;
  isLoading = false;
  postForm: FormGroup;
  files: BasicFileModel[] = [];
  currentQuestion: any;
  answersToDelete = [];
  postId: string;
  currentPost: PostModel;
  attachments: any[] = [];


  ngOnInit() {
    this.postId = this.route.snapshot.params.id;
    this.initForm();
    this.getPostDetails();

  }
  initForm() {
    if (this.currentType == 1) {
      this.postForm = this.formBuilder.group({
        postText: [
          this.currentPost ? this.currentPost.postText : "",
          [Validators.required],
        ],
        postType: [this.currentType, [Validators.required]],
        shareType: [
          this.currentPost ? this.currentPost.shareType : 2,
          [Validators.required],
        ],
        noOfValidDays: [
          this.currentPost ? this.currentPost.noOfValidDays : 7,
          [Validators.required],
        ],
      });
    }
    if (this.currentType == 2 || this.currentType == 3) {
      this.postForm = this.formBuilder.group({
        postText: [this.currentPost ? this.currentPost.postText : ""],
        postType: [this.currentType, [Validators.required]],
        shareType: [
          this.currentPost ? this.currentPost.shareType : 2,
          [Validators.required],
        ],
        noOfValidDays: [
          this.currentPost ? this.currentPost.noOfValidDays : 7,
          [Validators.required],
        ],
        postFiles: this.formBuilder.array([]),
      });
      if (this.currentPost.postFiles) {
        let filesLoaded = 0;
        this.isLoading = true;
        for (
          let attach = 0;
          attach < this.currentPost.postFiles.length;
          attach++
        ) {
          this.lookupSerivce
            .getAttachmentById(this.currentPost.postFiles[attach].attachmentId)
            .subscribe((res) => {
              let file: BasicFileModel = {
                id: res.entity.attachmentId,
                fileData: res.entity.fileData,
                extension: res.entity.extention,
              };
              this.files.push(file);
              this.postFiles.push(new FormControl(res.entity.attachmentId));
              filesLoaded++;
              this.isLoading = false;
            }),
            () => (this.isLoading = false);
        }
      }
    }
    if (this.currentType == 4) {
      this.postForm = this.formBuilder.group({
        pollId: [this.currentPost ? this.currentPost.poll?.pollId : ""],
        postType: [this.currentType, [Validators.required]],
        shareType: [
          this.currentPost ? this.currentPost.shareType : 2,
          [Validators.required],
        ],
        noOfValidDays: [
          this.currentPost ? this.currentPost.noOfValidDays : 7,
          [Validators.required],
        ],
        question: [this.currentPost ? this.currentPost.poll?.question : ""],
        deadLineDays: [
          this.currentPost ? this.currentPost.poll?.deadLineDays : "",
        ],
        question_answers: this.formBuilder.array([], [Validators.required]),
      });
      this.currentPost.poll?.answers?.forEach((answer) => {
        let answerForm = this.formBuilder.group({
          answer_id: [answer.pollAnswerId],
          answer_title: [answer.answer, Validators.required],
        });
        this.questionAnswers.push(answerForm);
      });
    }


  }
  getPostDetails() {
    this.postService.getPostById(this.postId).subscribe((res) => {
      this.currentPost = res.entity;
      this.currentType = this.currentPost.postType;
      this.initForm();
    });
  }
  changePostType(e) {
    this.currentType = e;
    this.clearFiles();
    this.initForm();
  }
  addAnswer() {
    const val = this.formBuilder.group({
      answer_id: [null],
      answer_title: ["", Validators.required],
    });

    const form = this.postForm.get("question_answers") as FormArray;

    if (this.postForm.controls.question_answers?.value.length < 5) {
      form.push(val);
    } else {
      this.toaster.danger("max answers is 5", "Error");
    }
  }
  addAnswers(answer_title, correct, answer_id) {
    const val = this.formBuilder.group({
      answer_id: [answer_id],
      answer_title: [answer_title, Validators.required],
      answer_is_correct: [correct],
    });

    const form = this.postForm.get("question_answers") as FormArray;

    if (this.postForm.controls.question_answers?.value.length < 5) {
      form.push(val);
    } else {
      this.toaster.danger("max answers is 5", "Error");
    }
  }
  trackByFn(index: any, item: any) {
    return index;
  }
  updateCorrectAnswer(answerIndex) {
    let newAnswers = [...this.postForm.controls.question_answers.value];
    newAnswers.map((answer, index) => {
      if (index === answerIndex) answer.answer_is_correct = true;
      else answer.answer_is_correct = false;
    });
    this.postForm.patchValue({
      question_answers: newAnswers,
    });
  }
  removeAnswer(index) {
    const form = this.postForm.get("question_answers") as FormArray;
    if (form.value[index].answer_id) {
      this.answersToDelete.push(form.value[index].answer_id);
    }
    form.removeAt(index);
  }
  onSubmit() {
    this.submitted = true;
    this.isSubmitted = true;
    if (this.postForm.invalid) {
      return;
    }
    let dto = this.postForm.value;
    let model: UpdatePostModel;
    if (this.currentType == 1) {
      model = {
        postId: this.postId,
        postText: dto.postText,
        postType: dto.postType,
        shareType: dto.shareType,
        noOfValidDays: dto.noOfValidDays,
        postFiles: null,
        pollPost: null,
      };
    }
    if (this.currentType == 2 || this.currentType == 3) {
      model = {
        postId: this.postId,
        postText: dto.postText,
        postType: dto.postType,
        shareType: dto.shareType,
        noOfValidDays: dto.noOfValidDays,
        postFiles: dto.postFiles,
        pollPost: null,
      };
    }
    if (this.currentType == 4) {
      var answers = [];
      for (let answer = 0; answer < dto.question_answers.length; answer++) {
        answers.push({
          pollAnswerId: dto.question_answers[answer].answer_id,
          answer: dto.question_answers[answer].answer_title,
        });
      }
      model = {
        postId: this.postId,
        postType: dto.postType,
        shareType: dto.shareType,
        noOfValidDays: dto.noOfValidDays,
        postText: dto.postText,
        postFiles: null,
        pollPost: {
          pollId: dto.pollId,
          question: dto.question,
          deadLineDays: dto.deadLineDays,
          answers: answers,
        },
      };
    }
    this.postService.updatePost(model).subscribe((userResponse) => {
      this.toaster.Update(EntityNames.Post);
      this.router.navigate(["pages/post/"]);
    });
  }

  clearFiles() {
    this.files.forEach((element) => this.deleteAttachment(element.id));
  }
  deleteAttachment(attachmentId) {
    this.postFiles.removeAt(
      this.postFiles.value.findIndex((file) => file == attachmentId)
    );
    this.fileService.deleteFile(attachmentId).toPromise();
    this.files = this.files.filter((c) => c.id != attachmentId);
  }
  onSelectFile(event) {
    this.isLoading = true;
    let filesLoaded = 0;
    if (event.target.files) {
      let files = event.target.files;
      for (let i = 0; i < event.target.files.length; i++) {
        var readerProfile = new FileReader();
        readerProfile.onload = (event: any) => {
          this.fileService
            .uploadImage(files[i], (+FileModuleEnum.Post).toString())
            .subscribe(
              (res) => {
                this.files.push({
                  id: res.entity,
                  fileData: event.target.result,
                  extension: "." + files[i].name.split(".").pop(),
                });
                this.postFiles.push(new FormControl(res.entity));
                filesLoaded++;
                this.isLoading = filesLoaded == files.length ? false : true;
              },
              () => {
                this.translate
                  .get("errorUploadImageType")
                  .subscribe((message) => {
                    this.translate.get("error").subscribe((title) => {
                      this.toaster.danger(message, title);
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
  onSelectVideo(event) {
    if (event.target.files && event.target.files.length > 0) {
      this.isLoading = true;
      let filesLoaded = 0;
      let files = event.target.files;
      for (let i = 0; i < event.target.files.length; i++) {
        var readerProfile = new FileReader();
        readerProfile.onload = (event: any) => {
          this.fileService
            .uploadVideo(files[i], (+FileModuleEnum.Post).toString())
            .subscribe(
              (res) => {
                this.files.push({
                  id: res.entity,
                  fileData: event.target.result,
                  extension: "." + files[i].name.split(".").pop(),
                  file: files[i],
                });
                this.postFiles.push(new FormControl(res.entity));
                filesLoaded++;
                this.isLoading = filesLoaded == files.length ? false : true;
              },
              () => {
                this.translate
                  .get("errorUploadVideoType")
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
  getPostAttachments(postFiles: any[]) {
    postFiles.map((file, i) => {
      return this.lookupSerivce
        .getAttachmentById(file.attachmentId)
        .subscribe((res: any) => {
          if (file.fileType == 1) {
            this.attachments.push(
              this.base64ImagePipe.transform(
                res.entity.fileData,
                res.entity.extention
              )
            );
          } else {
            this.attachments.push(
              this.base64VideoPipe.transform(
                res.entity.fileData,
                res.entity.extention
              )
            );
          }
        });
    });
  }

  get fc() {
    return this.postForm.controls;
  }
  get postFiles() {
    return this.postForm.controls["postFiles"] as FormArray;
  }
  get questionAnswers() {
    return this.postForm.controls["question_answers"] as FormArray;
  }


}
