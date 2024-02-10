import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AttachmentModel } from "../models/attachment/attachment.model";
import { BaseResponse } from "../models/base.response";
import { CategoryModel } from "../models/category/category.model";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class LookupsService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }
  getAllCategories() {
    return this.http.get<BaseResponse<CategoryModel[]>>(
      EndPoints.baseUrl + EndPoints.Category.list
    );
  }
  getAttachmentById(fileId: any) {
    return this.http.get<BaseResponse<AttachmentModel>>(
      EndPoints.baseUrl + EndPoints.Lookups.file + fileId
    );
  }
  getAttachmentListByIds(fileListIds: any[]) {
    return this.http.get<BaseResponse<AttachmentModel>>(
      EndPoints.baseUrl + EndPoints.Lookups.filesList + fileListIds
    );
  }
  getAllRoles() {
    return this.http.post<any>(EndPoints.baseUrl + EndPoints.Lookups.role, {
      pageSize: 100,
      pageNumber: 1,
    });
  }
  getAllColors() {
    return this.http.get<any>(EndPoints.baseUrl + EndPoints.Colors.list);
  }
  getAllCurencies() {
    return this.http.get<any>(EndPoints.baseUrl + EndPoints.Curencies.list);
  }
  getAllCities() {
    return this.http.get<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Lookups.city
    );
  }
}
