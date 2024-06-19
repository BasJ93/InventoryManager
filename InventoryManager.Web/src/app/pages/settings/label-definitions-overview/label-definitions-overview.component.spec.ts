import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabelDefinitionsOverviewComponent } from './label-definitions-overview.component';

describe('LabelDefinitionsOverviewComponent', () => {
  let component: LabelDefinitionsOverviewComponent;
  let fixture: ComponentFixture<LabelDefinitionsOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LabelDefinitionsOverviewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LabelDefinitionsOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
