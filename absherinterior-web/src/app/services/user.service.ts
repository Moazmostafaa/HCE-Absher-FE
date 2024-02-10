import { DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { BaseResponse } from "../models/base.response";
import { BasicUserModel } from "../models/User/basic-user.model";
import { EditUserModel } from "../models/User/edit-user.model";
import { AddUserModel } from "../models/User/register-user.model";
import { BasicUserPaginatedModel } from "../models/User/user-paginated.model";
import { UserProfileModel } from "../models/User/user-profile.model";
import {
  AutoCompleteSearchModel,
  UserSearchModel,
} from "../models/User/user-search.model";
import { BaseService } from "./base.service";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class UserService extends BaseService {
  protected userState$ = new BehaviorSubject({});
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }

  addUser(model: any) {
    return this.http.post<BaseResponse<string>>(
      EndPoints.baseUrl + EndPoints.User.add,
      model
    );
  }
  getBlockLaws() {
    return this.http.post<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.User.laws,
      {
        pageSize: 100,
        pageNumber: 1,
      }
    );
  }
  editUser(model: any) {
    return this.http.put<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.User.edit,
      model
    );
  }
  editProfile(model) {
    return this.http.post<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.User.editProfile,
      model
    );
  }
  userActivation(queryParams) {
    return this.http.put<BaseResponse<string>>(
      EndPoints.baseUrl + EndPoints.User.activate,
      queryParams
    );
  }
  userDeActivation(queryParams) {
    return this.http.put<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.User.deactivate,
      queryParams
    );
  }
  setUserState(user: any): any {
    this.userState$.next(user);
  }

  getUserState(): Observable<any> {
    return this.userState$.asObservable();
  }
  verifyPhone(phoneData): Observable<any> {
    return this.http.put(
      EndPoints.baseUrl +
        EndPoints.User.verifyPhone +
        `?Phone=' + ${phoneData}`,
      {}
    );
  }
  verifyEmail(emailData): Observable<any> {
    return this.http.put(
      EndPoints.baseUrl + EndPoints.User.verifyEmail + "?Email=" + emailData,
      {}
    );
  }
  searchUser(searchModel: UserSearchModel) {
    return this.http.post<any>(
      EndPoints.baseUrl + EndPoints.User.list,
      searchModel
    );
  }
  getUserProfile(): Observable<BaseResponse<UserProfileModel>> {
    return this.http.get<BaseResponse<UserProfileModel>>(
      EndPoints.baseUrl + EndPoints.User.Profile
    );
  }
  autoCompletesearchUser(model: AutoCompleteSearchModel) {
    return this.http.post<BaseResponse<BasicUserPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.User.autoComplete,
      model
    );
  }
  deleteUser(id: string) {
    return this.http.put(EndPoints.baseUrl + EndPoints.User.archive, {
      userId: id,
    });
  }
  getUserById(id: string) {
    return this.http.post<any>(EndPoints.baseUrl + EndPoints.User.getById, {
      userId: id,
    });
  }
  getProfileData() {
    return this.http.get<any>(EndPoints.baseUrl + EndPoints.User.Profile);
  }
}
