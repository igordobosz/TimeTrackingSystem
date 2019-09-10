import { Component, OnInit } from "@angular/core";
import { AuthenticationService } from "../authentication/authentication.service";
import { Router} from "@angular/router";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.css"]
})
export class HeaderComponent implements OnInit {
  logged: Boolean;
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.authenticationService.token.subscribe(token => this.logged = token ? true : false);
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(["/login"]);
  }
}
