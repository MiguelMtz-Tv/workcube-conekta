import { Injectable } from '@angular/core';
import { getBaseUrl } from 'src/main';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ServiciosService {
  baseUrl: string = getBaseUrl()

  constructor() { }
}
