import { Component, OnInit, ViewChild } from '@angular/core';
import { AdminServicesService } from '../../../services/admin-services.service';
import { AdminProductList } from '../../../Models/AdminProductList';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

declare var $: any;
@Component({
  selector: 'app-admin-products',
  templateUrl: './admin-products.component.html',
  styleUrl: './admin-products.component.css',
})
export class AdminProductsComponent implements OnInit {
  productList!: AdminProductList[];
  constructor(
    private adminService: AdminServicesService,
    private router: Router
  ) {}

  displayedColumns: string[] = [
    'name',
    'brand',
    'category',
    'price',
    'quantity',
    'edit',
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
    this.adminService.productsList(pageNumber, pageSize).subscribe({
      next: (response) => {
        this.productList = response.data.list;
        this.dataSource.data = this.productList;
        $('.preloader').fadeOut();
      },
      error: (response) => {
        console.log(response);
        $('.preloader').fadeOut();
      },
    });
  }

  addNewProduct() {
    this.router.navigate(['/admin/productForm']);
  }

  editProduct(product: any) {
    this.router.navigate(['/admin/productForm', product.id]);
  }
}
