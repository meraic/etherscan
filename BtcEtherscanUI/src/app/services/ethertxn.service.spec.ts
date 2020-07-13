import { TestBed } from '@angular/core/testing';

import { EthertxnService } from './ethertxn.service';

describe('EthertxnService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: EthertxnService = TestBed.get(EthertxnService);
    expect(service).toBeTruthy();
  });
});
