import { PaginationResponse } from "../pagination.response";
import { BasicUserModel } from "./basic-user.model";
import { UserModel } from "./user.model";

export interface BasicUserPaginatedModel
  extends PaginationResponse<BasicUserModel> {}
