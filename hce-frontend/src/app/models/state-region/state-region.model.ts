import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Basic model for StateRegion

// Create Model for StateRegion
export interface StateRegionCreateModel {
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  countryId:string;
}

// Update Model for StateRegion
export interface StateRegionUpdateModel {
  regionId: string;
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  countryId: string;
}

// Details Model for StateRegion
export interface StateRegionModel {
  regionId: string;
  regionNameAr: string,
  regionNameEn: string,
  regionNameLang: string,
  regionDesc: string,
  createdBy: string;
  creationDate: string;
  countryId: string;
  countryNameAr:string;
  countryNameEn:string;
  countryNameLang:string;
}

// Paginated Model for StateRegion
export interface StateRegionPaginatedModel
  extends PaginationResponse<StateRegionModel> {}

  export interface CountryStatetRegionModel extends StateRegionSearchModel{
    countryId: string;
  }

// Search Model for StateRegion
export interface StateRegionSearchModel extends PaginationRequest {}
