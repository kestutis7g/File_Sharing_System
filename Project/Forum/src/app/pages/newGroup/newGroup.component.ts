import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { GroupService } from 'src/app/core/services/group.service';
import { GroupUser } from 'src/app/core/types/web.types';
import { GroupUserService } from 'src/app/core/services/groupUser.service';

@Component({
  selector: 'app-newGroup',
  templateUrl: './newGroup.component.html',
  styleUrls: ['./newGroup.component.scss'],
})
export class NewGroupComponent implements OnInit {
  constructor(
    private groupService: GroupService,
    private groupUserService: GroupUserService,
    private router: Router,
    private fb: FormBuilder
  ) {}
  groupForm: FormGroup = new FormGroup({
    name: new FormControl<string>('', [Validators.required]),
    visibility: new FormControl<string>('Public', [Validators.required]),
    iconPicture: new FormControl<string>(
      'https://i.pinimg.com/564x/32/73/52/3273526eafb749b98268e7ecef52a432.jpg'
    ),
    description: new FormControl<string>('', [Validators.required]),
    userId: new FormControl<string>(localStorage.getItem('userId') || '', [
      Validators.required,
    ]),
  });

  ngOnInit() {
    console.log(localStorage.getItem('userId'));

    if (!localStorage.getItem('userId')) {
      this.router.navigate(['/login']).then(() => {});
    }
    this.groupForm.value.userId = localStorage.getItem('userId');
  }

  addGroup() {
    if (this.groupForm.value.name.replace(/\s/g, '').length == 0) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Please type a valid title name',
      });
      return;
    }
    if (this.groupForm.value.name.length > 32) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Title name can be up to 32 characters',
      });
      return;
    }

    if (this.groupForm.value.description.replace(/\s/g, '').length == 0) {
      Swal.fire({
        icon: 'error',
        title: '',
        text: 'Please enter group description',
      });
      return;
    }

    this.groupService.addGroup(this.groupForm.value).subscribe({
      next: (group) => {
        console.log(group);

        const groupUser: GroupUser = {
          userId: localStorage.getItem('userId')!,
          groupId: group.id!,
          isAdmin: true,
        };
        this.groupUserService.addGroupUser(groupUser).subscribe({
          next: (data) => {
            console.log(data);
            this.router.navigate(['/group', group.id]).then(() => {});
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
          text: 'Unsuccessful group creation',
        });
        console.log(error);
      },
    });
  }
}
