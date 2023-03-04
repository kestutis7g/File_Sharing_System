import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { CommentService } from 'src/app/core/services/comment.service';
import { GroupService } from 'src/app/core/services/group.service';
import { GroupPostService } from 'src/app/core/services/groupPost.service';
import { GroupUserService } from 'src/app/core/services/groupUser.service';
import { PostService } from 'src/app/core/services/post.service';
import { UserService } from 'src/app/core/services/user.service';
import {
  Post,
  Comment,
  Group,
  GroupPost,
  GroupUser,
} from 'src/app/core/types/web.types';
import Swal from 'sweetalert2';
import { HomeComponent } from '../home/home.component';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.scss'],
})
export class GroupComponent implements OnInit {
  constructor(
    private groupService: GroupService,
    private postService: PostService,
    private groupPostService: GroupPostService,
    private groupUserService: GroupUserService,
    private commentService: CommentService,
    private userService: UserService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.signedIn = localStorage.getItem('userId') != undefined ?? false;
  }

  postForm: FormGroup = new FormGroup({
    title: new FormControl<string>('', [Validators.required]),
    content: new FormControl<string>('', [Validators.required]),
    type: new FormControl<string>('Text', [Validators.required]),
    userId: new FormControl<string>(localStorage.getItem('userId') || '', [
      Validators.required,
    ]),
    picture: new FormControl<string | undefined>(undefined, [
      Validators.required,
    ]),
    //group: new FormControl<Group | undefined>(undefined, [Validators.required]),
  });

  posts: Post[] = [];
  groupPosts: GroupPost[] = [];

  newComment: Comment = {
    postId: '',
    parentId: '',
    content: '',
    userId: '',
  } as Comment;

  signedIn: boolean;
  userId = '';
  userGroups: GroupUser[] = [];
  isAdmin = false;
  joined = false;

  showPosts = false;

  group: Group = {
    id: '',
    name: '',
    visibility: '',
    iconPicture: '',
    userId: '',
  };

  postEdit: Post = {
    title: '',
    content: '',
  };

