import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { AdminServicesService } from '../../../services/admin-services.service';
declare var $: any;

@Component({
  selector: 'app-admin-categories',
  templateUrl: './admin-categories.component.html',
  styleUrl: './admin-categories.component.css',
})
export class AdminCategoriesComponent implements OnInit {
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
    this.adminService.categoryList(pageNumber, pageSize).subscribe({
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

  addNewCategory() {
    this.router.navigate(['/admin/categoryForm']);
  }

  editCategory(category: any) {
    this.router.navigate(['/admin/categoryForm', category.id, category.name]);
  }
}
