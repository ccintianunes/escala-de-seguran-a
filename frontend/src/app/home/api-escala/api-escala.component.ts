import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EscalaService } from '../../services/escala.service'; 
import { EscalaDTO } from '../../models/escala-dto'; 

@Component({
  selector: 'app-escala',
  standalone: true,
  templateUrl: './escala.component.html',
  styleUrls: ['./escala.component.css']
})
export class ApiEscalaComponent implements OnInit {
  escalaId: number | null = null;
  escala: EscalaDTO | null = null;

  constructor(
    private route: ActivatedRoute,
    private escalaService: EscalaService
  ) {}

  ngOnInit(): void {
    // Obtemos o id da rota
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.escalaId = +id;
        this.getEscala(this.escalaId);
      }
    });
  }

  getEscala(id: number): void {
    this.escalaService.getEscala(id).subscribe(
      (data: EscalaDTO) => {
        this.escala = data;
      },
      error => {
        console.error('Erro ao obter a escala', error);
      }
    );
  }
}
