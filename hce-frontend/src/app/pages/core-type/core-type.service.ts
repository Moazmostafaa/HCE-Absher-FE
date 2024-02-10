import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse } from '../../models/base.response';
import { CoreTypeCreateModel, CoreTypeModel, CoreTypePaginatedModel, CoreTypeSearchModel, CoreTypeUpdateModel } from '../../models/core-type/CoreType.model';
import { BaseService } from '../../services/base.service';
import { EndPoints } from '../../services/EndPoints';

@Injectable({
  providedIn: 'root'
})
export class CoreTypeService extends BaseService{
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: CoreTypeSearchModel) {
    return this.http.post<BaseResponse<CoreTypePaginatedModel>>(
      EndPoints.baseUrl + EndPoints.CoreType.list,
      searchModel
    );
  }
  delete(id: string) {
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.CoreType.delete + "/" + id,
    );
  }
  create(model: CoreTypeCreateModel) {
    return this.http.post<BaseResponse<CoreTypeModel>>(
      EndPoints.baseUrl + EndPoints.CoreType.create,
      model
    );
  }
  update(model: CoreTypeUpdateModel) {
    return this.http.put<BaseResponse<CoreTypeModel>>(
      EndPoints.baseUrl + EndPoints.CoreType.update,
      model
    );
  }
  getById(id: string) {
    return this.http.get<BaseResponse<CoreTypeModel>>(
      EndPoints.baseUrl + EndPoints.CoreType.get + "/" + id
    );
  }
}
