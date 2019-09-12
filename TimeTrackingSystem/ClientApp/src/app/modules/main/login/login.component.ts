import { Component, OnInit } from "@angular/core";
import { Router, Route, ActivatedRoute } from "@angular/router";
import { AuthenticationService } from '../../../core/authentication/authentication.service';
import { FormGroup, Validators, FormBuilder, Form } from '@angular/forms';
import { SnackbarService, SnackbarItem, SnackbarType } from '../../../core/services/snackbar.service';



@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})
export class LoginComponent implements OnInit {
  returnUrl: string;
  loginForm: FormGroup;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private snackbarService: SnackbarService
  ) {
    if (this.authenticationService.IsAutenthicated) {
      this.router.navigate(["/"]);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
  });
    this.returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
  }

  get f() {return this.loginForm.controls}

  onSubmit() {
    if (this.loginForm.invalid)
    {
      return;
    }
    this.authenticationService
      .login(this.f.username.value, this.f.password.value)
      .subscribe(response => {
        if (response) {
          this.snackbarService.success('Zalogowano');
          this.router.navigate([this.returnUrl]);
        } else {
          this.snackbarService.error('Błędne dane logowania');
        }
      });
  }

  cheat(){
    this.f.username.setValue("IDobosz");
    this.f.password.setValue("IDobosz");
  }
}
