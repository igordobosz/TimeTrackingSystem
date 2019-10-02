import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../authentication/authentication.service';
import { API_BASE_URL } from '../api.generated';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const isApiUrl = request.url.includes('api');
        if (this.authenticationService.IsAutenthicated && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${this.authenticationService.Token}`
                }
            });
        }


        return next.handle(request);
    }
}
