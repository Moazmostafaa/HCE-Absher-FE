import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse } from '../../models/base.response';
import { ClusterSearchModel, ClusterPaginatedModel, ClusterCreateModel, ClusterModel, ClusterUpdateModel } from '../../models/cluster/cluster.model';
import { CountryStatetRegionModel } from '../../models/state-region/state-region.model';
import { BaseService } from '../../services/base.service';
import { EndPoints } from '../../services/EndPoints';

@Injectable({
  providedIn: 'root'
})
export class ClusterService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: ClusterSearchModel) {
    return this.http.post<BaseResponse<ClusterPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Cluster.list,
      searchModel
    );
  }

  searchByCountry(searchModel: CountryStatetRegionModel) {
    return this.http.post<BaseResponse<ClusterPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Cluster.list,
      searchModel
    );
  }

  delete(id: string) {
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Cluster.delete + "/" + id
    );
  }
  create(model: ClusterCreateModel) {
    return this.http.post<BaseResponse<ClusterModel>>(
      EndPoints.baseUrl + EndPoints.Cluster.create,
      model
    );
  }
  update(model: ClusterUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Cluster.update,
      model
    );
  }
  getById(id: string) {
    return this.http.get<BaseResponse<ClusterModel>>(
      EndPoints.baseUrl + EndPoints.Cluster.get + "/" + id
    );
  }
}
