import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MarcacaoEscalaDTO } from '../models/escala-dto';

@Injectable({
  providedIn: 'root'
})
export class MarcacaoEscalaService {
  private apiUrl = 'http://seu-api-url/api/MarcacaoEscala';

  constructor(private http: HttpClient) { }

  getMarcacaoEscalas(): Observable<MarcacaoEscalaDTO[]> {
    return this.http.get<MarcacaoEscalaDTO[]>(this.apiUrl);
  }

  getMarcacaoEscala(id: number): Observable<MarcacaoEscalaDTO> {
    return this.http.get<MarcacaoEscalaDTO>(`${this.apiUrl}/${id}`);
  }

  createMarcacaoEscala(marcacaoEscala: MarcacaoEscalaDTO): Observable<MarcacaoEscalaDTO> {
    return this.http.post<MarcacaoEscalaDTO>(this.apiUrl, marcacaoEscala);
  }

  updateMarcacaoEscala(id: number, marcacaoEscala: MarcacaoEscalaDTO): Observable<MarcacaoEscalaDTO> {
    return this.http.put<MarcacaoEscalaDTO>(`${this.apiUrl}/${id}`, marcacaoEscala);
  }

  deleteMarcacaoEscala(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
