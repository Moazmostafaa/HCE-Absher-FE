import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../models/base.response";
import { CategoryPaginatedModel } from "../models/Category/category-paginated.model";
import { CategorySearchModel } from "../models/category/category-search.model";
import { CategoryModel } from "../models/category/category.model";
import { PagnationRequest } from "../models/pagination.request";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class CategoryService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }
  searchCategories(searchModel: CategorySearchModel) {
    return this.http.post<BaseResponse<CategoryPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Category.list,
      searchModel
    );
  }
  deleteCategory(id: string) {
    let model = { categoryId: id };
    return this.http.request<BaseResponse<boolean>>(
      "DELETE",
      EndPoints.baseUrl + EndPoints.Category.delete,
      {
        body: model,
      }
    );
  }
  addCategory(model: any) {
    return this.http.post<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Category.add,
      model
    );
  }
  updateCategory(model: any) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Category.edit,
      model
    );
  }
  getCategoryById(id: string) {
    return  this.http.post<BaseResponse<CategoryModel>>(
      EndPoints.baseUrl + EndPoints.Category.getById,
      {
        categoryId: id,
      });
  }
  getCategoriesForKnowledgeCenter(searchModel: PagnationRequest) {
    return this.http.post<BaseResponse<CategoryPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Category.KnwoledgeCenterCategories,
      searchModel
    );
  }
  getCategoriesForEvent(searchModel: PagnationRequest) {
    return this.http.post<BaseResponse<CategoryPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Category.eventCategories,
      searchModel
    );
  }
}
