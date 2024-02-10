import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../../models/base.response";
import { GoalCreateModel, GoalModel, GoalPaginatedModel, GoalSearchModel, GoalUpdateModel } from "../../models/goal/goal.model";
import { VendorCreateModel, VendorModel, VendorPaginatedModel, VendorSearchModel, VendorUpdateModel } from "../../models/vendor/vendor.model";
import { BaseService } from "../../services/base.service";
import { EndPoints } from "../../services/EndPoints";

@Injectable({
  providedIn: "root",
})
export class VendorService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: VendorSearchModel) {
    return this.http.post<BaseResponse<VendorPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Vendor.list,
      searchModel
    );
  }
  delete(id: string) {
    let model = { vendorid: id };
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Vendor.delete + '/' + id, 
    );
  }
  create(model: VendorCreateModel) {
    return this.http.post<BaseResponse<VendorModel>>(
      EndPoints.baseUrl + EndPoints.Vendor.create,
      model
    );
  }
  update(model: VendorUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Vendor.update,
      model
    );
  }
  getById(id: string) {
    let model = { vendorId: id};
    return this.http.get<BaseResponse<VendorModel>>(
      EndPoints.baseUrl + EndPoints.Vendor.get + '/' + id
    );
  }
}
