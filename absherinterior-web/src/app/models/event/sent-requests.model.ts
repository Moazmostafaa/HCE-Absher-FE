import { BasicUserModel } from "../User/basic-user.model";

export interface SentRequestsModel {
    requestId: string;
    creationDate: string;
    receiver: BasicUserModel;
  }