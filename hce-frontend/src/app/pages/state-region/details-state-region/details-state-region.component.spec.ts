import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsStateRegionComponent } from './details-state-region.component';

describe('DetailsStateRegionComponent', () => {
  let component: DetailsStateRegionComponent;
  let fixture: ComponentFixture<DetailsStateRegionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailsStateRegionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsStateRegionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
