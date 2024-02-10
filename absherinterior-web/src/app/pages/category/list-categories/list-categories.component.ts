import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import { CategorySearchModel } from "../../../models/category/category-search.model";
import {
  CategoryModel,
  CategoryTypeEnum,
} from "../../../models/category/category.model";

import { CategoryService } from "../../../services/category.service";
import { ToastrService } from "../../../services/toastr.service";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { EntityNames } from "../../../shared/Entity-Names";

@Component({
  selector: "ngx-list-categories",
  templateUrl: "./list-categories.component.html",
  styleUrls: ["./list-categories.component.scss"],
})
export class ListCategoriesComponent implements OnInit {
  dir = 'ltr';
  constructor(
    private categoryService: CategoryService,
    private dialogService: NbDialogService,
    private toastrService: ToastrService,
    private translate: TranslateService
    ) {
      if (translate.currentLang == 'ar') this.dir = 'rtl';
    }
  active = true;
  categories: CategoryModel[] = [];
  totalRecords: number = 0;
  searchModel: CategorySearchModel;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "name",
    "categoryType",
    "isCategoryOwner",
    "hasParentCategory",
    "creationDate",
    "actions",
  ];
  @ViewChild(MatPaginator) paginator: MatPaginator;

  ngOnInit() {
    this.searchModel = {
      PageNumber: 1,
      PageSize: environment.DEFAULT_PAGE_SIZE,
    };
    this.search();
  }
  search(page?: PageEvent) {
    if (page) {
      this.searchModel.PageNumber = page.pageIndex + 1;
      this.searchModel.PageSize = page.pageSize;
    }
    this.categoryService.searchCategories(this.searchModel).subscribe((res) => {
      this.categories = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
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
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.categoryService.deleteCategory(id).subscribe((result) => {
            this.categories = this.categories.filter((x) => x.categoryId != id);
            this.toastrService.Delete(EntityNames.Category);
          });
        }
      });
  }
  getCategoryType(categoryType: number) {
    return CategoryTypeEnum[categoryType];
  }
}
