import { PaginationResponse } from "../pagination.response";
import { PostModel } from "./post.model";


export interface PostPaginatedModel extends PaginationResponse<PostModel> {
}