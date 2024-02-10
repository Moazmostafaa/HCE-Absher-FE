import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Create Model for  District
export interface DistrictCreateModel {
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  cityId: string;
}

// Update Model for  District
export interface DistrictUpdateModel {
  districtId: string;
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  cityId: string;
}

// Details Model for  District
export interface DistrictModel {
  districtId: string;
  districtNameAr: string;
  districtNameEn: string;
  districtNameLang: string;
  districtDesc: string;
  createdBy: string;
  creationDate: string;
  cityId: string;
  cityNameAr: string;
  cityNameEn: string;
  cityNameLang: string;
}

// Paginated Model for  District
export interface DistrictPaginatedModel extends PaginationResponse<DistrictModel> {}

export interface StateCityDistrictsModel extends DistrictSearchModel {
  cityId: string;
}

// Search Model for  District
export interface DistrictSearchModel extends PaginationRequest {}
