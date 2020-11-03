import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/shared/models/user.model';
import { UserService } from 'src/app/shared/services/user.service';
import { RegisterValidator } from './register.validator';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

	constructor(private formBuilder: FormBuilder, private userService: UserService) { }

	form: FormGroup;
	registerError: string = null;
	userCreated = false;

	ngOnInit(): void {
		this.buildForm();
	}

	private buildForm(): void {
		this.form = this.formBuilder.group({
			email: ['', [Validators.required, Validators.email]],
			password: ['', [Validators.required, Validators.minLength(6)]],
			repeatPassword: ['']
		});
		
		this.form.get('repeatPassword').setValidators([Validators.required, RegisterValidator.sameValue(this.form.get('password'))]);			
	}
	
	register(): void {
		const newUser = this.form.getRawValue() as User;
		this.userService.createUser(newUser).subscribe(() => {
			this.registerError = null;
			this.userCreated = true;
		}, err => this.registerError = err.error.detail);
	}
}
