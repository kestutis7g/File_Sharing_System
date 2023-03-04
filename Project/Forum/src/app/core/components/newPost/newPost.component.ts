import { Component, Input, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { PostService } from 'src/app/core/services/post.service';
import { GroupService } from 'src/app/core/services/group.service';
import {
  Group,
  GroupPost,
  GroupUser,
  Post,
} from 'src/app/core/types/web.types';
import { GroupPostService } from 'src/app/core/services/groupPost.service';
import { HomeComponent } from '../../../pages/home/home.component';
import { GroupUserService } from '../../services/groupUser.service';
import { FilterableDataSource } from 'angular-helper-utils';

@Component({
  selector: 'newPost',
  templateUrl: './newPost.component.html',
  styleUrls: ['./newPost.component.scss'],
})
export class NewPostComponent implements OnInit {
  constructor(
    private groupUserService: GroupUserService,
    private postService: PostService,
    private groupService: GroupService,
    private groupPostService: GroupPostService,
    private router: Router,
    private homeCompoent: HomeComponent,
    private fb: FormBuilder
  ) {}

  urlRegex =
    /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/;

  postForm: FormGroup = new FormGroup({
    title: new FormControl<string>('', [Validators.required]),
    content: new FormControl<string>('', [Validators.required]),
    type: new FormControl<string>('Text', [Validators.required]),
    userId: new FormControl<string>(localStorage.getItem('userId') || '', [
      Validators.required,
    ]),
    picture: new FormControl<string | undefined>(undefined, [
      Validators.required,
      Validators.pattern(this.urlRegex),
    ]),
    group: new FormControl<Group | undefined>(undefined, [Validators.required]),
  });

  groupList: Group[] = [];
  groupListTemp = new FilterableDataSource<Group>();
  userGroups: GroupUser[] = [];

  @Input() showGroupSelect: boolean = false;

  ngOnInit() {
    if (!localStorage.getItem('userId')) {
      this.router.navigate(['/login']).then(() => {});
    }
    this.postForm.value.userId = localStorage.getItem('userId');

    this.groupUserService
      .getGroupsByUserId(localStorage.getItem('userId')!)
      .subscribe({
        next: (userGroups) => {
          this.userGroups = userGroups;

          this.userGroups.forEach((group) => {
            this.groupService.getGroupById(group.groupId).subscribe({
              next: (group) => {
                this.groupList.push(group);
                this.groupList = [...this.groupList];
                this.groupListTemp.set(this.groupList);
                console.log(this.groupList);
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

    // this.groupService.getGroupList().subscribe({
    //   next: (groups) => {
    //     this.groupList = groups;
    //     console.log(this.groupList);
    //   },
    //   error: (error) => {
    //     console.log(error);
    //   },
    // });
  }

  addPost() {
    const postForm = this.postForm.getRawValue();
    const post: Post = {
      title: postForm.title,
      content: postForm.content,
      type: postForm.type,
      userId: postForm.userId,
      picture: postForm.picture,
      group: postForm.group,
    };
    if (!this.validatePost(post)) {
      return;
    }

    if (post.type == 'Text') {
      post.picture = undefined;
    } else if (post.type == 'Photo') {
      post.content = '';
    }

    this.postService.addPost(post).subscribe({
      next: (data) => {
        const groupPost: GroupPost = {
          groupId: postForm.group.id,
          postId: data.id ?? '',
        };
        this.groupPostService.addGroupPost(groupPost).subscribe({});
        this.homeCompoent.getUserPosts();
        window.location.reload();
      },
      error: (error) => {
        Swal.fire({
          icon: 'error',
          title: error.status,
          text: 'Nepavyko sukurti paskyros',
        });
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
      this.postForm.setValue({
        title: '',
        content: post.content,
        userId: this.homeCompoent.userId,
        type: post.type,
        picture: post.picture,
        group: post.group,
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
        userId: this.homeCompoent.userId,
        type: post.type,
        picture: post.picture,
        group: post.group,
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
        userId: this.homeCompoent.userId,
        type: post.type,
        picture: post.picture,
        group: post.group,
      });
      return false;
    }

    if (
      post.group == undefined ||
      post.group.id == undefined ||
      post.group.id.replace(/\s/g, '').length == 0
    ) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Please select the group',
      });
      return false;
    }

    if (post.type == 'Photo') {
      this.postForm.setValue({
        title: post.title,
        content: 'text',
        userId: this.homeCompoent.userId,
        type: post.type,
        picture: post.picture,
        group: post.group,
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
}
