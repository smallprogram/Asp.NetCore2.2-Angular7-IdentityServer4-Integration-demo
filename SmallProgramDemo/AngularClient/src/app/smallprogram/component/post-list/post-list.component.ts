import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/post.service';
import { PostParameters } from '../../models/post-parameters';
import { PageMeta } from 'src/app/shared/models/page-meta';
import { ResultWithLinks } from 'src/app/shared/models/result-with-links';
import { Post } from '../../models/post';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {
  
  posts:Post[];
  
  pageMeta: PageMeta;
  postParameters = new PostParameters({ orderBy: 'id desc', pageSize: 10, pageIndex: 0 })

  constructor(private postService: PostService) { }

  ngOnInit() {
    this.getPosts();
  }

  getPosts() {
    this.postService.getPagedPosts(PostParameters).subscribe(response => {

      this.pageMeta = JSON.parse(response.headers.get('X-Pagination')) as PageMeta;
    
      let result = {...response.body} as ResultWithLinks<Post>;
      
      this.posts = result.values;
      
      console.log(this.pageMeta);
      console.log(response.body);
      console.log(this.posts);
    })
  }

}
