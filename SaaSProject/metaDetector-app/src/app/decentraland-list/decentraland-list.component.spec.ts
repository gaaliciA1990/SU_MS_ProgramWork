import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DecentralandListComponent } from './decentraland-list.component';

describe('DecentralandListComponent', () => {
  let component: DecentralandListComponent;
  let fixture: ComponentFixture<DecentralandListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DecentralandListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DecentralandListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
