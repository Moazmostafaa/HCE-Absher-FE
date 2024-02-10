import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { CategoryModel } from '../../../models/category/category.model';
import { FileModuleEnum } from '../../../models/file-module';
import { CategoryService } from '../../../services/category.service';
import { FileService } from '../../../services/file.service';
import { LookupsService } from '../../../services/lookups.service';
import { ToastrService } from '../../../services/toastr.service';
import { EntityNames } from '../../../shared/Entity-Names';
import { WhiteSpaceValidator } from '../../../shared/validators/white-space.validator';

@Component({
  selector: 'ngx-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent implements OnInit {

  constructor(
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private toastrService: ToastrService,
    private translate : TranslateService,
    private lookupSerivce: LookupsService
  ) {}
  categoryForm: FormGroup;
  categoryId: string = '';
  categories: CategoryModel[] = [];
  currentCategory : CategoryModel;
  Types = ["Event", "Knowledge Center"];
  submitted = false;
  isSubmitted :boolean= false;
  categoryImageAttachment: string = "";
  categoryImageExtension: string = "";
  async ngOnInit() {
    this.categoryId = this.route.snapshot.params.id;
    this.initForm();
    await this.getCategories();
    this.getCategoryById();
  }
  getCategoryById() {
    this.categoryService.getCategoryById(this.categoryId).subscribe((res) => {
      this.currentCategory = res.entity;
      this.bindForm();
    });
  }
  bindForm() {
    this.categoryForm.controls["categoryId"].setValue(this.currentCategory.categoryId);
    this.categoryForm.controls["categoryName"].setValue(this.currentCategory.name);
    this.categoryForm.controls["description"].setValue(this.currentCategory.description);
    this.categoryForm.controls["categoryType"].setValue(this.currentCategory.categoryType);
    this.categoryForm.controls["parentCategoryId"].setValue(this.currentCategory.parentCategoryId == null? '' : this.currentCategory.parentCategoryId);
    this.categoryForm.controls["categoryPhoto"].setValue(this.currentCategory.attachmentId);
    if (this.currentCategory.attachmentId) {
      this.lookupSerivce.getAttachmentById(this.currentCategory.attachmentId)
        .subscribe((res: any) => {
          this.categoryImageAttachment = res.entity.fileData;
          this.categoryImageExtension = res.entity.extention;
        });
    }
  }
  async getCategories() {
    await this.categoryService
      .searchCategories({
        PageNumber: 1,
        PageSize: 1000,
      })
      .subscribe((res) => {
        this.categories = res.entity.entities.filter(c=>c.categoryId != this.categoryId);
      });
  }
  initForm() {
    this.categoryForm = this.formBuilder.group({
      categoryId: [this.categoryId],
      categoryName: ["", [Validators.required, Validators.minLength(3) , WhiteSpaceValidator.noWhiteSpace]],
      description: ["", [Validators.required, Validators.minLength(3), WhiteSpaceValidator.noWhiteSpace]],
      categoryType: [1, [Validators.required]],
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
      categoryId: category.categoryId,
      name: category.categoryName,
      description: category.description,
      categoryType: category.categoryType,
      attachmentId: category.categoryPhoto.length < 1 ? null : category.categoryPhoto,
      parentId: category.parentCategoryId.length < 1 ? null : category.parentCategoryId,
    };
    this.categoryService.updateCategory(model).subscribe((res) => {
      if (res.status == 200) {
        this.toastrService.Update(EntityNames.Category);
        this.router.navigate(["/pages/category"]);
      }
      else {
        this.toastrService.UpdateFailed(EntityNames.Category);
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
  get fc() {
    return this.categoryForm.controls;
  }
  getModuleId() {
    let categoryType = this.categoryForm.controls["categoryType"].value;
    if (categoryType == 1) {
      return (+FileModuleEnum.Events).toString();
    } else if (categoryType == 2) {
      return (+FileModuleEnum.KnowledgeCenter).toString();
    } else return (+FileModuleEnum.Events).toString();
  }

}
