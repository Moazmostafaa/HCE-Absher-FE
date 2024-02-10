import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { CategoryModel } from "../../../models/category/category.model";
import { FileModuleEnum } from "../../../models/file-module";
import { CategoryService } from "../../../services/category.service";
import { FileService } from "../../../services/file.service";
import { KnowledgeCenterService } from "../../../services/KnowledgeCenter.service";
import { LookupsService } from "../../../services/lookups.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { WhiteSpaceValidator } from "../../../shared/validators/white-space.validator";

@Component({
  selector: "ngx-edit-document",
  templateUrl: "./edit-document.component.html",
  styleUrls: ["./edit-document.component.scss"],
})
export class EditDocumentComponent implements OnInit {
  constructor(
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private router: Router,
    private knowledgeCenterService: KnowledgeCenterService,
    private toastrService: ToastrService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private lookupService: LookupsService,
    private translate: TranslateService
  ) {}
  documentForm: FormGroup;
  categories: CategoryModel[] = [];
  docId: string;
  attachment = "";
  isSubmitted:boolean = false;
  attachmentExtension = "";
  ngOnInit() {
    this.initForm();
    this.route.params.subscribe((params) => {
      this.docId = params["id"];
      this.knowledgeCenterService.getDocument(this.docId).subscribe((res) => {
        this.documentForm.patchValue({
          docId: res.entity.docId,
          name: res.entity.name,
          description: res.entity.description,
          link: res.entity.link,
          categoryId: res.entity.categoryId,
          attachmentId: res.entity.attachmentId,
          shareType: res.entity.shareType,
        });
        if (res.entity.attachmentId) {
          this.lookupService
            .getAttachmentById(res.entity.attachmentId)
            .subscribe((res: any) => {
              this.attachment = res.entity.fileData;
              this.attachmentExtension = res.entity.extention;
            });
        }
      });
    });
    this.categoryService
      .getCategoriesForKnowledgeCenter({
        PageNumber: 1,
        PageSize: 1000,
      })
      .subscribe((res) => {
        this.categories = res.entity.entities;
      });
  }
  initForm() {
    this.documentForm = this.formBuilder.group({
      docId: [null],
      name: ["", [Validators.required, Validators.minLength(3),WhiteSpaceValidator.noWhiteSpace]],
      description: ["", [Validators.required, Validators.minLength(3),WhiteSpaceValidator.noWhiteSpace]],
      link: ["", Validators.minLength(6)],
      categoryId: ["", [Validators.required]],
      shareType: [2, Validators.required],
      attachmentId: [null],
    });
  }

  onSubmit() {
    this.isSubmitted = true;
    if (this.documentForm.invalid) {
      return;
    }
    let dto = this.documentForm.value;
    this.knowledgeCenterService.editDocument(dto).subscribe((res) => {
      if (res.status == 200) {
        this.toastrService.Create(EntityNames.Document);
        this.router.navigate(["/pages/document"]);
      }
    });
  }
  onSelectFile(event) {
    if (event.target.files) {
      let isValid = true;
      const [file] = event.target.files;
      var readerProfile = new FileReader();
      readerProfile.onload = (event: any) => {
        if (isValid) {
          this.attachment = event.target.result;
          this.attachmentExtension = "." + file.name.split(".").pop();
        } else {
          this.attachment = "";
          this.attachmentExtension = "";
        }
      };
      readerProfile.readAsDataURL(event.target.files[0]);
      this.fileService
        .uploadVariousfile(file, (+FileModuleEnum.KnowledgeCenter).toString())
        .subscribe(
          (res) => {
            this.documentForm.controls["attachmentId"].setValue(res.entity);
          },
          () => {
            this.translate.get("errorFileType").subscribe((message) => {
              this.translate.get("error").subscribe((title) => {
                this.toastrService.danger(message, title);
                isValid = false;
                this.attachment = "";
                this.attachmentExtension = "";
              });
            });
          }
        );
    }
  }
  get fc() {
    return this.documentForm.controls;
  }
}
