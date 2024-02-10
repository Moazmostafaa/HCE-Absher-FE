import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Basic model for AccessTechnology
export interface AccessTechnologyBasicModel {
  serviceName: string;
  serviceDesc: string;
}

// Create Model for AccessTechnology
export interface AccessTechnologyCreateModel
  extends AccessTechnologyBasicModel {}

// Update Model for AccessTechnology
export interface AccessTechnologyUpdateModel
  extends AccessTechnologyBasicModel {
  serviceId: string;
}

// Details Model for AccessTechnology
export interface AccessTechnologyModel
  extends AccessTechnologyBasicModel {
  serviceId: string;
  creationDate: any;
  createdBy: string;
}

// Paginated Model for AccessTechnology
export interface AccessTechnologyPaginatedModel
  extends PaginationResponse<AccessTechnologyModel> {}

// Search Model for AccessTechnology

export interface AccessTechnologySearchModel extends PaginationRequest {}
