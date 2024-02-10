import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse } from '../../models/base.response';
import { CitySearchModel, CityPaginatedModel, CityCreateModel, CityModel, CityUpdateModel } from '../../models/city/City.model';
import { CountryStatetRegionModel } from '../../models/state-region/state-region.model';
import { BaseService } from '../../services/base.service';
import { EndPoints } from '../../services/EndPoints';

@Injectable({
  providedIn: 'root'
})
export class CityService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: CitySearchModel) {
    return this.http.post<BaseResponse<CityPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.City.list,
      searchModel
    );
  }

  searchByCountry(searchModel: CountryStatetRegionModel) {
    return this.http.post<BaseResponse<CityPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.City.list,
      searchModel
    );
  }

  delete(id: string) {
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.City.delete + "/" + id
    );
  }
  create(model: CityCreateModel) {
    return this.http.post<BaseResponse<CityModel>>(
      EndPoints.baseUrl + EndPoints.City.create,
      model
    );
  }
  update(model: CityUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.City.update,
      model
    );
  }
  getById(id: string) {
    return this.http.get<BaseResponse<CityModel>>(
      EndPoints.baseUrl + EndPoints.City.get + "/" + id
    );
  }
}
