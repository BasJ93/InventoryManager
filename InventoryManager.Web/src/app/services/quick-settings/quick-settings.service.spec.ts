import { TestBed } from '@angular/core/testing';

import { QuickSettingsService } from './quick-settings.service';

describe('QuickSettingsService', () => {
  let service: QuickSettingsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuickSettingsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
