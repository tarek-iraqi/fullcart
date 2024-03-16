import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AdminServicesService } from '../../../services/admin-services.service';
declare var $: any;

@Component({
  selector: 'app-admin-brands',
  templateUrl: './admin-brands.component.html',
  styleUrl: './admin-brands.component.css',
})
export class AdminBrandsComponent implements OnInit {
  constructor(
    private adminService: AdminServicesService,
    private router: Router
  ) {}

  displayedColumns: string[] = ['name', 'numberOfProducts', 'edit'];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit() {
    this.loadData(1, 10);
  }

  loadData(pageNumber: number, pageSize: number) {
    this.adminService.brandList(pageNumber, pageSize).subscribe({
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

  addNewBrand() {
    this.router.navigate(['/admin/brandForm']);
  }

  editBrand(brand: any) {
    this.router.navigate(['/admin/brandForm', brand.id, brand.name]);
  }
}
