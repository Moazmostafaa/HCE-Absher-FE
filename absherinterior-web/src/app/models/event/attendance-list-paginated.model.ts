import { PaginationResponse } from "../pagination.response";
import { AttendanceListModel } from "./attendance-list.model";

export interface AttendanceListPaginatedModel
  extends PaginationResponse<AttendanceListModel> {}
