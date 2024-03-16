import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../Models/ProductModel';

@Injectable({
  providedIn: 'root',
})
export class AdminServicesService {
  constructor(private http: HttpClient) {}

  productsList(pageNumber: number, pageSize: number): Observable<any> {
    return this.http.get<any>(
      `/api/Admin/ProductList?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  product(id: string): Observable<any> {
    return this.http.get<any>(`/api/Admin/Product/${id}`);
  }

  addUpdateProduct(product: Product): Observable<any> {
    return this.http.post<any>('/api/Admin/UpsertProduct', product);
  }

  categoryList(pageNumber: number, pageSize: number): Observable<any> {
    return this.http.get<any>(
      `/api/Admin/CategoryList?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  brandList(pageNumber: number, pageSize: number): Observable<any> {
    return this.http.get<any>(
      `/api/Admin/BrandList?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  addUpdateCategory(category: any): Observable<any> {
    return this.http.post<any>('/api/Admin/UpsertCategory', category);
  }

  addUpdateBrand(brand: any): Observable<any> {
    return this.http.post<any>('/api/Admin/UpsertBrand', brand);
  }

  orderList(pageNumber: number, pageSize: number): Observable<any> {
    return this.http.get<any>(
      `/api/Admin/OrderList?pageNumber=${pageNumber}&pageSize${pageSize}`
    );
  }
}
