import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficesListsComponent } from './offices-lists.component';

describe('OfficesListsComponent', () => {
  let component: OfficesListsComponent;
  let fixture: ComponentFixture<OfficesListsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OfficesListsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfficesListsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
