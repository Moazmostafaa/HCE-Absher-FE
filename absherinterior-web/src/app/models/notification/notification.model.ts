import { PaginationResponse } from "../pagination.response";

export interface NotificationModel {
  notificationId: string;
  message: string;
  title: string;
  isSentByAdmin: boolean;
  creationDate: Date;
  isBulk: boolean;
}
export interface NotifcationPaginatedResponse
  extends PaginationResponse<NotificationModel> {}
