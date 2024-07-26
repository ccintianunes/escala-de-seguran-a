import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EscalaDTO } from '../models/escala-dto';

@Injectable({
  providedIn: 'root'
})
export class EscalaService {
  private apiUrl = 'http://localhost:5115/api/Escala';

  constructor(private http: HttpClient) {}

  getEscalas(): Observable<EscalaDTO[]> {
    return this.http.get<EscalaDTO[]>(this.apiUrl);
  }

  getEscala(id: number): Observable<EscalaDTO> {
    return this.http.get<EscalaDTO>(`${this.apiUrl}/${id}`);
  }

  createEscala(escala: EscalaDTO): Observable<EscalaDTO> {
    return this.http.post<EscalaDTO>(this.apiUrl, escala);
  }

  updateEscala(id: number, escala: EscalaDTO): Observable<EscalaDTO> {
    return this.http.put<EscalaDTO>(`${this.apiUrl}/${id}`, escala);
  }

  deleteEscala(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
