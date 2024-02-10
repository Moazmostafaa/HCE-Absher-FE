import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../models/base.response";
import { CategoryModel } from "../models/category/category.model";
import { AttendanceListPaginatedModel } from "../models/event/attendance-list-paginated.model";
import { EditEventModel } from "../models/event/edit-event.model";
import { EventPaginatedModel } from "../models/event/event-paginated.model";
import { EventSearchModel } from "../models/event/event-search.model";
import { EventModel } from "../models/event/event.model";
import { SearchWithEventIdModel } from "../models/event/search-with-event.model";
import { PendingRequestsPaginatedModel } from "../models/event/pending-requests-paginated.model";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";
import { SentRequestsPaginatedModel } from "../models/event/sent-requests-paginated.model";
import { BasicUserPaginatedModel } from "../models/User/user-paginated.model";

@Injectable({
  providedIn: "root",
})
export class EventService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }
  searchEvents(searchModel: EventSearchModel) {
    return this.http.post<BaseResponse<EventPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Event.list,
      searchModel
    );
  }
  getPendingRequests(pendingRequestsSearchModel: SearchWithEventIdModel) {
    return this.http.post<BaseResponse<PendingRequestsPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Event.pendingRequests,
      pendingRequestsSearchModel
    );
  }
  getSentRequests(model: SearchWithEventIdModel) {
    return this.http.post<BaseResponse<SentRequestsPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Event.sentRequests,
      model
    );
  }
  getAcceptedRequestToAttend(model: SearchWithEventIdModel) {
    return this.http.post<BaseResponse<BasicUserPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Event.acceptedRequests,
      model
    );
  }
  getAttendanceList(searchModel: SearchWithEventIdModel) {
    return this.http.post<BaseResponse<AttendanceListPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Event.attendanceList,
      searchModel
    );
  }
  acceptRequest(userId: string, eventId: string) {
    let model = {
      eventId: eventId,
      userId: userId,
      isAccepted: true,
    };
    return this.http.put<BaseResponse<PendingRequestsPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Event.replyToInvitationRequest,
      model
    );
  }
  rejectRequest(userId: string, eventId: string) {
    let model = {
      eventId: eventId,
      userId: userId,
      isAccepted: false,
    };
    return this.http.put<BaseResponse<PendingRequestsPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.Event.replyToInvitationRequest,
      model
    );
  }
  deleteEvent(id: string) {
    let model = { eventId: id };
    return this.http.request<BaseResponse<boolean>>(
      "DELETE",
      EndPoints.baseUrl + EndPoints.Event.delete,
      {
        body: model,
      }
    );
  }
  addEvent(model: any) {
    return this.http.post<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Event.add,
      model
    );
  }
  editEvent(model: EditEventModel) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Event.edit,
      model
    );
  }
  updateEvent(model: any) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Event.edit,
      model
    );
  }
  cancelEvent(id: string) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Event.cancel,
      {
        eventId: id,
      }
    );
  }
  postponeEvent(model: any) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Event.postpone,
     model
    );
  }
  sendInvitations(model: any) {
    return this.http.post<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.Event.sendInvitations,
      model
    );
  }
  getEventById(id: string) {
    return this.http.get<BaseResponse<EventModel>>(
      EndPoints.baseUrl + EndPoints.Event.getById + `?EventId=${id}`
    );
  }
  getEventCategoryById(id: string) {
    return this.http.get<BaseResponse<CategoryModel>>(
      EndPoints.baseUrl +
        EndPoints.Event.getEventCategoryById +
        `?CategoryId=${id}`
    );
  }
}
