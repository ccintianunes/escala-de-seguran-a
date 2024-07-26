import { Component, OnInit } from '@angular/core';
import { PolicialService } from '../../services/policial.service';
import { PolicialDTO } from '../../models/escala-dto';

@Component({
  selector: 'app-policial',
  templateUrl: './policial.component.html',
  styleUrls: ['./policial.component.css']
})
export class ApiPolicialComponent implements OnInit {
  policiais: PolicialDTO[] = [];
  selectedPolicial: PolicialDTO | null = null;

  constructor(private policialService: PolicialService) {}

  ngOnInit(): void {
    this.getPoliciais();
  }

  getPoliciais(): void {
    this.policialService.getPoliciais().subscribe(policiais => this.policiais = policiais);
  }

  selectPolicial(policial: PolicialDTO): void {
    this.selectedPolicial = policial;
  }

  savePolicial(policial: PolicialDTO): void {
    if (policial.Id) {
      this.policialService.updatePolicial(policial.Id, policial).subscribe(() => this.getPoliciais());
    } else {
      this.policialService.createPolicial(policial).subscribe(() => this.getPoliciais());
    }
    this.selectedPolicial = null;
  }

  deletePolicial(id: number): void {
    this.policialService.deletePolicial(id).subscribe(() => this.getPoliciais());
  }
}
