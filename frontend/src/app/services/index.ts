import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class WeatherService {

  private apiUrl = 'http://localhost:5000/api/weather';

  constructor(private http: HttpClient) { }

  consultarCidade(cidade: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/consultar?cidade=${cidade}`, {});
  }

  buscarPorPeriodo(cidade: string, inicio: string, fim: string): Observable<any> {
    let params = new HttpParams()
      .set('cidade', cidade)
      .set('inicio', inicio)
      .set('fim', fim);

    return this.http.get(`${this.apiUrl}/periodo`, { params });
  }

  obterEstatisticasHoje(cidade: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/estatisticas-hoje?cidade=${cidade}`);
  }
}