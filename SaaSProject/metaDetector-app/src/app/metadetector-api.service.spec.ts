import { TestBed } from '@angular/core/testing';

import { MetadetectorApiService } from './metadetector-api.service';

describe('MetadetectorApiService', () => {
  let service: MetadetectorApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MetadetectorApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
