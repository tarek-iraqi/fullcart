import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerSideMenuComponent } from './customer-side-menu.component';

describe('CustomerSideMenuComponent', () => {
  let component: CustomerSideMenuComponent;
  let fixture: ComponentFixture<CustomerSideMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomerSideMenuComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CustomerSideMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
