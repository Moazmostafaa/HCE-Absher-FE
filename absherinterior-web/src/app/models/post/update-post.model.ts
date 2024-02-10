import { PostTypesEnum } from "../../Enums/post-types.enum";
import { ShareTypeEnum } from "../document/document.model";

export interface UpdatePostModel {
    postId: string;
    postText: string;
    postType: PostTypesEnum;
    shareType: ShareTypeEnum;
    noOfValidDays?: number;
    postFiles: string[];
    pollPost?: UpdatePollPostModel;
}

export interface UpdatePollPostModel {
    pollId?: string;
    question: string;
    deadLineDays?: number;
    answers: UpdatePollAnswerModel[];
}
export interface UpdatePollAnswerModel {
    answerId?: string;
    answer: string;
}