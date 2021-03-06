import { Injectable } from "@angular/core";
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpInterceptor,
    HttpErrorResponse
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { retry, catchError } from "rxjs/operators";
import { SnackbarService } from "../services/snackbar.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private snackbarService: SnackbarService) { }

    intercept(
        request: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(
            retry(1),
            catchError((error: HttpErrorResponse) => {
                if (error.status === 400) {
                    this.snackbarService.error("Bad request.");
                } else if (error.status === 401) {
                    this.snackbarService.error("Unauthorized.");
                } else if (error.status === 404) {
                    this.snackbarService.error("Resource unavaible.");
                } else if (error.status === 500) {
                    this.snackbarService.error("Server doesn't respond.");
                } else {
                    return throwError(error);
                }
            })
        );
    }
}
