import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContentsOverviewComponentComponent } from './contents-overview-component.component';

describe('ContentsOverviewComponentComponent', () => {
  let component: ContentsOverviewComponentComponent;
  let fixture: ComponentFixture<ContentsOverviewComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContentsOverviewComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ContentsOverviewComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
