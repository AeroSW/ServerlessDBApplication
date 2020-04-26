import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PetsFooterComponent } from './pets-footer.component';

describe('PetsFooterComponent', () => {
  let component: PetsFooterComponent;
  let fixture: ComponentFixture<PetsFooterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PetsFooterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PetsFooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
