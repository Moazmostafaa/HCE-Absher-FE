import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import {
  AccessTechnologyCreateModel,
  AccessTechnologyModel,
  AccessTechnologyPaginatedModel,
  AccessTechnologySearchModel,
  AccessTechnologyUpdateModel,
} from "../../models/access-technology/AccessTechnology.model";
import { BaseResponse } from "../../models/base.response";
import { BaseService } from "../../services/base.service";
import { EndPoints } from "../../services/EndPoints";

@Injectable({
  providedIn: "root",
})
export class AccessTechnologyService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: AccessTechnologySearchModel) {
    return this.http.post<BaseResponse<AccessTechnologyPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.AccessTechnology.list,
      searchModel
    );
  }
  delete(id: string) {
    let model = { serviceid: id };
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.AccessTechnology.delete + '/' + id, 
    );
  }
  create(model: AccessTechnologyCreateModel) {
    return this.http.post<BaseResponse<AccessTechnologyModel>>(
      EndPoints.baseUrl + EndPoints.AccessTechnology.create,
      model
    );
  }
  update(model: AccessTechnologyUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.AccessTechnology.update,
      model
    );
  }
  getById(id: string) {
    let model = { serviceId: id};
    return this.http.get<BaseResponse<AccessTechnologyModel>>(
      EndPoints.baseUrl + EndPoints.AccessTechnology.get + '/' + id
    );
  }
}
