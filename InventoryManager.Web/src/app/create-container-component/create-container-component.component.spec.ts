import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateContainerComponentComponent } from './create-container-component.component';

describe('CreateContainerComponentComponent', () => {
  let component: CreateContainerComponentComponent;
  let fixture: ComponentFixture<CreateContainerComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateContainerComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateContainerComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
