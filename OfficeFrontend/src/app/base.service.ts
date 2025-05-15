import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  officesApi="https://p161-7ddfd-default-rtdb.europe-west1.firebasedatabase.app/offices.json"
  constructor(private http:HttpClient) { }
    getOffices(){
    return this.http.get(this.officesApi)
  }
  postOfficeRental(body:any){
    return this.http.post("https://localhost:7086/api/Berlesek", body)
  }
}
