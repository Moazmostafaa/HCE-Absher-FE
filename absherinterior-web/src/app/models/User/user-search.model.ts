import { PagnationRequest } from "../pagination.request";

export interface UserSearchModel extends PagnationRequest {}

export interface AutoCompleteSearchModel extends PagnationRequest {
    query: string;
}

