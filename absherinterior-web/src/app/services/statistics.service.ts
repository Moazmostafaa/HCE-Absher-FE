import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../models/base.response";
import { LabelModel } from "../models/statistics/labels.model";
import { StatisticsEnum } from "../models/statistics/stats.enum";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class StatisticsService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  getNumberStatistics(statisticsEnum: StatisticsEnum, from: Date, to: Date) {
    let model = {
      fromDate:
        from == null ? null : this.datePipe.transform(from, "yyyy-MM-dd"),
      toDate: to == null ? null : this.datePipe.transform(to, "yyyy-MM-dd"),
    };
    switch (statisticsEnum) {
      case StatisticsEnum.TotalUsers:
        return this.http.post<BaseResponse<number>>(
          EndPoints.baseUrl + EndPoints.Stats.totalUsers,
          model
        );
      case StatisticsEnum.TotalActiveUsers:
        return this.http.post<BaseResponse<number>>(
          EndPoints.baseUrl + EndPoints.Stats.totalActiveUsers,
          model
        );
      case StatisticsEnum.TotalInactiveUsers:
        return this.http.post<BaseResponse<number>>(
          EndPoints.baseUrl + EndPoints.Stats.totalInactiveUsers,
          model
        );
      case StatisticsEnum.TotalPosts:
        return this.http.post<BaseResponse<number>>(
          EndPoints.baseUrl + EndPoints.Stats.totalPosts,
          model
        );
      case StatisticsEnum.TotalEvents:
        return this.http.post<BaseResponse<number>>(
          EndPoints.baseUrl + EndPoints.Stats.totalEvents,
          model
        );
      case StatisticsEnum.TotalChats:
        return this.http.post<BaseResponse<number>>(
          EndPoints.baseUrl + EndPoints.Stats.totalChats,
          model
        );
      case StatisticsEnum.TotalKnowledgeCenterDocuments:
        return this.http.post<BaseResponse<number>>(
          EndPoints.baseUrl + EndPoints.Stats.totalKnowledgeCenterDocuments,
          model
        );
    }
  }
  
  getPieStatistics(statisticsEnum: StatisticsEnum, from: Date, to: Date, number: number) {
    let model = {
      fromDate: from == null ? null : this.datePipe.transform(from, "yyyy-MM-dd"),
      toDate: to == null ? null : this.datePipe.transform(to, "yyyy-MM-dd"),
      number : number
    };
    switch (statisticsEnum) {
      case StatisticsEnum.AttendedEvents:
        return this.http.post<BaseResponse<LabelModel[]>>(
          EndPoints.baseUrl + EndPoints.Stats.attendedEvents,
          model
        );
      case StatisticsEnum.CommentedPosts:
        return this.http.post<BaseResponse<LabelModel[]>>(
          EndPoints.baseUrl + EndPoints.Stats.commentedPosts,
          model
        );
      case StatisticsEnum.LikedPosts:
        return this.http.post<BaseResponse<LabelModel[]>>(
          EndPoints.baseUrl + EndPoints.Stats.likedPosts,
          model
        );
      case StatisticsEnum.LikedKnowledgeCenterDocuments:
        return this.http.post<BaseResponse<LabelModel[]>>(
          EndPoints.baseUrl + EndPoints.Stats.likedKnowledgeCenterDocuments,
          model
        );
      case StatisticsEnum.PostOwners:
        return this.http.post<BaseResponse<LabelModel[]>>(
          EndPoints.baseUrl + EndPoints.Stats.postOwners,
          model
        );
      case StatisticsEnum.SharedPosts:
        return this.http.post<BaseResponse<LabelModel[]>>(
          EndPoints.baseUrl + EndPoints.Stats.sharedPosts,
          model
        );
      case StatisticsEnum.SharedKnowledgeCenterDocuments:
        return this.http.post<BaseResponse<LabelModel[]>>(
          EndPoints.baseUrl + EndPoints.Stats.sharedKnowledgeCenterDocuments,
          model
        );
    }
  }
  
}
