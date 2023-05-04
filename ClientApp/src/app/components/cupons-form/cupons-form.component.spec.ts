import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CuponsFormComponent } from './cupons-form.component';

describe('CuponsFormComponent', () => {
  let component: CuponsFormComponent;
  let fixture: ComponentFixture<CuponsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CuponsFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CuponsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
