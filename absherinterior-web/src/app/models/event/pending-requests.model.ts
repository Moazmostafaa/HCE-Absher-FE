import { BasicUserModel } from "../User/basic-user.model";

export interface PendingRequestsModel {
  requestId: string;
  creationDate: string;
  sender: BasicUserModel;
  replyStatus: ReplyStatusEnum;
  replyDate?: string;
}

export enum ReplyStatusEnum {
  Pending = 1,
  Accepted = 2,
  Rejected = 3,
}
