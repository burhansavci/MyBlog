import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Admin } from 'src/app/models/admin';
import { AlertifyService } from 'src/app/services/alertify.service';
import { DataResult } from 'src/app/models/data-result';
import { ProgressBarService } from 'src/app/services/progress-bar.service';

@Component({
  selector: 'app-admin-nav',
  templateUrl: './admin-nav.component.html',
  styleUrls: ['./admin-nav.component.css'],
})
export class AdminNavComponent implements OnInit {
  admin: Admin;

  constructor(
    public authService: AuthService,
    private alertifyService: AlertifyService,
    public progressBar: ProgressBarService
  ) {}

  ngOnInit(): void {
    this.authService.loadCurrentUser(localStorage.getItem('token')).subscribe(
      (admin: Admin) => {
        this.admin = admin;
      },
      (error) => {
        this.alertifyService.error(error);
      }
    );
  }
}
