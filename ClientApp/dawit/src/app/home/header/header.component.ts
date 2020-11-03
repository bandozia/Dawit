import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/shared/models/user.model';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

	user$: Observable<User>;

	constructor(private userService: UserService, private router: Router) { }

	ngOnInit(): void {
		this.user$ = this.userService.getUser();
	}

	logout(): void {
		this.userService.logoutUser();
		this.router.navigate(['/login']);
	}

}