  loading = true;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((params) => {
      this.groupService.getGroupById(params['id']).subscribe({
        next: (data) => {
          this.group = data;
          this.group.id = params['id'];
          this.userId = localStorage.getItem('userId') || '';
          this.refreshPosts();
          if (this.group.visibility == 'Public') {
            this.showPosts = true;
          }
        },
        error: (error) => {
          console.log(error);
        },
      });
    });
  }

  addPost() {
    const postForm = this.postForm.getRawValue();

    const post: Post = {
      title: postForm.title,
      content: postForm.content,
      type: postForm.type,
      userId: postForm.userId,
      picture: postForm.picture,
    };

    if (!this.validatePost(post)) {
      return;
    }

    console.log(post);

    if (post.type == 'Text') {
      post.picture = undefined;
    } else if (post.type == 'Photo') {
      post.content = '';
    }

    this.postService.addPost(post).subscribe({
      next: (data) => {
        console.log(data);

        this.postForm.reset();
        this.postForm.setValue({
          title: '',
          content: '',
          userId: this.userId,
          type: 'Text',
          picture: '',
        });

        const groupPost: GroupPost = {
          groupId: this.group.id!,
          postId: data.id ?? '',
        };
        this.groupPostService.addGroupPost(groupPost).subscribe({
          next: (data) => {
            this.refreshPosts();
            window.location.reload();
          },
          error: (error) => {
            console.log(error);
          },
        });
      },
      error: (error) => {
        Swal.fire({
          icon: 'error',
          title: error.status,
          text: 'Unable to create post',
        });
        console.log(error);
      },
    });
  }

  refreshPosts() {
    this.getUserGroups();
    this.groupPostService.getGroupPostsById(this.group.id!).subscribe({
      next: (data) => {
        this.groupPosts = data;
        this.loading = false;
        this.groupPosts.forEach((groupPost) => {
          this.postService.getPostById(groupPost.postId).subscribe({
            next: (p) => {
              const post = p;
              this.posts = [];

              this.postService.getCommentsByPostId(groupPost.postId).subscribe({
                next: (comments) => {
                  post.comments = comments;
                  post.newComment = '';

                  this.userService.getUserById(post.userId!).subscribe({
                    next: (user) => {
                      post.user = user;
                      this.posts.push(post);
                      this.updateCommentCreators();
                    },
                    error: (error) => {
                      console.log(error);
                    },
                  });
                },
                error: (error) => {
                  console.log(error);
                },
              });
            },
            error: (error) => {
              console.log(error);
            },
          });
        });
      },
      error: (error) => {
        console.log(error);
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
              this.refreshPosts();
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
        this.refreshPosts();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getUserGroups() {
    this.groupUserService.getGroupsByUserId(this.userId).subscribe({
      next: (data) => {
        this.userGroups = data;
        this.userGroups.forEach((userGroup) => {
          if (userGroup.groupId == this.group.id!) {
            this.joined = true;
            this.isAdmin = userGroup.isAdmin;
            this.showPosts = true;
          }
        });
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  joinGroup() {
    const groupUser: GroupUser = {
      userId: this.userId,
      groupId: this.group.id!,
      isAdmin: false,
    };
    this.groupUserService.addGroupUser(groupUser).subscribe({
      next: (data) => {
        Swal.fire({
          icon: 'success',
          title: 'You successfuly joined the group!',
          timer: 1000,
        });
        this.refreshPosts();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  leaveGroup() {
    this.joined = false;
    if (this.group.visibility == 'Private') {
      this.showPosts = false;
    }
    this.groupUserService
      .deleteGroupUser(this.group.id!, this.userId)
      .subscribe({
        next: (data) => {
          this.refreshPosts();
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  deleteComment(id: string, userId: string) {
    if (userId != this.userId) {
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
          text: 'Comment will be deleted permanently!',
          showDenyButton: true,
          showCancelButton: false,
          confirmButtonText: 'Delete',
          denyButtonText: `Cancel`,
        })
        .then((result) => {
          if (result.isConfirmed) {
            this.commentService.hardDeleteComment(id).subscribe({
              next: (data) => {
                this.refreshPosts();
              },
              error: (error) => {
                console.log(error);
              },
            });
          }
        });
    } else {
      this.commentService.hardDeleteComment(id).subscribe({
        next: (data) => {
          this.refreshPosts();
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }

  validatePost(post: Post) {
    if (post.title.replace(/\s/g, '').length == 0) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Please type a valid title name',
      });
      this.postForm.setValue({
        title: '',
        content: post.content,
        userId: this.userId,
        type: post.type,
        picture: post.picture,
      });
      return false;
    }

    if (post.type != 'Photo' && post.content.replace(/\s/g, '').length == 0) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Please type a valid post content',
      });
      this.postForm.setValue({
        title: post.title,
        content: '',
        userId: this.userId,
        type: post.type,
        picture: post.picture,
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
      this.postForm.setValue({
        title: post.title,
        content: '',
        userId: this.userId,
        type: post.type,
        picture: post.picture,
      });
      return false;
    }

    if (post.type == 'Photo') {
      this.postForm.setValue({
        title: post.title,
        content: 'text',
        userId: this.userId,
        type: post.type,
        picture: post.picture,
      });
      if (!this.postForm.valid) {
        Swal.fire({
          icon: 'error',
          title: '',
          text: 'Image URL is not valid',
        });
        return false;
      }
    } else if (post.type == 'PhotoWithText') {
      if (!this.postForm.valid) {
        Swal.fire({
          icon: 'error',
          title: '',
          text: 'Image URL is not valid',
        });
        return false;
      }
    }

    return true;
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
    if (!this.validateEditPost(this.postEdit)) {
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
        this.refreshPosts();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  validateEditPost(post: Post) {
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

  deleteGroup() {
    if (this.group.id == undefined) {
      return;
    }
    this.groupService.hardDeleteGroup(this.group.id).subscribe({
      next: (data) => {
        this.router.navigate(['/home']).then(() => {
          window.location.reload();
        });
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
}
