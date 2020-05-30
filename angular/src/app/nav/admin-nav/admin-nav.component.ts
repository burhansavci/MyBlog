import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Admin } from 'src/app/models/admin';
import { ProgressBarService } from 'src/app/services/progress-bar.service';

@Component({
  selector: 'app-admin-nav',
  templateUrl: './admin-nav.component.html',
  styleUrls: ['./admin-nav.component.css'],
})
export class AdminNavComponent implements OnInit {
  admin: Admin;
  isCollapsed: boolean = true;
  isSidebarOpen: boolean = window.innerWidth < 768 ? false : true;
  @Output() isOpenEvent = new EventEmitter<boolean>(this.isSidebarOpen);
  constructor(
    public authService: AuthService,
    public progressBar: ProgressBarService
  ) {}

  ngOnInit(): void {}
  toggle(event: MouseEvent) {
    event.preventDefault();
    this.isSidebarOpen = !this.isSidebarOpen;
    this.isOpenEvent.emit(this.isSidebarOpen);
  }
}
