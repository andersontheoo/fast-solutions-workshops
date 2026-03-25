import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WorkshopDTO, WorkshopResponseDTO } from '../models/workshop.model';

@Injectable({
  providedIn: 'root'
})
export class WorkshopService {
  private apiUrl = 'http://localhost:5102/api/workshops';

  constructor(private http: HttpClient) { }

  // GET - Listar todos workshops
  getWorkshops(): Observable<WorkshopResponseDTO[]> {
    return this.http.get<WorkshopResponseDTO[]>(this.apiUrl);
  }

  // GET por ID
  getWorkshopById(id: number): Observable<WorkshopResponseDTO> {
    return this.http.get<WorkshopResponseDTO>(`${this.apiUrl}/${id}`);
  }

  // POST - Criar workshop
  createWorkshop(workshop: WorkshopDTO): Observable<WorkshopResponseDTO> {
  return this.http.post<WorkshopResponseDTO>(this.apiUrl, workshop);
}

  // PUT - Atualizar workshop
  updateWorkshop(id: number, workshop: WorkshopDTO): Observable<WorkshopResponseDTO> {
    return this.http.put<WorkshopResponseDTO>(`${this.apiUrl}/${id}`, workshop);
  }

  // DELETE - Remover workshop
  deleteWorkshop(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
