import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/shared/models/user.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

	form: FormGroup;
	loginFail = false;

	constructor(private formBuilder: FormBuilder, 
		private userService: UserService,
		private router: Router) { }

	ngOnInit(): void {
		this.form = this.formBuilder.group({
			email: ['', [Validators.required, Validators.email]],
			password: [null, Validators.required]
		});
	}

	login(): void {
		const user = this.form.getRawValue() as User;
		this.userService.loginUser(user).subscribe(res => {
			this.router.navigate(['']);
		}, err => {
			console.log(err);
			this.loginFail = true;
		});
	}

	/*eventTest(): void {
		let connection: HubConnection;
		this.connection = new HubConnectionBuilder().withUrl("http://localhost/notifications").build();
		this.connection.start().then(() => {
			console.log("conectado");
			this.connection.on("onSubscripted", (msg) => {
				alert(msg);
			});
		}).catch(() => {
			console.log("deu ruim")
		});

		this.connection.invoke('SubscribeToNetwork', "3fa85f64-5717-4562-b3fc-2c963f66afa6", 0).catch(() => {console.log('deu ruim no server')})
		this.alertService.globalAlert("teu toba lalalala lala", 'info-standard');
	}*/


}
