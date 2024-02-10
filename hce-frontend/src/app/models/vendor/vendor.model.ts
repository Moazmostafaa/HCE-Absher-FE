import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Basic model for Vendor
export interface VendorBasicModel {
    vendorName: string;
    vendorDesc: string;
}

// Create Model for Vendor
export interface VendorCreateModel
  extends VendorBasicModel {}

// Update Model for Vendor
export interface VendorUpdateModel
  extends VendorBasicModel {
    vendorId: string;
}

// Details Model for Vendor
export interface VendorModel
  extends VendorBasicModel {
  vendorId: string;
  creationDate: any;
  createdBy: string;
}

// Paginated Model for Vendor
export interface VendorPaginatedModel
  extends PaginationResponse<VendorModel> {}

// Search Model for Vendor

export interface VendorSearchModel extends PaginationRequest {}
