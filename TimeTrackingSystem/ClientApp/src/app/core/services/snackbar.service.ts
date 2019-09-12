import { Injectable, Component } from "@angular/core";
import { Subject } from "rxjs";
import { MatSnackBarConfig, MatSnackBar } from "@angular/material/snack-bar";

export enum SnackbarType {
  Success,
  Warning,
  Error,
  Info
}

export class SnackbarItem {
  message: string;
  type: SnackbarType = SnackbarType.Success;
}

@Injectable({
  providedIn: "root",
})
export class SnackbarService {
  constructor(private snackbar: MatSnackBar) {}

  defaultErorr(){
    var item: SnackbarItem = {message: 'Niezidentyfikowany błąd', type: SnackbarType.Error};
    this.show(item);
  }

  error(message: string){
    var item: SnackbarItem = {message: message, type: SnackbarType.Error};
    this.show(item);
  }

  success(message: string){
    var item: SnackbarItem = {message: message, type: SnackbarType.Success};
    this.show(item);
  }

  warning(message: string){
    var item: SnackbarItem = {message: message, type: SnackbarType.Warning};
    this.show(item);
  }

  info(message: string){
    var item: SnackbarItem = {message: message, type: SnackbarType.Info};
    this.show(item);
  }

  show(snacbkarItem: SnackbarItem) {
    let config = new MatSnackBarConfig();
    config.duration = 3000;
    let cssClass = ['snackbar'];
    switch (snacbkarItem.type) {
      case SnackbarType.Success: {
        cssClass.push('snackbar-success');
        break;
      }
      case SnackbarType.Warning: {
        cssClass.push('snackbar-warning');
        break;
      }
      case SnackbarType.Error: {
        cssClass.push('snackbar-error');
        break;
      }
      case SnackbarType.Info: {
        cssClass.push('snackbar-info');
        break;
      }
    }
    config.panelClass = cssClass;
    this.snackbar.open(snacbkarItem.message, undefined, config);
  }
}
