import { FormGroup } from '@angular/forms';

// custom validator to check that end date is bigger than start date
export function DateComparison(startDateControlName: string, endDateControlName: string) {
    return (formGroup: FormGroup) => {
        const startDateControl = formGroup.controls[startDateControlName];
        const endDateControl = formGroup.controls[endDateControlName];

        if (endDateControl.errors && !endDateControl.errors.dateComparison) {
            // return if another validator has already found an error on the endDate control
            return;
        }
        let startDate = new Date(startDateControl.value);
        let endDate = new Date(endDateControl.value);
        // set error on endDate control if validation fails
        if (startDate > endDate) {
            endDateControl.setErrors({ dateComparison : true });
        } else {
            endDateControl.setErrors(null);
        }
    }
}