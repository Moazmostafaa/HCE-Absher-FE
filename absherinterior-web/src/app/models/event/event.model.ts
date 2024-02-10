import { BasicUserModel } from "../User/basic-user.model";

export interface EventModel {
  eventId: string;
  eventName: string;
  desc: string;
  userInfo: BasicUserModel;
  creationDate: Date;
  regStartDate?: any;
  regEndDate?: any;
  startDate: Date;
  endDate: Date;
  eventAgenda: string;
  eventLocation: string;
  eventType: EventTypeEnum;
  eventCategoryId: string;
  hasLimtedSeats: boolean;
  numberOfSeats: number;
  eventDuration: number;
  isEventOwner: boolean;
  status: EventStatusEnum;
  remainingDaysForRegistration?: any;
  numberOfRegisteredUsers: number;
  numberOfInvitedUsers: number;
  privacyType: EventPrivacyTypeEnum;
  eventFiles: EventFile[];
}
export interface EventFile {
  attachmentId: string;
  fileType: number;
}
export enum EventStatusEnum {
  NotStarted = 1,
  Started = 2,
  Finished = 3
}
export enum EventTypeEnum {
  Physical = 1,
  Online = 2
}
export enum EventPrivacyTypeEnum {
  Public = 1,
  Private = 2
}