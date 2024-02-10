import { PaginationResponse } from "../pagination.response";
import { EventModel } from "./event.model";
import { PendingRequestsModel } from "./pending-requests.model";

export interface PendingRequestsPaginatedModel
  extends PaginationResponse<PendingRequestsModel> {}
