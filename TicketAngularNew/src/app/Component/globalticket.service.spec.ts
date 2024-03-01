import { TestBed } from '@angular/core/testing';

import { GlobalticketService } from './globalticket.service';

describe('GlobalticketService', () => {
  let service: GlobalticketService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalticketService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
