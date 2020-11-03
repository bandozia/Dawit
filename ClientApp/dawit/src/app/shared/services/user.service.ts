import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { User } from '../models/user.model';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import jwt_decode from "jwt-decode";

const AUTH_URL = `${environment.API_URL}/auth`;
const TOKEN_KEY = "authToken";

@Injectable({
	providedIn: 'root'
})
export class UserService {

	private user$ = new BehaviorSubject<User>(null);

	constructor(private http: HttpClient) {
		let user = this.getStoredUser();
		if (user) {
			this.user$.next(user);
		}
	}	

	public getUser(): Observable<User> {
		return this.user$.asObservable();
	}

	public loginUser(user: User) {
		return this.http.post(`${AUTH_URL}/login`, user, {
			responseType: 'text',
			observe: 'body'
		}).pipe(tap(token => {
			window.localStorage.setItem(TOKEN_KEY, token);
			this.user$.next(user);
		}));			
	}

	public createUser(user: User) {
		return this.http.post(`${AUTH_URL}/register`, user);
	}

	public logoutUser() {
		window.localStorage.removeItem(TOKEN_KEY);
		this.user$.next(null);
	}

	private getStoredUser(): User {		
		const token = window.localStorage.getItem(TOKEN_KEY);
		if (token) {
			const user = jwt_decode(token) as User;
			return user;
		} else {
			return null;
		}
	}
}
