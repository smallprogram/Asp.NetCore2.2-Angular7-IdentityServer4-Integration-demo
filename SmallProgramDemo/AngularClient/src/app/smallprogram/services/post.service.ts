import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PostParameters } from '../models/post-parameters';

@Injectable({
  providedIn: 'root'
})
export class PostService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getPagedPosts(postParameter?: any | PostParameters) {
    return this.http.get(`${this.apiUslBase}/posts`,{
      headers:new HttpHeaders({
        'Accept':'application/vnd.smallprogram.hateoas+json'
      }),
      observe:'response', //获取完整的响应
      params:postParameter
    });
  }
}
