import { PagnationRequest } from "../pagination.request";
import { EventStatusEnum } from "./event.model";

export interface EventSearchModel extends PagnationRequest {
    searchBy: EventSearchCriteria;
}
export interface EventSearchCriteria {
    categId?: string;
    fromDate?: string;
    toDate?: string;
    eventStatus?: EventStatusEnum;
}
