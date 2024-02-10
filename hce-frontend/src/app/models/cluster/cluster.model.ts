import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Create Model for  District
export interface ClusterCreateModel {
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  districtId: string;
}

// Update Model for  Cluster
export interface ClusterUpdateModel {
  clusterId: string;
  nameAr: string;
  nameEn: string;
  nameLang: string;
  desc: string;
  districtId: string;
}

// Details Model for  Cluster
export interface ClusterModel {
  clusterId: string;
  clusterNameAr: string;
  clusterNameEn: string;
  clusterNameLang: string;
  clusterDesc: string;
  createdBy: string;
  creationDate: string;
  districtId: string;
  districtNameAr: string;
  districtNameEn: string;
  districtNameLang: string;
}

// Paginated Model for  Cluster
export interface ClusterPaginatedModel extends PaginationResponse<ClusterModel> {}

export interface StatedistrictClustersModel extends ClusterSearchModel {
  districtId: string;
}

// Search Model for  Cluster
export interface ClusterSearchModel extends PaginationRequest {}
