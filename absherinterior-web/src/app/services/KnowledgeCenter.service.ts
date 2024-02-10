import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResponse } from '../models/base.response';
import { AddDocumentModel } from '../models/document/add-document.model';
import { DocumentPaginatedModel } from '../models/document/document-paginated.model';
import { DocumentSearchModel } from '../models/document/document-search.model';
import { DocumentModel } from '../models/document/document.model';
import { BaseService } from './base.service';
import { EndPoints } from './EndPoints';

@Injectable({
  providedIn: 'root'
})
export class KnowledgeCenterService extends BaseService {
  constructor(private http: HttpClient, private datePipe: DatePipe) {
    super(datePipe);
  }
  searchDocuments(searchModel: DocumentSearchModel) {
    return this.http.post<BaseResponse<DocumentPaginatedModel>>(
      EndPoints.baseUrl + EndPoints.KnowledgeCenter.searchDocuments,
      searchModel
    );
  }
  addDocument(model: AddDocumentModel) {
    return this.http.post<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.KnowledgeCenter.addDocument,
      model
    );
  }
  editDocument(model: any) {
    return this.http.put<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.KnowledgeCenter.editDocument,
      model
    );
  }
  deleteDocument(id: string) {
    return this.http.delete<BaseResponse<boolean>>(
      EndPoints.baseUrl + EndPoints.KnowledgeCenter.deleteDocument + "/" + id);
  }
  getDocument(id: string) {
    return this.http.get<BaseResponse<DocumentModel>>(
      EndPoints.baseUrl + EndPoints.KnowledgeCenter.getDocumentDetails + "/" + id);
  }
}
