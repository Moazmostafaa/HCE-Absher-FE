import { PostTypesEnum } from "../../Enums/post-types.enum";
import { ShareTypeEnum } from "../document/document.model";
import { FileType } from "../file-type";
import { BasicUserModel } from "../User/basic-user.model";

export interface PostModel {
  postId: string;
  postType: PostTypesEnum;
  postText?: string;
  creationDate: string;
  sharePostId?: string;
  noOfValidDays: number;
  numberOfLikes: number;
  numberOfComments: number;
  userInfo: BasicUserModel;
  isPostOwner: boolean;
  shareType: ShareTypeEnum;
  shareKnowledgeCenterId?: string;
  postFiles?: PostFileModel[];
  poll?: PollModel;
}

export interface PostFileModel {
  attachmentId: string;
  fileType: FileType;
}
export interface PollModel {
  pollId: string;
  question: string;
  deadLineDays: number;
  votersNumber: number;
  answers: PollAnswerModel[];
}
export interface PollAnswerModel {
  pollAnswerId: string;
  answer: string;
  selected: boolean;
  numberOfVotes: number;
}
