import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiMarcacaoEscalaComponent } from './api-marcacao-escala.component';

describe('ApiMarcacaoEscalaComponent', () => {
  let component: ApiMarcacaoEscalaComponent;
  let fixture: ComponentFixture<ApiMarcacaoEscalaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApiMarcacaoEscalaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApiMarcacaoEscalaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
