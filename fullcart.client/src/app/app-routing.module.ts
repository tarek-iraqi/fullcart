import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminLayoutComponent } from './components/admin-module/admin-layout/admin-layout.component';
import { DashboardComponent } from './components/admin-module/dashboard/dashboard.component';
import { AdminProductsComponent } from './components/admin-module/admin-products/admin-products.component';
import { AdminProductFormComponent } from './components/admin-module/admin-product-form/admin-product-form.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { AdminCategoriesComponent } from './components/admin-module/admin-categories/admin-categories.component';
import { AdminCategoryFormComponent } from './components/admin-module/admin-category-form/admin-category-form.component';
import { AdminBrandsComponent } from './components/admin-module/admin-brands/admin-brands.component';
import { AdminBrandFormComponent } from './components/admin-module/admin-brand-form/admin-brand-form.component';
import { AdminOrdersComponent } from './components/admin-module/admin-orders/admin-orders.component';
import { CustomerLayoutComponent } from './components/customer-module/customer-layout/customer-layout.component';
import { CustomerProductsComponent } from './components/customer-module/customer-products/customer-products.component';
import { CustomerCartComponent } from './components/customer-module/customer-cart/customer-cart.component';
import { CustomerOrdersComponent } from './components/customer-module/customer-orders/customer-orders.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: 'admin',
    component: AdminLayoutComponent,
    children: [
      { path: 'home', component: DashboardComponent },
      { path: 'productList', component: AdminProductsComponent },
      { path: 'productForm', component: AdminProductFormComponent },
      { path: 'productForm/:id', component: AdminProductFormComponent },
      { path: 'categoryList', component: AdminCategoriesComponent },
      { path: 'categoryForm', component: AdminCategoryFormComponent },
      { path: 'categoryForm/:id/:name', component: AdminCategoryFormComponent },
      { path: 'brandList', component: AdminBrandsComponent },
      { path: 'brandForm', component: AdminBrandFormComponent },
      { path: 'brandForm/:id/:name', component: AdminBrandFormComponent },
      { path: 'orderList', component: AdminOrdersComponent },
    ],
  },
  {
    path: 'customer',
    component: CustomerLayoutComponent,
    children: [
      { path: 'home', component: CustomerProductsComponent },
      { path: 'productList', component: CustomerProductsComponent },
      { path: 'cart', component: CustomerCartComponent },
      { path: 'orderList', component: CustomerOrdersComponent },
    ],
  },
  { path: '**', component: PageNotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
