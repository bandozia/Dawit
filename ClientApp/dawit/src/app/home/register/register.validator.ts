import { AbstractControl } from '@angular/forms';

export class RegisterValidator {

    public static sameValue(other: AbstractControl) {
        return (control: AbstractControl) =>
            (control.value != other.value) ? { dontMatch: true } : null
    }
}