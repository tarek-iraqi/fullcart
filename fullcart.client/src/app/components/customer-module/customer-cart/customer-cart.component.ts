import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../../services/customer.service';
import { CartService } from '../../../services/cart.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customer-cart',
  templateUrl: './customer-cart.component.html',
  styleUrl: './customer-cart.component.css',
})
export class CustomerCartComponent implements OnInit {
  cartItems!: any[];
  totalCart = 0;
  errorMessages!: any[];

  constructor(
    private customerService: CustomerService,
    private cartService: CartService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  removeItem(productId: string) {
    this.customerService
      .removeFromCart({
        ProductId: productId,
      })
      .subscribe({
        next: (response) => {
          this.loadData();
        },
      });
  }

  loadData() {
    this.customerService.cart().subscribe({
      next: (response) => {
        this.cartItems = response.data;
        this.cartService.updateCart(response.data.length);
        this.cartItems.forEach((item) => {
          this.totalCart += item.totalPrice;
        });
      },
    });
  }

  placeOrder() {
    this.customerService.placeOrder().subscribe({
      next: (response) => {
        this.snackBar.open('Order created successfully', '', {
          duration: 3000,
        });

        this.cartService.updateCart(0);

        this.router.navigate(['/customer/orderList']);
      },
      error: (response) => {
        this.errorMessages = response.error.errors;
      },
    });
  }
}
