import { Component, OnInit } from '@angular/core';
import { MarcacaoEscalaService } from '../../services/marcacao-escala.service';
import { MarcacaoEscalaDTO } from '../../models/escala-dto';

@Component({
  selector: 'api-marcacao-escala',
  templateUrl: './marcacao-escala.component.html',
  styleUrls: ['./marcacao-escala.component.css']
})
export class ApiMarcacaoEscalaComponent implements OnInit {
  marcacoes: MarcacaoEscalaDTO[] = [];
  selectedMarcacao: MarcacaoEscalaDTO | null = null;

  constructor(private marcacaoEscalaService: MarcacaoEscalaService) {}

  ngOnInit(): void {
    this.getMarcacoes();
  }

  getMarcacoes(): void {
    this.marcacaoEscalaService.getMarcacaoEscalas().subscribe(marcacoes => this.marcacoes = marcacoes);
  }

  selectMarcacao(marcacao: MarcacaoEscalaDTO): void {
    this.selectedMarcacao = marcacao;
  }

  saveMarcacao(marcacao: MarcacaoEscalaDTO): void {
    if (marcacao.marcacaoEscalaId) {
      this.marcacaoEscalaService.updateMarcacaoEscala(marcacao.marcacaoEscalaId, marcacao).subscribe(() => this.getMarcacoes());
    } else {
      this.marcacaoEscalaService.createMarcacaoEscala(marcacao).subscribe(() => this.getMarcacoes());
    }
    this.selectedMarcacao = null;
  }

  deleteMarcacao(id: number): void {
    this.marcacaoEscalaService.deleteMarcacaoEscala(id).subscribe(() => this.getMarcacoes());
  }
}
