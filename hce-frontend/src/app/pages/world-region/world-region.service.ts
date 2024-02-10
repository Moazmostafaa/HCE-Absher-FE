import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse } from '../../models/base.response';
import { WorldRegionSearchModel, WorldRegionPaginatedModel, WorldRegionCreateModel, WorldRegionModel, WorldRegionUpdateModel } from '../../models/world-region/WorldRegion.model';
import { BaseService } from '../../services/base.service';
import { EndPoints } from '../../services/EndPoints';

@Injectable({
  providedIn: 'root'
})
export class WorldRegionService extends BaseService{
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: WorldRegionSearchModel) {
    return this.http.post<BaseResponse<WorldRegionPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.WorldRegion.list,
      searchModel
    );
  }
  delete(id: string) {
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.WorldRegion.delete + "/" + id
    );
  }
  create(model: WorldRegionCreateModel) {
    return this.http.post<BaseResponse<WorldRegionModel>>(
      EndPoints.baseUrl + EndPoints.WorldRegion.create,
      model
    );
  }
  update(model: WorldRegionUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.WorldRegion.update,
      model
    );
  }
  getById(id: string) {
    return this.http.get<BaseResponse<WorldRegionModel>>(
      EndPoints.baseUrl + EndPoints.WorldRegion.get + "/" + id
    );
  }
}
