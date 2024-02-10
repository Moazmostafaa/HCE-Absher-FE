import { PaginationResponse } from "../pagination.response";
import { EventModel } from "./event.model";

export interface EventPaginatedModel extends PaginationResponse<EventModel> {}
