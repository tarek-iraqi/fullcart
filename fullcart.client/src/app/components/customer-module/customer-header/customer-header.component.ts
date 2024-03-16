import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from '../../../services/cart.service';
import { Subscription } from 'rxjs';
import { CustomerService } from '../../../services/customer.service';

@Component({
  selector: 'app-customer-header',
  templateUrl: './customer-header.component.html',
  styleUrl: './customer-header.component.css',
})
export class CustomerHeaderComponent implements OnInit {
  username!: string | null;
  role!: string | null;
  numberOfProducts = 0;
  subscription: Subscription;

  constructor(
    private router: Router,
    private cartService: CartService,
    private customerService: CustomerService
  ) {
    this.subscription = this.cartService.data$.subscribe((data) => {
      this.numberOfProducts = data;
    });
  }

  ngOnInit(): void {
    this.username = localStorage.getItem('username');
    this.role = localStorage.getItem('role');

    this.customerService.cart().subscribe({
      next: (response) => {
        this.numberOfProducts = response.data.length;
      },
    });
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('username');
    localStorage.removeItem('role');
    this.router.navigate(['/']);
  }
}
