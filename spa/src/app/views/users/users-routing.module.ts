import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewUsersComponent } from './view-users.component';
import { NewUserComponent } from './new-user.component';
import { EditUserComponent } from './edit-user.component';
import { DetailsUserComponent } from './details-user.component';
import { AuthGuard } from '../../_guards/auth.guard';
import { ViewDeactivatedUsersComponent } from './view-deactivated-users.component';



const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Brugere'
    },
    canActivateChild: [AuthGuard],
    children: [
      {
        path: '',
        redirectTo: 'view'
      },
      {
        path: 'view',
        component: ViewUsersComponent,
        data: {
          title: 'Vis brugere',
          roles: ['User_View']
        }
      },
      {
        path: 'deactivated-view',
        component: ViewDeactivatedUsersComponent,
        data: {
          title: 'Vis deaktiverede brugere',
          roles: ['User_ActivateDeactivate']
        }
      },
      {
        path: 'new',
        component: NewUserComponent,
        data: {
          title: 'Tilføj ny bruger',
          roles: ['User_Add']
        }
      },
      {
        path: 'edit/:id',
        component: EditUserComponent,
        data: {
            title: 'Rediger bruger',
            roles: ['User_Edit']
        }
      },
      {
        path: 'details/:id',
        component: DetailsUserComponent,
        data: {
          title: 'Detaljer om bruger',
          roles: ['User_View']
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule {}
