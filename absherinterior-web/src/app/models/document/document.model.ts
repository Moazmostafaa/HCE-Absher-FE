import { BasicUserModel } from "../User/basic-user.model";

export interface DocumentModel {
  docId: string;
  name: string;
  description: string;
  link: string;
  userInfo: BasicUserModel;
  creationDate: string;
  categoryId: string;
  attachmentId: string;
  fileType: FileTypeEnum;
  shareKnowledgeCenterId: string;
  numberOfLikes: number;
  isKnowledgeCenterOwner: boolean;
  shareType: ShareTypeEnum;
}

export enum FileTypeEnum {
  Photo = 1,
  Video = 2,
  Pdf = 3,
  PowerPoint = 4,
}

export enum ShareTypeEnum {
  Message = 1,
  Post = 2,
}
