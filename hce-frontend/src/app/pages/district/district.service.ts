import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../../models/base.response";
import {
  DistrictSearchModel,
  DistrictPaginatedModel,
  DistrictModel,
  DistrictUpdateModel,
  DistrictCreateModel,
} from "../../models/district/district.model";
import { CountryStatetRegionModel } from "../../models/state-region/state-region.model";
import { BaseService } from "../../services/base.service";
import { EndPoints } from "../../services/EndPoints";

@Injectable({
  providedIn: "root",
})
export class DistrictService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: DistrictSearchModel) {
    return this.http.post<BaseResponse<DistrictPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.District.list,
      searchModel
    );
  }

  searchByCountry(searchModel: CountryStatetRegionModel) {
    return this.http.post<BaseResponse<DistrictPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.District.list,
      searchModel
    );
  }

  delete(id: string) {
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.District.delete + "/" + id
    );
  }
  create(model: DistrictCreateModel) {
    return this.http.post<BaseResponse<DistrictModel>>(
      EndPoints.baseUrl + EndPoints.District.create,
      model
    );
  }
  update(model: DistrictUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.District.update,
      model
    );
  }
  getById(id: string) {
    return this.http.get<BaseResponse<DistrictModel>>(
      EndPoints.baseUrl + EndPoints.District.get + "/" + id
    );
  }
}
