import { Component } from '@angular/core';
import { DashboardComponent } from './dashboard/dashboard';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [DashboardComponent], // 👈 MUITO IMPORTANTE
  template: `<app-dashboard></app-dashboard>`
})
export class AppComponent {}
