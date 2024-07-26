import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocalDTO } from '../models/escala-dto';

@Injectable({
  providedIn: 'root'
})
export class LocalService {
  private apiUrl = 'http://seu-api-url/api/Local';

  constructor(private http: HttpClient) { }

  getLocais(): Observable<LocalDTO[]> {
    return this.http.get<LocalDTO[]>(this.apiUrl);
  }

  getLocal(id: number): Observable<LocalDTO> {
    return this.http.get<LocalDTO>(`${this.apiUrl}/${id}`);
  }

  createLocal(local: LocalDTO): Observable<LocalDTO> {
    return this.http.post<LocalDTO>(this.apiUrl, local);
  }

  updateLocal(id: number, local: LocalDTO): Observable<LocalDTO> {
    return this.http.put<LocalDTO>(`${this.apiUrl}/${id}`, local);
  }

  deleteLocal(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
