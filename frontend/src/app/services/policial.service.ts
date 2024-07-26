import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PolicialDTO } from '../models/escala-dto';

@Injectable({
  providedIn: 'root'
})
export class PolicialService {
  private apiUrl = 'http://localhost:5115/api/Policial';

  constructor(private http: HttpClient) {}

  getPoliciais(): Observable<PolicialDTO[]> {
    return this.http.get<PolicialDTO[]>(this.apiUrl);
  }

  getPolicial(id: number): Observable<PolicialDTO> {
    return this.http.get<PolicialDTO>(`${this.apiUrl}/${id}`);
  }

  createPolicial(policial: PolicialDTO): Observable<PolicialDTO> {
    return this.http.post<PolicialDTO>(this.apiUrl, policial);
  }

  updatePolicial(id: number, policial: PolicialDTO): Observable<PolicialDTO> {
    return this.http.put<PolicialDTO>(`${this.apiUrl}/${id}`, policial);
  }

  deletePolicial(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
