import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContainersOverviewComponent } from './containers-overview.component';

describe('ContainersOverviewComponentComponent', () => {
  let component: ContainersOverviewComponent;
  let fixture: ComponentFixture<ContainersOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContainersOverviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ContainersOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
