import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { getStyle, hexToRgba } from '@coreui/coreui/dist/js/coreui-utilities';
import { CustomTooltips } from '@coreui/coreui-plugin-chartjs-custom-tooltips';
import { AuthService } from '../../_services/auth.service';

@Component({
  templateUrl: 'dashboard.component.html'
})

export class DashboardComponent {
  constructor(public authService: AuthService) {
  }
}

