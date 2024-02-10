import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FileService {

  constructor(private http: HttpClient) {}
  private filesBaseUrl = environment.filesBaseUrl;
  
  uploadFile(formImg): Observable<any> {
    return this.http.post(this.filesBaseUrl + 'api/File/Uploadfile', formImg);
  }
}
