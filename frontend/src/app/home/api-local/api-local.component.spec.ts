import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiLocalComponent } from './api-local.component';

describe('ApiLocalComponent', () => {
  let component: ApiLocalComponent;
  let fixture: ComponentFixture<ApiLocalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApiLocalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ApiLocalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
