import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewOrdersComponent } from './view-orders.component';
import { DetailsOrderComponent } from './details-order.component';
import { NewOrderComponent } from './new-order.component';
import { EditOrderComponent} from './edit-order.component';
import { PrintOrderComponent } from "./print-order.component";
import { AuthGuard } from '../../_guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Bestillinger',
      roles: ['ItemTemplates_View']
    },
    canActivateChild: [AuthGuard],
    children: [
      {
        path: '',
        redirectTo: 'view'
      },
      {
        path: 'view',
        component: ViewOrdersComponent,
        data: {
          title: 'Vis bestillinger',
          roles: ['Order_View']
        }
      },
      {
        path: 'details/:id',
        component: DetailsOrderComponent,
        data: {
          title: 'Detaljer for bestilling',
          roles: ['Order_View']
        }
      },
      {
        path: 'edit/:id',
        component: EditOrderComponent,
        data: {
          title: 'Rediger bestilling',
          roles: ['Order_Add']
        }
      },
      {
        path: 'new',
        component: NewOrderComponent,
        data: {
          title: 'Ny bestilling',
          roles: ['Order_Add']
        }
      },
      {
        path: 'print/:id',
        component: PrintOrderComponent,
        data: {
          title: 'Print bestilling',
          roles: ['Order_View']
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrdersRoutingModule {}
