import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateStorageLocationComponent } from './create-storage-location.component';

describe('CreateStorageLocationComponent', () => {
  let component: CreateStorageLocationComponent;
  let fixture: ComponentFixture<CreateStorageLocationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateStorageLocationComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateStorageLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
