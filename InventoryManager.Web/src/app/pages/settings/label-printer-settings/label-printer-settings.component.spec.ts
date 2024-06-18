import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LabelPrinterSettingsComponent } from './label-printer-settings.component';

describe('LabelPrinterSettingsComponent', () => {
  let component: LabelPrinterSettingsComponent;
  let fixture: ComponentFixture<LabelPrinterSettingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LabelPrinterSettingsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LabelPrinterSettingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
