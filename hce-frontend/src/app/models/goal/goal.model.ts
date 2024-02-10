import { PaginationRequest } from "../pagination.request";
import { PaginationResponse } from "../pagination.response";

// Basic model for Goal
export interface GoalBasicModel {
  goalName: string;
  goalDesc: string;
}

// Create Model for Goal
export interface GoalCreateModel
  extends GoalBasicModel {}

// Update Model for Goal
export interface GoalUpdateModel
  extends GoalBasicModel {
  goalId: string;
}

// Details Model for Goal
export interface GoalModel
  extends GoalBasicModel {
  goalId: string;
  creationDate: any;
  createdBy: string;
}

// Paginated Model for Goal
export interface GoalPaginatedModel
  extends PaginationResponse<GoalModel> {}

// Search Model for Goal

export interface GoalSearchModel extends PaginationRequest {}
