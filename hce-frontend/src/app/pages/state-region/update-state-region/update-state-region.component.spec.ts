import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateStateRegionComponent } from './update-state-region.component';

describe('UpdateStateRegionComponent', () => {
  let component: UpdateStateRegionComponent;
  let fixture: ComponentFixture<UpdateStateRegionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateStateRegionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateStateRegionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
