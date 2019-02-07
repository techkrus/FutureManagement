import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { AuthService } from '../../_services/auth.service';
import { UserForRegister } from '../../_models/UserForRegister';
import { AlertifyService } from '../../_services/alertify.service';
import { Router } from '@angular/router';
import { UserService } from '../../_services/user.service';
import { UserRole } from '../../_models/UserRole';
import { RoleCategory } from '../../_models/RoleCategory';


@Component({
  templateUrl: './new-user.component.html'
})

export class NewUserComponent implements OnInit {

  baseUrl = environment.spaUrl;
  user: UserForRegister = {} as UserForRegister;
  passwordConfirm = '';
  isValid = true;
  roles: RoleCategory[] = [];
  roleFilter: string;
  itemsPerPage = 15;
  currentPage = 1;
  rolesToAdd: RoleCategory[] = [] as RoleCategory[];
  roleCategoryNames: string[] = [] as string[];

  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router,
              private userService: UserService) {

  }
  ngOnInit(): void {
    this.user.password = '';
    this.userService.getAllRoleCategories().subscribe(roleCategories => {
      this.roles = roleCategories;
    });
  }
  createUser() {
    this.user.RoleCategory = this.rolesToAdd;
    if (this.user.password === this.passwordConfirm) {
      this.authService.register(this.user).subscribe(() => {
        this.alertify.success('Brugeren blev oprettet');
        this.router.navigate(['users/view/']);
      }, error => {
        this.alertify.error('Kunne ikke tilføje bruger');
      });
    } else {
      this.alertify.error('Brugeren var ikke valid');
    }
    console.log(this.user);
  }

  goToUserTable() {
    this.router.navigate(['/users/view/']);
  }

  onCheckboxChange(role, event) {
    if (event.target.checked) {
      this.rolesToAdd.push(role);
    } else {
      for (let i = 0; i < this.roles.length; i++) {
        if (this.rolesToAdd[i] === role) {
          this.rolesToAdd.splice(i, 1);
        }
      }
    }
  }

  checkBox(roleName) {
    for (const roleToCheck of this.rolesToAdd) {
      if (roleToCheck === roleName) {
        return true;
      }
    }
    return false;
  }
}
