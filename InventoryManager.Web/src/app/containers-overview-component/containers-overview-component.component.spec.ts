import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainersOverviewComponentComponent } from './containers-overview-component.component';

describe('ContainersOverviewComponentComponent', () => {
  let component: ContainersOverviewComponentComponent;
  let fixture: ComponentFixture<ContainersOverviewComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContainersOverviewComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ContainersOverviewComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
