import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LookupService {
  constructor(private http: HttpClient) {}

  getCategories(): Observable<any> {
    return this.http.get('/api/Common/CategoryLookup');
  }

  getBrands(): Observable<any> {
    return this.http.get('/api/Common/BrandLookup');
  }
}
