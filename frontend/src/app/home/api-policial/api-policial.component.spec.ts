import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiPolicialComponent } from './api-policial.component';

describe('ApiPolicialComponent', () => {
  let component: ApiPolicialComponent;
  let fixture: ComponentFixture<ApiPolicialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApiPolicialComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApiPolicialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
