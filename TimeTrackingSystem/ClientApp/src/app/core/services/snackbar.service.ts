import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { MatSnackBarConfig, MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {
  constructor(private snackbar: MatSnackBar) { }

  show(snacbkarItem: SnackbarItem)
  {
    let config = new MatSnackBarConfig();
    config.duration = 3000;
    this.snackbar.open(snacbkarItem.message, undefined, config);
  }
}
export enum SnackbarType{
  Success,
  Warning,
  Error,
  Info
}

export class SnackbarItem{
  message: string;
  type?: SnackbarType = SnackbarType.Success;
}


