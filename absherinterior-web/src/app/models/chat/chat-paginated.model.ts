import { ChatModule } from "../../pages/chat/chat.module";

import { PaginationResponse } from "../pagination.response";
import { ChatModel } from "./chat.model";

export interface ChatPaginatedModel extends PaginationResponse<ChatModel> {}
