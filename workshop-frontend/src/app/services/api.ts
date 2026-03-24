import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = 'http://localhost:5102';

  constructor(private http: HttpClient) { }

  getWorkshops() {
    return this.http.get(`${this.baseUrl}/workshops`);
  }

  getColaboradores() {
    return this.http.get(`${this.baseUrl}/colaboradores`);
  }
}
