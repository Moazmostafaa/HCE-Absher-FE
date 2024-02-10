import { BasicUserModel } from "../User/basic-user.model";

export interface AttendanceListModel {
    attendanceID:string;
    creationDate: string;
    userInfo: BasicUserModel;
}