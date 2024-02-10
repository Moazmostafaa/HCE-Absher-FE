import { PaginationResponse } from "../pagination.response";
import { SentRequestsModel } from "./sent-requests.model";

export interface SentRequestsPaginatedModel
  extends PaginationResponse<SentRequestsModel> {}
