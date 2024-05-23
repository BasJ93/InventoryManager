import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContentsOverviewComponent } from './contents-overview.component';

describe('ContentsOverviewComponentComponent', () => {
  let component: ContentsOverviewComponent;
  let fixture: ComponentFixture<ContentsOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ContentsOverviewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ContentsOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
