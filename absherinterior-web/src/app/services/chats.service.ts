import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseResponse } from "../models/base.response";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class ChatService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  getUsersChats(model: any) {
    return this.http.post<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Chats.Individual,
      model
    );
  }
  getGroupsChats(model: any) {
    return this.http.post<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Chats.groups,
      model
    );
  }
  deleteChat(id: string) {
    return this.http.put(EndPoints.baseUrl + EndPoints.Chats.archive, {
      chatGroupId: id,
    });
  }
  getRoomMembers(model: any) {
    return this.http.post<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Chats.Members,
      model
    );
  }
  getRoomMessages(model: any) {
    return this.http.post<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Chats.Messages,
      model
    );
  }
}
