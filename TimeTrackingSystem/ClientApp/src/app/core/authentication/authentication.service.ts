import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginDTO, AuthorizationService } from '../api.generated';
import { map, first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private tokenSubject: BehaviorSubject<string>;
  public token: Observable<string>;

  constructor(private authorizationService: AuthorizationService) {
    this.tokenSubject = new BehaviorSubject<string>(JSON.parse(localStorage.getItem('token')));
    this.token = this.tokenSubject.asObservable();
  }

  public get IsAutenthicated() {
    return this.tokenSubject.value ? true : false;
  }

  public get Token(){
    return this.tokenSubject.value;
  }

  login(username: string, password: string) {
    let user = new LoginDTO({password: password, username: username});
    return this.authorizationService.login(user).pipe(first())
        .pipe(map(loginResponse => {
            if (loginResponse.success) {
                localStorage.setItem('token', JSON.stringify(loginResponse.token));
                this.tokenSubject.next(JSON.stringify(loginResponse.token));
            }
            return loginResponse.success;
        }));
}

  logout() {
      localStorage.removeItem('token');
      this.tokenSubject.next(null);
  }
}
