import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse } from '../../models/base.response';
import { CountryStatetRegionModel, StateRegionCreateModel, StateRegionModel, StateRegionPaginatedModel, StateRegionSearchModel, StateRegionUpdateModel } from '../../models/state-region/state-region.model';
import { WorldRegionSearchModel, WorldRegionPaginatedModel, WorldRegionCreateModel, WorldRegionModel, WorldRegionUpdateModel } from '../../models/world-region/WorldRegion.model';
import { BaseService } from '../../services/base.service';
import { EndPoints } from '../../services/EndPoints';

@Injectable({
  providedIn: 'root'
})
export class StateRegionService extends BaseService{
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: StateRegionSearchModel) {
    return this.http.post<BaseResponse<StateRegionPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.StateRegion.list,
      searchModel
    );
  }

  searchByCountry(searchModel: CountryStatetRegionModel) {
    return this.http.post<BaseResponse<StateRegionPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.StateRegion.list,
      searchModel
    );
  }

  delete(id: string) {
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.StateRegion.delete + "/" + id
    );
  }
  create(model: StateRegionCreateModel) {
    return this.http.post<BaseResponse<StateRegionModel>>(
      EndPoints.baseUrl + EndPoints.StateRegion.create,
      model
    );
  }
  update(model: StateRegionUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.StateRegion.update,
      model
    );
  }
  getById(id: string) {
    return this.http.get<BaseResponse<StateRegionModel>>(
      EndPoints.baseUrl + EndPoints.StateRegion.get + "/" + id
    );
  }
}
