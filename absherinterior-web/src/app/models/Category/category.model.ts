import { FileType } from "../file-type";
import { BasicUserModel } from "../User/basic-user.model";

export interface CategoryModel {
  categoryId: string;
  name: string;
  description: string;
  categoryType: CategoryTypeEnum;
  isCategoryOwner: boolean;
  creationDate: Date;
  attachmentId?: string;
  fileType?: FileType;
  parentCategoryId?: string;
  userInfo: BasicUserModel;
}
export enum CategoryTypeEnum {
  Event = 1,
  KnowledgeCenter = 2,
}
