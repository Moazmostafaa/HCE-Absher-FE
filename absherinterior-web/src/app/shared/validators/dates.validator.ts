import { DatePipe } from "@angular/common";
import { AbstractControl, FormControl, ValidatorFn } from "@angular/forms";

export class DateValidators {
  static dateLessThan(
    startDate: string,
    endDate: string,
    validatorField: { [key: string]: boolean }
  ): ValidatorFn {
    return (c: AbstractControl): { [key: string]: boolean } | null => {
      const date1 = c.get(startDate).value;
      const date2 = c.get(endDate).value;
      if (date1 && date2 && date1 > date2) {
        return validatorField;
      }
      return null;
    };
  }
}

export function dateLessThanToday(control: FormControl) {
  if (!control.value) return null;


  var date = new Date(control.value);
  var today = new Date();
  var todaysDate = new Date(today.getFullYear(), today.getMonth(), today.getDate(), 0, 0, 0);

  const isValid = date >= todaysDate;
  return isValid ? null : { lessThanToday: true };
}
