import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PetsHeaderComponent } from './pets-header.component';

describe('PetsHeaderComponent', () => {
  let component: PetsHeaderComponent;
  let fixture: ComponentFixture<PetsHeaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PetsHeaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PetsHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
