import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../models/base.response";
import { CityModel } from "../models/city/City.model";
import { CountryModel } from "../models/country/country.model";
import { DistrictModel } from "../models/district/district.model";
import { StateRegionModel } from "../models/state-region/state-region.model";
import { WorldRegionModel } from "../models/world-region/WorldRegion.model";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class LookupService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }
  worldRegions() {
    return this.http.get<BaseResponse<WorldRegionModel[]>>(
      EndPoints.baseUrl + EndPoints.Lookups.worldRegions
    );
  }
  countries(worldRegionId?: string) {
    return this.http.get<BaseResponse<CountryModel[]>>(
      EndPoints.baseUrl +
        EndPoints.Lookups.countries +
        `?WorldRegionId=${worldRegionId}`
    );
  }
  stateRegions(countryId?: string) {
    return this.http.get<BaseResponse<StateRegionModel[]>>(
      EndPoints.baseUrl +
        EndPoints.Lookups.stateRegions +
        `?countryId=${countryId}`
    );
  }

  cities(stateRegionId?: string) {
    return this.http.get<BaseResponse<CityModel[]>>(
      EndPoints.baseUrl +
        EndPoints.Lookups.cities +
        `?stateRegionId=${stateRegionId}`
    );
  }

  districts(cityId?: string) {
    return this.http.get<BaseResponse<DistrictModel[]>>(
      EndPoints.baseUrl +
        EndPoints.Lookups.distrcits +
        `?cityId=${cityId}`
    );
  }
}
