import { Component, OnInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { CommentService } from 'src/app/core/services/comment.service';
import { GroupService } from 'src/app/core/services/group.service';
import { GroupPostService } from 'src/app/core/services/groupPost.service';
import { GroupUserService } from 'src/app/core/services/groupUser.service';
import { PostService } from 'src/app/core/services/post.service';
import { UserService } from 'src/app/core/services/user.service';
import {
  Post,
  Comment,
  GroupUser,
  Group,
  GroupPost,
} from 'src/app/core/types/web.types';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(
    private groupUserService: GroupUserService,
    private groupService: GroupService,
    private groupPostService: GroupPostService,
    private postService: PostService,
    private commentService: CommentService,
    private userService: UserService,
    private router: Router
  ) {
    this.signedIn = localStorage.getItem('userId') != undefined ?? false;
  }

  posts: Post[] = [];
  tempPosts: Post[] = [];

  newComment: Comment = {
    postId: '',
    parentId: '',
    content: '',
    userId: '',
  } as Comment;

  signedIn: boolean;
  userId = '';
  userGroups: GroupUser[] = [];
  groups: Group[] = [];
  groupPosts: GroupPost[] = [];

  postEdit: Post = {
    title: '',
    content: '',
  };
  loading = true;

  ngOnInit(): void {
    this.userId = localStorage.getItem('userId') || '';
    this.getUserPosts();
  }

  refreshPosts() {
    this.postService.getPostList().subscribe({
      next: (data) => {
        this.posts = data;

        for (let i = 0; i < this.posts.length; i++) {
          this.postService.getCommentsByPostId(this.posts[i].id!).subscribe({
            next: (comments) => {
              this.posts[i].comments = comments;
              this.posts[i].newComment = '';
            },
            error: (error) => {
              console.log(error);
            },
          });
        }
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getUserPosts() {
    this.tempPosts = [];

    this.groupUserService.getGroupsByUserId(this.userId).subscribe({
      next: (data) => {
        this.userGroups = data;
        this.loading = false;
        this.userGroups.forEach((userGroup) => {
          this.groupService.getGroupById(userGroup.groupId).subscribe({
            next: (group) => {
              this.groups.push(group);

              this.groupPostService.getGroupPostsById(group.id!).subscribe({
                next: (groupPosts) => {
                  this.groupPosts = groupPosts;

                  this.groupPosts.forEach((groupPost) => {
                    this.postService.getPostById(groupPost.postId).subscribe({
                      next: (p) => {
                        const post = p;
                        post.group = group;

                        this.postService
                          .getCommentsByPostId(groupPost.postId)
                          .subscribe({
                            next: (comments) => {
                              post.comments = comments;
                              post.newComment = '';

                              this.userService
                                .getUserById(post.userId!)
                                .subscribe({
                                  next: (user) => {
                                    post.user = user;
                                    this.tempPosts.push(post);
                                    this.updateCommentCreators();
                                    this.loading = false;
                                  },
                                  error: (error) => {
                                    console.log(error);
                                    this.loading = false;
                                  },
                                });
                              this.posts = this.tempPosts;
                            },
                            error: (error) => {
                              console.log(error);
                              this.loading = false;
                            },
                          });
                      },
                      error: (error) => {
                        console.log(error);
                        this.loading = false;
                      },
                    });
                  });
                },
                error: (error) => {
                  console.log(error);
                  this.loading = false;
                },
              });
            },
            error: (error) => {
              console.log(error);
              this.loading = false;
            },
          });
        });
      },
      error: (error) => {
        console.log(error);
        this.loading = false;
      },
    });
  }

  updateCommentCreators() {
    for (let i = 0; i < this.posts.length; i++) {
      for (let c = 0; c < this.posts[i].comments!.length; c++) {
        this.userService
          .getUserById(this.posts[i].comments![c].userId!)
          .subscribe({
            next: (user) => {
              this.posts[i].comments![c].user = user;
            },
            error: (error) => {
              console.log(error);
            },
          });
      }
    }
  }

  deletePost(postId: string) {
    const swalWithBootstrapButtons = Swal.mixin({
      customClass: {
        confirmButton: 'btn btn-danger btn-lg m-2',
        denyButton: 'btn btn-warning btn-lg m-2',
      },
      buttonsStyling: false,
    });

    swalWithBootstrapButtons
      .fire({
        icon: 'warning',
        title: 'Are you sure?',
        text: 'Post will be deleted permanently!',
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: 'Delete',
        denyButtonText: `Cancel`,
      })
      .then((result) => {
        if (result.isConfirmed) {
          this.postService.hardDeletePost(postId).subscribe({
            next: (data) => {
              Swal.fire({
                icon: 'success',
                title: 'Post was deleted!',
                timer: 1000,
              });
              this.getUserPosts();
            },
            error: (error) => {
              console.log(error);
            },
          });
        }
      });
  }

  addComment(postId: string, newComment: string) {
    this.newComment.postId = postId;
    this.newComment.userId = localStorage.getItem('userId')!;
    this.newComment.content = newComment;

    this.commentService.addComment(this.newComment).subscribe({
      next: (data) => {
        this.getUserPosts();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  deleteComment(id: string) {
    console.log(id);

    this.commentService.hardDeleteComment(id).subscribe({
      next: (data) => {
        this.getUserPosts();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  selectPostToEdit(id: string) {
    this.postService.getPostById(id).subscribe({
      next: (data) => {
        this.postEdit = data;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  editPost() {
    if (!this.validatePost(this.postEdit)) {
      return;
    }
    console.log(this.postEdit);

    this.postService.updatePost(this.postEdit).subscribe({
      next: (data) => {
        Swal.fire({
          icon: 'success',
          title: 'Post was updated!',
          timer: 1000,
        });
        this.getUserPosts();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  validatePost(post: Post) {
    if (post.title.replace(/\s/g, '').length == 0) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Please type a valid title name',
      });
      return false;
    }

    if (post.type != 'Photo' && post.content.replace(/\s/g, '').length == 0) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Please type a valid post content',
      });
      return false;
    }

    if (
      (post.type != 'Text' && post.picture == undefined) ||
      (post.type != 'Text' && post.picture!.replace(/\s/g, '').length == 0)
    ) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Image URL is empty',
      });
      return false;
    }

    return true;
  }
}
