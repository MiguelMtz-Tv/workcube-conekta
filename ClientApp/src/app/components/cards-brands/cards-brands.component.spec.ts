import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardsBrandsComponent } from './cards-brands.component';

describe('CardsBrandsComponent', () => {
  let component: CardsBrandsComponent;
  let fixture: ComponentFixture<CardsBrandsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardsBrandsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardsBrandsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
