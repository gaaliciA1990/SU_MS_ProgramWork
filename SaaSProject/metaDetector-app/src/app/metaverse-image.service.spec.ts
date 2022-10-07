import { TestBed } from '@angular/core/testing';

import { MetaverseImageService } from './metaverse-image.service';

describe('MetaverseImageService', () => {
  let service: MetaverseImageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MetaverseImageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
