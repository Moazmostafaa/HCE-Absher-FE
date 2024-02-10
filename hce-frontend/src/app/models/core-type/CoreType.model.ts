import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Basic model for CoreType

export interface CoreTypeBasicModel{
  npskpiWeightName: string,
  npskpiWeightDesc: string,
}

// Create Model for CoreType
export interface CoreTypeCreateModel extends CoreTypeBasicModel {
}

// Update Model for CoreType
export interface CoreTypeUpdateModel extends CoreTypeBasicModel {
  npskpiWeightId:   string,
}

// Details Model for CoreType
export interface CoreTypeModel extends CoreTypeBasicModel {
  npskpiWeightId:   string,
  creationDate:     string,
  createdBy:        string
}


// Paginated Model for CoreType
export interface CoreTypePaginatedModel
  extends PaginationResponse<CoreTypeModel> {}

  // Search Model for CoreType
export interface CoreTypeSearchModel extends PaginationRequest {}
