import { SnackbarService } from '../services/snackbar.service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: "root",
})
export class SnackbarHelper{

  constructor(private snackbarService: SnackbarService){}

  deleteSuccess(){
    this.snackbarService.success("Pomyślnie usunięto");
  }

  deleteFail(){
    this.snackbarService.success("Usuwanie nie powiodło się");
  }

  insertSuccess(){
    this.snackbarService.success("Pomyślnie dodano");
  }

  insertFail(){
    this.snackbarService.success("Dodawanie nie powiodło się");
  }

  updateSuccess(){
    this.snackbarService.success("Pomyślnie zeedytowano");
  }

  updateFail(){
    this.snackbarService.success("Edycja nie powiodła się");
  }
}
