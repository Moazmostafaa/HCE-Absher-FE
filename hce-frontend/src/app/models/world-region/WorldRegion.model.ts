import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Basic model for WorldRegion

// Create Model for WorldRegion
export interface WorldRegionCreateModel {
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
}

// Update Model for WorldRegion
export interface WorldRegionUpdateModel {
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  isTop: boolean;
  level: number;
  countryId: string
  regionId: string
}

// Details Model for WorldRegion
export interface WorldRegionModel {
  regionNameAr: string,
  regionNameEn: string,
  regionNameLang: string,
  regionDesc: string,
  creationDate: string;
  createdBy: string;
  regionId: string;
}

// Paginated Model for WorldRegion
export interface WorldRegionPaginatedModel
  extends PaginationResponse<WorldRegionModel> {}

// Search Model for WorldRegion
export interface WorldRegionSearchModel extends PaginationRequest {}
