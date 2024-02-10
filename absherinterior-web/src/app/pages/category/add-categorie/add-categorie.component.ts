import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { FileService } from "../../../services/file.service";
import { FileModuleEnum } from "../../../models/file-module";
import { CategoryModel } from "../../../models/category/category.model";
import { CategoryService } from "../../../services/category.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { TranslateService } from "@ngx-translate/core";
import {WhiteSpaceValidator } from "../../../shared/validators/white-space.validator";

@Component({
  selector: "ngx-add-categorie",
  templateUrl: "./add-categorie.component.html",
  styleUrls: ["./add-categorie.component.scss"],
})
export class AddCategorieComponent implements OnInit {
  constructor(
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
    private toastrService: ToastrService,
    private translate: TranslateService
  ) {}
  categoryForm: FormGroup;
  categories: CategoryModel[] = [];
  Types = ["Event", "Knowledge Center"];
  submitted = false;
  isSubmitted :boolean= false;
  categoryImageAttachment: string = "";
  categoryImageExtension: string = "";
  ngOnInit() {
    this.initForm();
    this.getCategories();
  }
  getCategories() {
    this.categoryService
      .searchCategories({
        PageNumber: 1,
        PageSize: 1000,
      })
      .subscribe((res) => {
        this.categories = res.entity.entities;
      });
  }
  initForm() {
    this.categoryForm = this.formBuilder.group({
      categoryName: ["", [Validators.required, Validators.minLength(3),WhiteSpaceValidator.noWhiteSpace]],
      description: ["", [Validators.required, Validators.minLength(3), WhiteSpaceValidator.noWhiteSpace]],
      categoryType: [1, [Validators.required ] ],
      categoryPhoto: [""],
      parentCategoryId: [""],
    });
  }
  onSubmit() {
    this.submitted = true;
    this.isSubmitted = true;
    if (this.categoryForm.invalid) {
      return;
    }
    const category = this.categoryForm.value;
    let model = {
      name: category.categoryName,
      description: category.description,
      categoryType: category.categoryType,
      attachmentId:
        category.categoryPhoto.length < 1 ? null : category.categoryPhoto,
      parentId:
        category.parentCategoryId.length < 1 ? null : category.parentCategoryId,
    };
    this.categoryService.addCategory(model).subscribe((res) => {
      if (res.status == 200) {
        this.toastrService.Create(EntityNames.Category);
        this.router.navigate(["/pages/category"]);
      }
    });
  }
  onSelectImage(event) {
    if (event.target.files) {
      let isValid = true;
      const [file] = event.target.files;
      var readerProfile = new FileReader();
      readerProfile.onload = (event: any) => {
        if (isValid) {
          this.categoryImageAttachment = event.target.result;
          this.categoryImageExtension = "." + file.name.split(".").pop();
        } else {
          this.categoryImageAttachment = "";
          this.categoryImageExtension = "";
        }
      };
      readerProfile.readAsDataURL(event.target.files[0]);
      this.fileService.uploadImage(file, this.getModuleId()).subscribe(
        (res) => {
          this.categoryForm.controls["categoryPhoto"].setValue(res.entity);
        },
        () => {
          this.translate.get("errorUploadImageType").subscribe((message) => {
            this.translate.get("error").subscribe((title) => {
              this.toastrService.danger(message, title);
              isValid = false;
            });
          });
        }
      );
    }
  }
  getModuleId() {
    let categoryType = this.categoryForm.controls["categoryType"].value;
    if (categoryType == 1) {
      return (+FileModuleEnum.Events).toString();
    } else if (categoryType == 2) {
      return (+FileModuleEnum.KnowledgeCenter).toString();
    } else return (+FileModuleEnum.Events).toString();
  }
  get fc() {
    return this.categoryForm.controls;
  }
}
