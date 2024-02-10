import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { environment } from "../../environments/environment";
import { AcceptedFileTypes } from "./accepted-file-types";

@Injectable({
  providedIn: "root",
})
export class FileService {
  constructor(private http: HttpClient) {}
  private filesBaseUrl = environment.filesBaseUrl;

  uploadFile(formImg): Observable<any> {
    return this.http.post(this.filesBaseUrl + "Documents/UploadFile", formImg);
  }
  uploadImage(file, moduleId: string): Observable<any> {
    var fileTypes = [".jpg", ".jpeg", ".png", ".gif"]; //acceptable file types
    let extension = "." + file.name.split(".").pop();
    let formData = new FormData();
    formData.append("File", file);
    formData.append("ModuleId", moduleId);

    if (!fileTypes.some((c) => c == extension)) {
      return throwError("Invalid file type.");
    } else {
      return this.http.post(
        this.filesBaseUrl + "Documents/UploadFile",
        formData
      );
    }
  }
  uploadVideo(file, moduleId: string): Observable<any> {
    var fileTypes = [".mp4", ".mov", ".avi", ".3gp"]; //acceptable file types
    let extension = "." + file.name.split(".").pop();
    let formData = new FormData();
    formData.append("File", file);
    formData.append("ModuleId", moduleId);

    if (!fileTypes.some((c) => c == extension)) {
      return throwError("Invalid file type.");
    } else {
      return this.http.post(
        this.filesBaseUrl + "Documents/UploadFile",
        formData
      );
    }
  }
  uploadVariousfile(file, moduleId: string): Observable<any> {
    var fileTypes = AcceptedFileTypes; //acceptable file types
    let extension = "." + file.name.split(".").pop();
    let formData = new FormData();
    formData.append("File", file);
    formData.append("ModuleId", moduleId);

    if (!fileTypes.some((c) => c == extension)) {
      return throwError("Invalid file type.");
    } else {
      return this.http.post(
        this.filesBaseUrl + "Documents/UploadFile",
        formData
      );
    }
  }
  downloadFile(fileId: string): Observable<any> {
    return this.http.get(
      this.filesBaseUrl + "Documents/DownloadFileById/" + fileId
    );
  }
  deleteFile(fileId): Observable<any> {
    return this.http.delete(
      this.filesBaseUrl + `Documents/DeleteFileById/${fileId}`
    );
  }
}
