import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { GlobalAlert } from '../models/global-alert.model';

@Injectable({
	providedIn: 'root'
})
export class AlertsService {

	newAlert = new Subject<GlobalAlert>();

	constructor() { }

	public globalAlert(msg: string, type: string): void {
		this.newAlert.next({ text: msg, type: type });
	}
}
