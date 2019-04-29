import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PostParameters } from '../models/post-parameters';
import { Post } from '../models/post';
import { PostAdd } from '../models/post-add';

@Injectable({
  providedIn: 'root'
})
export class PostService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getPagedPosts(postParameter?: any | PostParameters) {
    return this.http.get(`${this.apiUslBase}/posts`, {
      headers: new HttpHeaders({
        'Accept': 'application/vnd.smallprogram.hateoas+json'
      }),
      observe: 'response', //获取完整的响应
      params: postParameter
    });
  }

  addPost(post: PostAdd) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/vnd.smallprogram.post.create+json',
        'Accept': 'application/vnd.smallprogram.hateoas+json'
      })
    }
    return this.http.post<Post>(`${this.apiUslBase}/posts`, post, httpOptions);
  }
}
