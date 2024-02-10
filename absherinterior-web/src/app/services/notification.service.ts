import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../models/base.response";
import { NotifcationPaginatedResponse } from "../models/notification/notification.model";
import { PagnationRequest } from "../models/pagination.request";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class NotificationService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }
  sendNotification(notification: any) {
    return this.http.post<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Notification.send,
      notification
    );
  }
  searchDocuments(request: PagnationRequest) {
    return this.http.post<BaseResponse<NotifcationPaginatedResponse>>(
      EndPoints.baseUrl + EndPoints.Notification.list,
      request
    );
  }
}
