import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import {
  EmployeeViewModel,
  EmployeeService,
  IEmployeeViewModel
} from "../../../core/api.generated";
import { Location } from "@angular/common";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { SnackbarHelper } from "../../../core/helpers/snackbar.helper";

@Component({
  selector: "app-employe-edit",
  templateUrl: "./employe-edit.component.html",
  styleUrls: ["./employe-edit.component.scss"]
})
export class EmployeEditComponent implements OnInit {
  editMode: boolean = false;
  employee: EmployeeViewModel;
  form: FormGroup;
  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private location: Location,
    private formBuilder: FormBuilder,
    private snackbarHelper: SnackbarHelper
  ) {}

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: ["", Validators.required],
      surename: ["", Validators.required],
      identityCode: ["", Validators.required]
    });

    this.route.params.subscribe(params => {
      if (params != null && params["id"] != null) {
        this.editMode = true;
        this.employeeService.getByID(params["id"]).subscribe(e => {
          this.employee = e;
          this.form.patchValue(this.employee);
        });
      } else {
        this.employee.init();
      }
    });
  }

  onCancel() {
    this.location.back();
  }

  async onSubmit(data: EmployeeViewModel) {
    if (this.form.invalid) {
      return;
    }
    if (this.editMode) {
      data.id = this.employee.id;
      await this.employeeService.update(data).toPromise().then(r => {
        if (r.success) {
          this.snackbarHelper.updateSuccess();
        } else {
          this.snackbarHelper.updateFail();
        }
      });
    } else {
      await this.employeeService.insert(data).toPromise().then(
        r => {
          if (r.success) {
            this.snackbarHelper.insertSuccess();
          } else {
            this.snackbarHelper.insertFail();
          }
        }
      );
    }
    this.onCancel();
  }
}
