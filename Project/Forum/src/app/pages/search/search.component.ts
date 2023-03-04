import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupService } from 'src/app/core/services/group.service';
import { GroupUserService } from 'src/app/core/services/groupUser.service';
import { Group, GroupUser } from 'src/app/core/types/web.types';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
})
export class SearchComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private groupService: GroupService,
    private groupUserService: GroupUserService,
    private router: Router
  ) {}

  userId: string = '';
  userGroups: GroupUser[] = [];
  groups: Group[] = [];

  loading = true;

  ngOnInit(): void {
    this.search();
  }
  search() {
    this.groups = [];

    this.activatedRoute.params.subscribe((params) => {
      const keyword = params['keyword'];
      if (keyword == 'mygroups') {
        this.userId = localStorage.getItem('userId')!;

        this.groupUserService.getGroupsByUserId(this.userId).subscribe({
          next: (userGroups) => {
            this.userGroups = userGroups;

            this.userGroups.forEach((userGroup) => {
              this.groupService.getGroupById(userGroup.groupId).subscribe({
                next: (data) => {
                  this.groups.push(data);
                  this.loading = false;
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
      } else if (keyword.replace(/\s/g, '').length != 0) {
        this.groupService.getGroupList().subscribe({
          next: (data) => {
            const groups: Group[] = data;
            const keywords: string[] = keyword.split(' ');
            for (let i = 0; i < keywords.length; i++) {
              //remove empty keywords
              if (keywords[i] == '') {
                keywords.splice(i, 1);
                i--;
              }
            }

            groups.forEach((group) => {
              const name = group.name.split(' ');
              for (let i = 0; i < name.length; i++) {
                if (name[i] == '') {
                  name.splice(i, 1);
                  i--;
                }
              }

              for (let k = 0; k < keywords.length; k++) {
                for (let i = 0; i < name.length; i++) {
                  if (name[i].toUpperCase() == keywords[k].toUpperCase()) {
                    this.groups.push(group);
                    break;
                  } else if (
                    keywords[k].toUpperCase() == group.visibility.toUpperCase()
                  ) {
                    this.groups.push(group);
                    break;
                  }
                }
              }
            });
            this.loading = false;
          },
          error: (error) => {
            console.log(error);
          },
        });
      } else {
        this.groupService.getGroupList().subscribe({
          next: (data) => {
            this.groups = data;
            this.loading = false;
          },
          error: (error) => {
            console.log(error);
          },
        });
      }
    });
  }

  openGroup(id: string) {
    this.router.navigate(['/group', id]);
  }
}
