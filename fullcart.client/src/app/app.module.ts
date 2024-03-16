import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { AdminLayoutComponent } from './components/admin-module/admin-layout/admin-layout.component';
import { DashboardComponent } from './components/admin-module/dashboard/dashboard.component';
import { AdminHeaderComponent } from './components/admin-module/admin-header/admin-header.component';
import { AdminSideMenuComponent } from './components/admin-module/admin-side-menu/admin-side-menu.component';

import { JwtInterceptor } from './jwt-interceptor';
import { AdminProductsComponent } from './components/admin-module/admin-products/admin-products.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
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
import { CustomerHeaderComponent } from './components/customer-module/customer-header/customer-header.component';
import { CustomerSideMenuComponent } from './components/customer-module/customer-side-menu/customer-side-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    AdminLayoutComponent,
    DashboardComponent,
    AdminHeaderComponent,
    AdminSideMenuComponent,
    AdminProductsComponent,
    AdminProductFormComponent,
    PageNotFoundComponent,
    AdminCategoriesComponent,
    AdminCategoryFormComponent,
    AdminBrandsComponent,
    AdminBrandFormComponent,
    AdminOrdersComponent,
    CustomerLayoutComponent,
    CustomerProductsComponent,
    CustomerCartComponent,
    CustomerOrdersComponent,
    CustomerHeaderComponent,
    CustomerSideMenuComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatIconModule,
    MatButtonModule,
    MatSnackBarModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true,
    },
    provideAnimationsAsync(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
