import { EventPrivacyTypeEnum, EventTypeEnum } from "./event.model";

export interface EditEventModel {
  eventId: string;
  eventName: string;
  desc: string;
  eventLocation: string;
  regStartDate: string;
  regEndDate: string;
  startDate: string;
  endDate: string;
  eventAgenda: string;
  eventType: EventTypeEnum;
  eventCategoryId: string;
  hasLimtedSeats: boolean;
  numberOfSeats: number;
  eventDuration: number;
  privacyType: EventPrivacyTypeEnum;
  eventFiles: string[];
}
