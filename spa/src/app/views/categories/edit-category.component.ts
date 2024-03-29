
import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../_services/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../../_models/Category';
import { AlertifyService } from '../../_services/alertify.service';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  templateUrl: './edit-category.component.html'
})
export class EditCategoryComponent implements OnInit {

  constructor(private categoryService: CategoryService, private route: ActivatedRoute, private alertify: AlertifyService,
              private router: Router, private spinnerService: Ng4LoadingSpinnerService) {}

  category = {} as Category;

  ngOnInit() {
    this.categoryService.getCategory(+this.route.snapshot.params['id'])
      .subscribe(category => {
        this.category = category;
      });
  }

  Save() {
    this.spinnerService.show();
        this.categoryService.editCategory(this.category).subscribe(
          category => {
            this.category = category;
            this.alertify.success('Opdaterede kategori');
          },
          error => {
            this.alertify.error(error.error);
          }, () => {
            this.router.navigate(['categories/view']);
          });
    this.spinnerService.hide();
    }
}
