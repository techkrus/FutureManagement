import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PaginationModule } from 'ngx-bootstrap/pagination';
import { OrdersRoutingModule } from './orders-routing.module';

import { DetailsOrderComponent } from './details-order.component';
import { ViewOrdersComponent } from './view-orders.component';
import { NewOrderComponent } from './new-order.component';
import { EditOrderComponent} from './edit-order.component';
import { PrintOrderComponent } from './print-order.component';

import { FormsModule } from '@angular/forms';
import { Ng2TableModule } from 'ng2-table/ng2-table';
import { NgSelectModule } from '@ng-select/ng-select';
import { TechTableModule } from '../../_modules/techtable/techtable.module';
import { from } from 'rxjs';

@NgModule({
  imports: [
    OrdersRoutingModule,
    PaginationModule.forRoot(),
    FormsModule,
    NgSelectModule,
    Ng2TableModule,
    TechTableModule,
    CommonModule
  ],
  declarations: [
    ViewOrdersComponent,
    NewOrderComponent,
    DetailsOrderComponent,
    EditOrderComponent,
    PrintOrderComponent
  ]
})
export class OrdersModule { }
