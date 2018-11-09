import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../_services/alertify.service';



@Component({
  templateUrl: './new-user-roles.component.html'
})

export class NewUserRolesComponent implements OnInit {

  constructor(private alertify: AlertifyService) {}

  // dette objekt er kun til test af UI. Skal ændres til en model når backend for userroles virker
  userRoles: { id: number, name: string, hasRole: boolean, subRoles: { id: number, name: string, hasRole: boolean }[] }[] = [
    {
      'id': 0, 'name': 'Kunder', hasRole: false, subRoles: [
        { id: 0, name: 'Vis alle kunder', hasRole: false }, { id: 1, name: 'Opret kunde', hasRole: false },
        { id: 2, name: 'Rediger kunde', hasRole: false }
      ],
    },
    {
      'id': 1, 'name': 'Projekter', hasRole: false, subRoles: [
        { id: 0, name: 'Vis alle projekter', hasRole: false }, { id: 1, name: 'Opret projekt', hasRole: false },
        { id: 2, name: 'Rediger projekt', hasRole: false }
      ],
    },
    {
      'id': 2, 'name': 'Bestillinger', hasRole: false, subRoles: [
        { id: 0, name: 'Vis alle bestillinger', hasRole: false }, { id: 1, name: 'Opret bestilling', hasRole: false },
        { id: 2, name: 'Rediger bestilling', hasRole: false }
      ],
    }
  ];

  ngOnInit() {

  }

  // Ændrer på bool værdien af en rolle når der ændres på en switch
  changeHasRole(id: number) {
    if (this.userRoles[id].hasRole === true) {
      this.userRoles[id].hasRole = false;
    } else {
      this.userRoles[id].hasRole = true;
    }
  }

  // Ændrer på bool værdien af en subrolle når der ændres på en switch
  changeHasSubRole(roleId: number, subRoleId: number) {
    if (this.userRoles[roleId].subRoles[subRoleId].hasRole === true) {
      this.userRoles[roleId].subRoles[subRoleId].hasRole = false;
    } else {
      this.userRoles[roleId].subRoles[subRoleId].hasRole = true;
    }
  }

  addRole() {
    // Skal implementeres når backend virker
    this.alertify.error('Kunne ikke tilføje rolle');
  }
}
