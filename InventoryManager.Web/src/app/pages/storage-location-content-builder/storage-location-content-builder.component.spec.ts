import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StorageLocationContentBuilderComponent } from './storage-location-content-builder.component';

describe('StoragecaseContentBuilderComponent', () => {
  let component: StorageLocationContentBuilderComponent;
  let fixture: ComponentFixture<StorageLocationContentBuilderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StorageLocationContentBuilderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StorageLocationContentBuilderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
