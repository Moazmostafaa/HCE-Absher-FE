import { Component, OnInit } from "@angular/core";

import {
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { BasicFileModel } from "../../../models/attachment/basic.file.model";
import { FileModuleEnum } from "../../../models/file-module";
import { FileService } from "../../../services/file.service";
import { PostService } from "../../../services/post.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";

@Component({
  selector: "ngx-add-post",
  templateUrl: "./add-post.component.html",
  styleUrls: ["./add-post.component.scss"],
})
export class AddPostComponent implements OnInit {
  constructor(
    private postService: PostService,
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private router: Router,
    private toaster: ToastrService,
    private translate: TranslateService
  ) {
    this.Days.length = 7;
  }
  Types = ["Text", "Photo", "Video", "Poll"];
  currentType = 1;
  Days = [];
  submitted = false;
  isLoading = false;
  postForm: FormGroup;
  files: BasicFileModel[] = [];
  currentQuestion: any;
  answersToDelete = [];
  isSubmitted:boolean=false;

  ngOnInit() {
    this.initForm();
  }
  initForm() {
    if (this.currentType == 1) {
      this.postForm = this.formBuilder.group({
        postText: ["",[Validators.required]],
        postType: [1, [Validators.required]],
        shareType: [2, [Validators.required]],
        noOfValidDays: [7, [Validators.required]],
      });
    }

    if (this.currentType == 2 || this.currentType == 3) {
      this.postForm = this.formBuilder.group({
        postText: [""],
        postType: [this.currentType, [Validators.required]],
        shareType: [2, [Validators.required]],
        noOfValidDays: [7, [Validators.required]],
        postFiles: this.formBuilder.array([]),
      });
    }
    if (this.currentType == 4) {
      this.postForm = this.formBuilder.group({
        postType: [4, [Validators.required]],
        shareType: [2, [Validators.required]],
        noOfValidDays: [7, [Validators.required]],
        question: [""],
        deadLineDays: [""],
        question_answers: this.formBuilder.array([], [Validators.required]),
      });
    }
  }
  changePostType(e) {
    this.currentType = e;
    this.initForm();
    this.clearFiles();
  }
  addAnswer() {
    const val = this.formBuilder.group({
      answer_title: ["", Validators.required],
      answer_is_correct: [false],
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
    let model = {};
    if (this.currentType == 1) {
      model = {
        postText: dto.postText,
        postType: dto.postType,
        shareType: dto.shareType,
        noOfValidDays: dto.noOfValidDays,
      };
    }
    if (this.currentType == 2 || this.currentType == 3) {
      model = {
        postText: dto.postText,
        postType: dto.postType,
        shareType: dto.shareType,
        noOfValidDays: dto.noOfValidDays,
        postFiles: dto.postFiles,
      };
    }
    if (this.currentType == 4) {
      var answers = [];
      for (let answer = 0; answer < dto.question_answers.length; answer++) {
        answers.push({
          answer: dto.question_answers[answer].answer_title,
        });
      }
      model = {
        postType: dto.postType,
        shareType: dto.shareType,
        noOfValidDays: dto.noOfValidDays,
        pollpost: {
          question: dto.question,
          deadLineDays: dto.deadLineDays,
          answers: answers,
        },
      };
    }

    console.log("model : ", model);

    this.postService.addPost(model).subscribe((userResponse) => {
      this.toaster.Create(EntityNames.Post);
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
  get fc() {
    return this.postForm.controls;
  }
  get postFiles() {
    return this.postForm.controls["postFiles"] as FormArray;
  }
}
