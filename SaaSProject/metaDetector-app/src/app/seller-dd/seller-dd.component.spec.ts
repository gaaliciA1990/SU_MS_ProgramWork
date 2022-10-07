import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerDdComponent } from './seller-dd.component';

describe('SellerDdComponent', () => {
  let component: SellerDdComponent;
  let fixture: ComponentFixture<SellerDdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SellerDdComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerDdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
