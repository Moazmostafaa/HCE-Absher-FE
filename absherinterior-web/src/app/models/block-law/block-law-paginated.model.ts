import { PaginationResponse } from "../pagination.response";
import { BlockLawModel } from "./block-law.model";

export interface BlockLawPaginatedModel
  extends PaginationResponse<BlockLawModel> {}
