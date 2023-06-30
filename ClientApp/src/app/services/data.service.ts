import { Injectable, OnDestroy } from '@angular/core';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private dataSubject = new Subject<any>();

  constructor() {}

  updateData(data: any) {
    this.dataSubject.next(data);
  }

  observeData(): Observable<any> {
      return this.dataSubject;
  }
}