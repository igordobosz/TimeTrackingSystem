import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User, UsersService } from '../api.generated';
import { map, first } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private usersService: UsersService) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }
  login(email: string, password: string) {
    let user = new User();
    user.init({ id: 0, userName : "", email: email, password: password, token: "" });
    return this.usersService.post(user).pipe(first())
        .pipe(map(user => {
            if (user && user.token) {
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
            }
            return user;
        }));
}

  logout() {
      localStorage.removeItem('currentUser');
      this.currentUserSubject.next(null);
  }
}
