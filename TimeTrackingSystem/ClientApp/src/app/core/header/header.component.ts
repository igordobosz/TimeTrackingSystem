import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  logged = false;
  constructor(private authentication: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.authentication.currentUser.subscribe(s => this.logged = s != null)
  }

  login(){
    this.authentication.login("igordobosz@gmail.com", "IDobosz");
  }

  logout(){
    this.authentication.logout();
    this.router.navigate(['/login']);
  }

}
