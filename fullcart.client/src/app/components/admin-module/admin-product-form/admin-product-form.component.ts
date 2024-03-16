import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminServicesService } from '../../../services/admin-services.service';
import { Product } from '../../../Models/ProductModel';
import { ApiErrorResponse } from '../../../Models/ApiErrorResponse';
import { LookupService } from '../../../services/lookup.service';
import { Lookup } from '../../../Models/Lookup';
import { MatSnackBar } from '@angular/material/snack-bar';

declare var $: any;

@Component({
  selector: 'app-admin-product-form',
  templateUrl: './admin-product-form.component.html',
  styleUrl: './admin-product-form.component.css',
})
export class AdminProductFormComponent implements OnInit {
  productId: any;
  productForm!: FormGroup;
  errorMessage!: ApiErrorResponse;
  brands!: Lookup[];
  categories!: Lookup[];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private adminService: AdminServicesService,
    private lookupService: LookupService,
    private snackBar: MatSnackBar
  ) {
    this.productForm = this.formBuilder.group({
      name: ['', Validators.required],
      category: ['', Validators.required],
      brand: ['', Validators.required],
      price: ['', Validators.required],
      quantity: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.productId = this.route.snapshot.paramMap.get('id');

    if (this.productId) {
      this.adminService.product(this.productId).subscribe({
        next: (response) => {
          var product = response.data;

          this.productForm.controls['name'].setValue(product.name);
          this.productForm.controls['description'].setValue(
            product.description
          );
          this.productForm.controls['price'].setValue(product.price);
          this.productForm.controls['quantity'].setValue(product.quantity);
          this.productForm.controls['category'].setValue(product.categoryId);
          this.productForm.controls['brand'].setValue(product.brandId);

          $('.preloader').fadeOut();
        },
        error: (response) => {},
      });
    } else {
      $('.preloader').fadeOut();
    }

    this.lookupService.getCategories().subscribe({
      next: (response) => {
        this.categories = response.data;
      },
    });

    this.lookupService.getBrands().subscribe({
      next: (response) => {
        this.brands = response.data;
      },
    });
  }

  save() {
    if (this.productForm.valid) {
      this.adminService
        .addUpdateProduct(
          new Product(
            this.productId,
            this.productForm.controls['name'].value,
            this.productForm.controls['description'].value,
            this.productForm.controls['price'].value,
            this.productForm.controls['quantity'].value,
            this.productForm.controls['category'].value,
            this.productForm.controls['brand'].value
          )
        )
        .subscribe({
          next: (response) => {
            console.log(response);
            this.snackBar.open('Saved successfully', '', {
              duration: 3000,
            });
            this.router.navigate(['/admin/productList']);
          },
          error: (response) => {
            this.errorMessage = response.error;
          },
        });
    }
  }
}
