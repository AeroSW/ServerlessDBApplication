import { TestBed } from '@angular/core/testing';

import { CatBreedService } from './cat-breed.service';

describe('CatService', () => {
  let service: CatBreedService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CatBreedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
