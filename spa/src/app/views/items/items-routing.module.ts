import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ViewItemsComponent } from './view-items.component';
import { AddItemsComponent } from './new-items.component';
import { DetailsItemComponent } from './details-item.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Genstande'
    },
    children: [
      {
        path: 'view',
        component: ViewItemsComponent,
        data: {
          title: 'Vis genstande'
        }
      },
      {
        path: 'new',
        component: AddItemsComponent,
        data: {
          title: 'Tilføj ny genstand'
        }
      },
      {
        path: 'details/:id',
        component: DetailsItemComponent,
        data: {
          title: 'Detaljer om genstand'
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ItemsRoutingModule {}
