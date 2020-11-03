import { Component, OnInit } from '@angular/core';
import { AlertsService } from '../../shared/services/alerts.service';
import { GlobalAlert } from '../../shared/models/global-alert.model';


@Component({
	selector: 'app-global-alerts',
	templateUrl: './global-alerts.component.html',
	styleUrls: ['./global-alerts.component.css']
})
export class GlobalAlertsComponent implements OnInit {

	alerts: GlobalAlert[] = [];
	

	constructor(private alertService: AlertsService) { }

	ngOnInit(): void {
		this.alertService.newAlert.subscribe(a => {			
			this.alerts.push(a);			
		})
	}

	alertClosed(alert: GlobalAlert): void {
		const i = this.alerts.indexOf(alert, 0);
		if(i  > -1) {
			this.alerts.splice(i, 1)
		}		
	}

}
