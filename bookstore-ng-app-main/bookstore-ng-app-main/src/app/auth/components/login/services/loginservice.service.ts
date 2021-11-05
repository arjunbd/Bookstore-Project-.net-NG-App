import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class LoginserviceService {

  constructor(private http:HttpClient) { }
  onAdminLogin(adminformData:any){
    return this.http.get('http://localhost:64107/api/Admin',adminformData)
    .pipe(map((res:any)=>{
      return res;
    }));
  }
}
