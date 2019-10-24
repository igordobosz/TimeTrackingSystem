import { Pipe, PipeTransform } from '@angular/core';

import { RegisterTimePerEmployeeDayWrapperViewModel } from '../../core/api.generated';
@Pipe({
    name: 'filterWorkEventsAttended',
})
export class FilterWorkEventsAttended implements PipeTransform {
    transform(items: RegisterTimePerEmployeeDayWrapperViewModel[], attended: boolean): any[] {
        if (items != null && attended) {
            return items.filter(it => {
                return it.workRegisterEvent != null;
            });
        }
        return items;
    }
}
