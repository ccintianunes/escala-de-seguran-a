import { Component, OnInit } from '@angular/core';
import { LocalService } from '../../services/local.service';
import { LocalDTO } from '../../models/escala-dto';

@Component({
  selector: 'app-local',
  templateUrl: './local.component.html',
  styleUrls: ['./local.component.css']
})
export class ApiLocalComponent implements OnInit {
  locais: LocalDTO[] = [];
  selectedLocal: LocalDTO | null = null;

  constructor(private localService: LocalService) {}

  ngOnInit(): void {
    this.getLocais();
  }

  getLocais(): void {
    this.localService.getLocais().subscribe(locais => this.locais = locais);
  }

  selectLocal(local: LocalDTO): void {
    this.selectedLocal = local;
  }

  saveLocal(local: LocalDTO): void {
    if (local.localId) {
      this.localService.updateLocal(local.localId, local).subscribe(() => this.getLocais());
    } else {
      this.localService.createLocal(local).subscribe(() => this.getLocais());
    }
    this.selectedLocal = null;
  }

  deleteLocal(id: number): void {
    this.localService.deleteLocal(id).subscribe(() => this.getLocais());
  }
}
