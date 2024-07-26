import { Component, OnInit } from '@angular/core';
import { MarcacaoEscalaService } from '../../services/marcacao-escala.service';
import { MarcacaoEscalaDTO } from '../../models/escala-dto';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  marcacoesEscala: MarcacaoEscalaDTO[] = [];

  constructor(private marcacaoEscalaService: MarcacaoEscalaService) {}

  ngOnInit(): void {
    this.marcacaoEscalaService.getMarcacaoEscalas().subscribe(data => {
      this.marcacoesEscala = data;
    });
  }
}
