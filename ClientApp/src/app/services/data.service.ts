import { Injectable, OnDestroy } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DataService implements OnDestroy {
  private dataSubject = new Subject<any>();
  private isCalled = false;
  private destroy$ = new Subject<void>();

  constructor() {}

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  updateData(data: any) {
    this.dataSubject.next(data);
  }

  observeData(): Observable<any> {
    if (!this.isCalled) {
      this.isCalled = true;
      return this.dataSubject.pipe(takeUntil(this.destroy$));
    } else {
      throw new Error('observeData() solo se puede llamar una vez en cada componente.');
    }
  }
}