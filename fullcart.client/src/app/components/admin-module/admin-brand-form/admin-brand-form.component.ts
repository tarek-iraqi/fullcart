import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminServicesService } from '../../../services/admin-services.service';
import { ApiErrorResponse } from '../../../Models/ApiErrorResponse';
import { MatSnackBar } from '@angular/material/snack-bar';

declare var $: any;

@Component({
  selector: 'app-admin-brand-form',
  templateUrl: './admin-brand-form.component.html',
  styleUrl: './admin-brand-form.component.css',
})
export class AdminBrandFormComponent implements OnInit {
  brandId: any;
  brandName: any;
  brandForm!: FormGroup;
  errorMessage!: ApiErrorResponse;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private adminService: AdminServicesService,
    private snackBar: MatSnackBar
  ) {
    this.brandForm = this.formBuilder.group({
      name: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.brandId = this.route.snapshot.paramMap.get('id');
    this.brandName = this.route.snapshot.paramMap.get('name');

    if (this.brandId) {
      this.brandForm.controls['name'].setValue(this.brandName);
    }

    $('.preloader').fadeOut();
  }

  save() {
    if (this.brandForm.valid) {
      this.adminService
        .addUpdateBrand({
          id: this.brandId,
          name: this.brandForm.controls['name'].value,
        })
        .subscribe({
          next: (response) => {
            console.log(response);
            this.snackBar.open('Saved successfully', '', {
              duration: 3000,
            });
            this.router.navigate(['/admin/brandList']);
          },
          error: (response) => {
            this.errorMessage = response.error;
          },
        });
    }
  }
}
