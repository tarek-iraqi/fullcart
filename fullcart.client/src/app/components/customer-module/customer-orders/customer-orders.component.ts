import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { CustomerService } from '../../../services/customer.service';
import { MatSnackBar } from '@angular/material/snack-bar';
declare var $: any;

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styleUrl: './customer-orders.component.css',
})
export class CustomerOrdersComponent implements OnInit {
  constructor(
    private customerService: CustomerService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  displayedColumns: string[] = [
    'createdAt',
    'status',
    'total',
    'orderItems',
    'cancel',
  ];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit() {
    this.loadData(1, 10);
  }

  loadData(pageNumber: number, pageSize: number) {
    this.customerService.orders(pageNumber, pageSize).subscribe({
      next: (response) => {
        this.dataSource.data = response.data.list;
        $('.preloader').fadeOut();
      },
      error: (response) => {
        console.log(response);
        $('.preloader').fadeOut();
      },
    });
  }

  cancelOrder(order: any) {
    this.customerService.cancelOrder({ orderId: order.id }).subscribe({
      next: (response) => {
        this.snackBar.open('Order cancelled successfully', '', {
          duration: 3000,
        });

        this.loadData(1, 10);
      },
    });
  }
}
