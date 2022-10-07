import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BuyerDdComponent } from './buyer-dd.component';

describe('BuyerDdComponent', () => {
  let component: BuyerDdComponent;
  let fixture: ComponentFixture<BuyerDdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BuyerDdComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BuyerDdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
