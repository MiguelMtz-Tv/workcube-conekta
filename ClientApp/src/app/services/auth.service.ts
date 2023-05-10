import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  setUserSession(){
  }

  getUserSession(){
    const dataStrig = localStorage.getItem('user')
    const user = JSON.parse(dataStrig!)
    return user
  }

  dropUserSession(){
    localStorage.removeItem('user')
  }
}
