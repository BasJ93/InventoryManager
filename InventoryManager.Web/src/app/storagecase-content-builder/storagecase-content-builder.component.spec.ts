import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StoragecaseContentBuilderComponent } from './storagecase-content-builder.component';

describe('StoragecaseContentBuilderComponent', () => {
  let component: StoragecaseContentBuilderComponent;
  let fixture: ComponentFixture<StoragecaseContentBuilderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StoragecaseContentBuilderComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StoragecaseContentBuilderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
