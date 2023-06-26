import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPdfPaymentComponent } from './view-pdf-payment.component';

describe('ViewPdfPaymentComponent', () => {
  let component: ViewPdfPaymentComponent;
  let fixture: ComponentFixture<ViewPdfPaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewPdfPaymentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewPdfPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
