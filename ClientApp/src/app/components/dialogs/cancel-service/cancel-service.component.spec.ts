import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CancelServiceComponent } from './cancel-service.component';

describe('CancelServiceComponent', () => {
  let component: CancelServiceComponent;
  let fixture: ComponentFixture<CancelServiceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CancelServiceComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CancelServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
