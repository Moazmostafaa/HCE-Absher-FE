import { AbstractControl, ValidatorFn } from "@angular/forms";

export class EventDateValidators {
  static checkRegistrationEndDate(
    regStartDate: string,
    regEndDate: string,
    validatorField: { [key: string]: boolean }
  ): ValidatorFn {
    return (c: AbstractControl): { [key: string]: boolean } | null => {
      const date1 = c.get(regStartDate).value;
      const date2 = c.get(regEndDate).value;
      if ((date1 == null && date2 != null) || (date1 != null && date2 == null)) {
        return validatorField;
      }
      return null;
    };
  }
}
