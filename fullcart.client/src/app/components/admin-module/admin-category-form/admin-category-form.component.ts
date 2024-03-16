import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminServicesService } from '../../../services/admin-services.service';
import { ApiErrorResponse } from '../../../Models/ApiErrorResponse';
import { MatSnackBar } from '@angular/material/snack-bar';

declare var $: any;

@Component({
  selector: 'app-admin-category-form',
  templateUrl: './admin-category-form.component.html',
  styleUrl: './admin-category-form.component.css',
})
export class AdminCategoryFormComponent implements OnInit {
  categoryId: any;
  categoryName: any;
  categoryForm!: FormGroup;
  errorMessage!: ApiErrorResponse;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private adminService: AdminServicesService,
    private snackBar: MatSnackBar
  ) {
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.categoryId = this.route.snapshot.paramMap.get('id');
    this.categoryName = this.route.snapshot.paramMap.get('name');

    if (this.categoryId) {
      this.categoryForm.controls['name'].setValue(this.categoryName);
    }

    $('.preloader').fadeOut();
  }

  save() {
    if (this.categoryForm.valid) {
      this.adminService
        .addUpdateCategory({
          id: this.categoryId,
          name: this.categoryForm.controls['name'].value,
        })
        .subscribe({
          next: (response) => {
            console.log(response);
            this.snackBar.open('Saved successfully', '', {
              duration: 3000,
            });
            this.router.navigate(['/admin/categoryList']);
          },
          error: (response) => {
            this.errorMessage = response.error;
          },
        });
    }
  }
}
