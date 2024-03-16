import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../Models/ProductModel';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private http: HttpClient) {}

  productsList(
    pageNumber: number,
    pageSize: number,
    productName: string | null,
    brand: number | null,
    category: number | null
  ): Observable<any> {
    let url = `/api/Customer/ProductList?PageNumber=${pageNumber}&PageSize=${pageSize}&ProductName=${productName}`;
    if (brand !== null) {
      url += `&BrandId=${brand}`;
    }

    if (category !== null) {
      url += `&CategoryId=${category}`;
    }
    return this.http.get<any>(url);
  }

  addToCart(prodcut: any): Observable<any> {
    return this.http.post<any>('/api/Customer/AddToCart', prodcut);
  }

  removeFromCart(prodcut: any): Observable<any> {
    return this.http.post<any>('/api/Customer/RemoveFromCart', prodcut);
  }

  cart(): Observable<any> {
    return this.http.get<any>('/api/Customer/Cart');
  }

  placeOrder(): Observable<any> {
    return this.http.post<any>('/api/Customer/PlaceOrder', null);
  }

  cancelOrder(order: any): Observable<any> {
    return this.http.post<any>('/api/Customer/CancelOrder', order);
  }

  orders(pageNumber: number, pageSize: number): Observable<any> {
    return this.http.get<any>(
      `/api/Customer/OrderList?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }
}
