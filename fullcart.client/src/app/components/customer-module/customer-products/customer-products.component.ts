import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../../services/customer.service';
import { Lookup } from '../../../Models/Lookup';
import { LookupService } from '../../../services/lookup.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CartService } from '../../../services/cart.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-customer-products',
  templateUrl: './customer-products.component.html',
  styleUrl: './customer-products.component.css',
})
export class CustomerProductsComponent implements OnInit {
  products!: any[];
  brands!: Lookup[];
  categories!: Lookup[];
  productForm!: FormGroup;
  errorMessage: any;

  constructor(
    private customerService: CustomerService,
    private formBuilder: FormBuilder,
    private lookupService: LookupService,
    private cartService: CartService,
    private snackBar: MatSnackBar
  ) {
    this.productForm = this.formBuilder.group({
      productName: [''],
      category: [''],
      brand: [''],
    });
  }

  ngOnInit(): void {
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

    this.loadData(1, 10, '', null, null);
  }

  search() {
    this.loadData(
      1,
      10,
      this.productForm.controls['productName'].value,
      this.productForm.controls['brand'].value,
      this.productForm.controls['category'].value
    );
  }

  clear() {
    this.productForm.controls['productName'].setValue('');
    this.productForm.controls['brand'].setValue('');
    this.productForm.controls['category'].setValue('');
    this.loadData(1, 10, '', null, null);
  }

  loadData(
    pageNumber: number,
    pageSize: number,
    productName: string | null,
    brand: number | null,
    category: number | null
  ) {
    this.customerService
      .productsList(pageNumber, pageSize, productName, brand, category)
      .subscribe({
        next: (responce) => {
          this.products = responce.data.list;
        },
      });
  }

  addToCart(productId: any) {
    this.errorMessage = null;
    this.customerService
      .addToCart({ ProductId: productId, Quantity: 1 })
      .subscribe({
        next: (response) => {
          this.cartService.updateCart(response.data);
          this.snackBar.open('Item added successfully', '', {
            duration: 3000,
          });
        },
        error: (response) => {
          this.errorMessage = response.error.errors;
        },
      });
  }
}
