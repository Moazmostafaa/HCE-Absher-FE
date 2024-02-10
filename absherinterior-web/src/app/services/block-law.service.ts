import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../models/base.response";
import { BlockLawPaginatedModel } from "../models/block-law/block-law-paginated.model";
import { BlockLawModel } from "../models/block-law/block-law.model";
import { PagnationRequest } from "../models/pagination.request";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class BlockLawService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }
  searchBlockLaws(searchModel: PagnationRequest) {
    return this.http.post<BaseResponse<BlockLawPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.BlockLaw.list,
      searchModel
    );
  }
  deleteBlockLaw(id: string) {
    let model = { categoryId: id };
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.BlockLaw.delete + "/" + id
    );
  }
  addBlockLaw(model: any) {
    return this.http.post<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.BlockLaw.add,
      model
    );
  }
  updateBlockLaw(model: any) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.BlockLaw.edit,
      model
    );
  }
  getBlockLawById(id: string) {
    return this.http.get<BaseResponse<BlockLawModel>>(
      EndPoints.baseUrl + EndPoints.BlockLaw.getById + "/" + id
    );
  }
}
