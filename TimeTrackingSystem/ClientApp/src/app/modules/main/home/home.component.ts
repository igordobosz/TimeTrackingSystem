import { Component, OnInit } from "@angular/core";
import { AuthorizationService, User } from '../../../core/api.generated';

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"]
})
export class HomeComponent implements OnInit {
  public users: User[];
  public displayedColumns: string[] = ['id', 'username', 'email', 'password'];
  constructor(private authorizationService: AuthorizationService) {}

  ngOnInit() {
    this.authorizationService.get().subscribe(us => this.users = us);
  }
}
