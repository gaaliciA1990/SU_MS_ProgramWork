import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetaMarketplaceComponent } from './meta-marketplace.component';

describe('MetaMarketplaceComponent', () => {
  let component: MetaMarketplaceComponent;
  let fixture: ComponentFixture<MetaMarketplaceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MetaMarketplaceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MetaMarketplaceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
