import { PaginationResponse } from "../pagination.response";
import { DocumentModel } from "./document.model";

export interface DocumentPaginatedModel
  extends PaginationResponse<DocumentModel> {}
