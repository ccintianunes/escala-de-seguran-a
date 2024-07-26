import { TestBed } from '@angular/core/testing';

import { MarcacaoEscalaService } from './marcacao-escala.service';

describe('MarcacaoEscalaService', () => {
  let service: MarcacaoEscalaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MarcacaoEscalaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
