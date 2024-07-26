import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiEscalaComponent } from './api-escala.component';

describe('ApiEscalaComponent', () => {
  let component: ApiEscalaComponent;
  let fixture: ComponentFixture<ApiEscalaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApiEscalaComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApiEscalaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
