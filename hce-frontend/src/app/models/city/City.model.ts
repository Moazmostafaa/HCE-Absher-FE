import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Create Model for  City
export interface CityCreateModel {
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  stateRegionId: string;
}

// Update Model for  City
export interface CityUpdateModel {
  cityId: string;
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  stateRegionId: string;
}

// Details Model for  City
export interface CityModel {
  cityId: string;
  cityNameAr: string;
  cityNameEn: string;
  cityNameLang: string;
  cityDesc: string;
  createdBy: string;
  creationDate: string;
  stateRegionId: string;
  stateRegionNameAr: string;
  stateRegionNameEn: string;
  stateRegionNameLang: string;
}

// Paginated Model for  City
export interface CityPaginatedModel extends PaginationResponse<CityModel> {}

export interface StateRegionCitiesModel extends CitySearchModel {
  stateRegionId: string;
}

// Search Model for  City
export interface CitySearchModel extends PaginationRequest {}
