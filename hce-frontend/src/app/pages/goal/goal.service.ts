import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../../models/base.response";
import { GoalCreateModel, GoalModel, GoalPaginatedModel, GoalSearchModel, GoalUpdateModel } from "../../models/goal/goal.model";
import { BaseService } from "../../services/base.service";
import { EndPoints } from "../../services/EndPoints";

@Injectable({
  providedIn: "root",
})
export class GoalService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  search(searchModel: GoalSearchModel) {
    return this.http.post<BaseResponse<GoalPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Goal.list,
      searchModel
    );
  }
  delete(id: string) {
    let model = { goalid: id };
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Goal.delete + '/' + id, 
    );
  }
  create(model: GoalCreateModel) {
    return this.http.post<BaseResponse<GoalModel>>(
      EndPoints.baseUrl + EndPoints.Goal.create,
      model
    );
  }
  update(model: GoalUpdateModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Goal.update,
      model
    );
  }
  getById(id: string) {
    return this.http.get<BaseResponse<GoalModel>>(
      EndPoints.baseUrl + EndPoints.Goal.get + '/' + id);
  }
}
