import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CuponsDialogComponent } from './cupons-dialog.component';

describe('CuponsDialogComponent', () => {
  let component: CuponsDialogComponent;
  let fixture: ComponentFixture<CuponsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CuponsDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CuponsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
