import { Component, OnInit } from "@angular/core";
import { AuthorizationService, RegisterTimePerDayViewModel, RegisterTimePerEmployeeViewModel, WorkRegisterEventService } from '../../../core/api.generated';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
    viewModel: RegisterTimePerDayViewModel;
    displayedColumns: string[] = ['employeeFullName', 'dateGoIn',];
    constructor(private workRegisterEventService: WorkRegisterEventService) { }

    ngOnInit() {
        this.subscribeList();
    }

    subscribeList() {
        this.workRegisterEventService.getEmployeesInWork().subscribe(e => {
            this.viewModel = e;
        });

    }
}
