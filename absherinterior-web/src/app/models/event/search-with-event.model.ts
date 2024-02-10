import { PagnationRequest } from "../pagination.request";

export interface SearchWithEventIdModel extends PagnationRequest {
  eventId: string;
  status: number;
}

export enum ReplyStatus {
  Pending = 1,
  Accepted = 2,
  Rejected = 3,
}
