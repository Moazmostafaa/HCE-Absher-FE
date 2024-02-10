import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseResponse } from "../models/base.response";
import { EndPoints } from "./EndPoints";

@Injectable({
  providedIn: "root",
})
export class PostService {
  constructor(private http: HttpClient) {}

  getPosts(model: any) {
    return this.http.get<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Posts.list,
      { params: model }
    );
  }
  deletePost(id: string) {
    return this.http.put(EndPoints.baseUrl + EndPoints.Posts.archive, {
      postId: id,
    });
  }
  deleteComment(id: string) {
    return this.http.put(EndPoints.baseUrl + EndPoints.Posts.DeleteComment, {
      commentId: id,
    });
  }
  addPost(model: any) {
    return this.http.post<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Posts.add,
      model
    );
  }
  updatePost(model: any) {
    return this.http.post<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Posts.update,
      model
    );
  }
  getPostById(id: string) {
    return this.http.post<any>(EndPoints.baseUrl + EndPoints.Posts.getById, {
      postId: id,
    });
  }
  getCommentsByPostById(model: any) {
    return this.http.get<BaseResponse<any>>(
      EndPoints.baseUrl + EndPoints.Posts.Comments, {params: model});
  }
}
