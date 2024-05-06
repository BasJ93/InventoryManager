import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StoragecasesOverviewComponentComponent } from './storagecases-overview-component.component';

describe('StoragecasesOverviewComponentComponent', () => {
  let component: StoragecasesOverviewComponentComponent;
  let fixture: ComponentFixture<StoragecasesOverviewComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StoragecasesOverviewComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StoragecasesOverviewComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
