import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Basic model for Country

// Create Model for Country
export interface CountryCreateModel {
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  worldRegionId:string;
}

// Update Model for Country
export interface CountryUpdateModel {
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  countryId: string;
  worldRegionId:string;
}

// Details Model for Country
export interface CountryModel {
  worldRegionNameEn: string,
  worldRegionNameAr: string,
  worldRegionNameLang: string,
  creationDate: string;
  createdBy: string;
  worldRegionId:string;
  countryId: string;
  countryNameAr:string;
  countryNameEn:string;
  countryNameLang:string;
  countryDesc:string;
}

// Paginated Model for Country
export interface CountryPaginatedModel
  extends PaginationResponse<CountryModel> {}

// Search Model for Country
export interface CountrySearchModel extends PaginationRequest {}
