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

  constructor(private policialService: PolicialService) {}

  ngOnInit(): void {
    this.policialService.getPoliciais().subscribe(data => {
      this.policiais = data;
    });
  }

  navigateTo(route: string) {
    window.location.href = route;
  }
}
