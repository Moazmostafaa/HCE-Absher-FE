import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse } from '../../models/base.response';
import { CountryCreateModel, CountryModel, CountryPaginatedModel, CountrySearchModel, CountryUpdateModel } from '../../models/country/country.model';
import { WorldRegionSearchModel, WorldRegionPaginatedModel, WorldRegionCreateModel, WorldRegionModel, WorldRegionUpdateModel } from '../../models/world-region/WorldRegion.model';
import { BaseService } from '../../services/base.service';
import { EndPoints } from '../../services/EndPoints';

@Injectable({
  providedIn: 'root'
})
export class CountryService extends BaseService{
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: CountrySearchModel) {
    return this.http.post<BaseResponse<CountryPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Country.list,
      searchModel
    );
  }
  delete(id: string) {
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Country.delete + "/" + id
    );
  }
  create(model: CountryCreateModel) {
    return this.http.post<BaseResponse<CountryModel>>(
      EndPoints.baseUrl + EndPoints.Country.create,
      model
    );
  }
  update(model: CountryUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Country.update,
      model
    );
  }
  getById(id: string) {
    return this.http.get<BaseResponse<CountryModel>>(
      EndPoints.baseUrl + EndPoints.Country.get + "/" + id
    );
  }
}
