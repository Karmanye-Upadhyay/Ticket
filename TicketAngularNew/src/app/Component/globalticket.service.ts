import { Injectable } from '@angular/core';
import { ModelClass } from '../model-class';
import {HttpClient}  from '@angular/common/http'
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class GlobalticketService {
  url : string ="http://localhost:5023/api/Ticket/GetAllData"
  uploadUrl : string ="http://localhost:5023/api/Ticket/Ticketdetail";
 // downloadUrl: string ="http://localhost:5222/api/Image/GetMultipleImage"
  //http://localhost:5222/api/Ticket/Comment?id=1
  list : ModelClass[]=[]
 object : ModelClass =new ModelClass() ;
    constructor(private http : HttpClient) { }

  refresh(){
    this.object  =new ModelClass();
  }

  paymentList():Observable<any[]>{
    {
        return this.http.get<any[]>(this.url) 
    }
    }
  


   addData(formData: any) {
  return this.http.post(this.uploadUrl, formData);
}

updateData(formData: any) {
  return this.http.put(this.url, formData);
}

deleteData(formData: any) {
  return this.http.delete(this.url, formData);
}
  
// Download(id:number):Observable<any[]>{
//   {
//       return this.http.get<any[]>(this.downloadUrl+id) 
//   }
// }
}
