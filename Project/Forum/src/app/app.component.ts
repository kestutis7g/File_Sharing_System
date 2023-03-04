import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { UserService } from './core/services/user.service';
import { User } from './core/types/web.types';
import { ThemeService } from './theme/theme.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Forum';
  public isCollapsed = true;
  constructor(
    private themeService: ThemeService,
    private router: Router,
    private userService: UserService
  ) {
    this.darkTheme = localStorage.getItem('dark') == 'true';
    this.signedIn = localStorage.getItem('userId') != undefined ?? false;
  }

  darkTheme: boolean;
  signedIn: boolean;
  searchKeyword: string = '';
  userName: string = '';

  ngOnInit(): void {
    if (this.darkTheme) {
      this.themeService.setTheme('dark');
    } else {
      this.themeService.setTheme('light');
    }
    if (this.signedIn) {
      this.userService.getUserById(localStorage.getItem('userId')!).subscribe({
        next: (data) => {
          const user: User = data;
          this.userName = user.name;
        },
        error: (error) => {
          console.log(error);
        },
      });
    }
  }

  toggleTheme() {
    if (!this.darkTheme) {
      this.themeService.setTheme('dark');
      this.darkTheme = true;
    } else {
      this.themeService.setTheme('light');
      this.darkTheme = false;
    }
    localStorage.setItem('dark', this.darkTheme.toString());
  }

  authenticationFailedMessage() {
    Swal.fire({
      position: 'top-end',
      icon: 'info',
      title: 'Prašome prisijungti iš naujo',
      showConfirmButton: false,
      timer: 2000,
    });
  }

  search() {
    this.searchKeyword = this.searchKeyword.substring(0, 64);
    this.router.navigate(['/search', this.searchKeyword]).then(() => {
      window.location.reload();
    });
    this.searchKeyword = '';
  }

  signOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('userId');
    this.signedIn = false;
    window.location.reload();
  }
}
