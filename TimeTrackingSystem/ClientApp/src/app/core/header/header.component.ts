import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { Router, Route, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  logged = false;
  returnUrl: string;
  constructor(private authenticationService: AuthenticationService, private router: Router, private route: ActivatedRoute) {
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/']);
   }
  }

  ngOnInit() {
    this.authenticationService.currentUser.subscribe(s => this.logged = s != null)
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login(){
    this.authenticationService.login("igordobosz@gmail.com", "IDobosz").subscribe(user => {
      if (user.token)
      {
        this.router.navigate([this.returnUrl]);
      }
      else
      {
        alert('errror');
      }})
  }

  logout(){
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }

}
