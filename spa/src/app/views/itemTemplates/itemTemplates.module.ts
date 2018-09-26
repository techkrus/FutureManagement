import { NgModule } from '@angular/core';

import { Ng2SmartTableModule } from 'ng2-smart-table';
import { ItemTemplatesRoutingModule } from './itemTemplates-routing.module';

import { DetailsItemTemplateComponent } from './details-itemTemplate.component';
import { ViewItemTemplatesComponent } from './view-itemTemplates.component';
import { NewItemTemplateComponent } from './new-itemTemplate.component';

import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';


@NgModule({
  imports: [
    ItemTemplatesRoutingModule,
    Ng2SmartTableModule,
    FormsModule,
    CommonModule,
    NgSelectModule
  ],
  declarations: [
    ViewItemTemplatesComponent,
    NewItemTemplateComponent,
    DetailsItemTemplateComponent ]
})
export class ItemTemplatesModule { }