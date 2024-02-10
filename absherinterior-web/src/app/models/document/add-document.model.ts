import { ShareTypeEnum } from "./document.model";

export interface AddDocumentModel {
  name: string;
  description: string;
  link: string;
  attachmentId: string;
  categoryId: string;
  shareType: ShareTypeEnum;
}
