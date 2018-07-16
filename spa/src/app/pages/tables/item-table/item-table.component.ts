import { Component } from '@angular/core';
import { LocalDataSource } from 'ng2-smart-table';


import { ItemTableService } from '../../../@core/data/item-table.service';


@Component({
  selector: 'ngx-item-table',
  templateUrl: './item-table.component.html',
  styles: [`
    nb-card {
      transform: translate3d(0, 0, 0);
    }
  `],
})
export class ItemTableComponent {

  settings = {
    mode: 'external',
    delete: {
      deleteButtonContent: '<i class="nb-trash"></i>',
      confirmDelete: true,
    },
    add: {
      addButtonContent: 'Tilføj ny',
    },
    edit: {
      editButtonContent: '<i class="nb-edit"></i>',
      saveButtonContent: '<i class="nb-checkmark"></i>',
      cancelButtonContent: '<i class="nb-close"></i>',
    },
    columns: {
      name: {
        title: 'Navn',
        type: 'string',
      },
      placement: {
        title: 'Placering',
        type: 'string',
      },
      amount: {
        title: 'Mængde',
        type: 'string',
      },
      order: {
        title: 'Ordre',
        type: 'string',
      },
      type: {
        title: 'Lavet af skabelon',
        type: 'string',
      },
    },
  };

  source: LocalDataSource = new LocalDataSource();

  constructor(private service: ItemTableService) {
    const data = this.service.getData();
    this.source.load(data);
  }

  onDeleteConfirm(event): void {
    if (window.confirm('Er du sikker på at du vil slette denne forekomst?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }
  }

  editItem(event): void {
    location.href = 'http://localhost:4200/#/pages/forms/item-detail';
  }

  deleteItem(event): void {
    if (window.confirm('Er du sikker på at du vil slette denne forekomst?')) {
      event.confirm.resolve();
    } else {
      event.confirm.reject();
    }
  }

  addNewItem() {
    location.href = 'http://localhost:4200/#/pages/forms/item';
  }
}
