import { CategoryModule } from "../../pages/category/category.module";

import { PaginationResponse } from "../pagination.response";
import { CategoryModel } from "./category.model";

export interface CategoryPaginatedModel
  extends PaginationResponse<CategoryModel> {}
