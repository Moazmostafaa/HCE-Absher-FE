import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupschatsComponent } from './groupschats.component';

describe('GroupschatsComponent', () => {
  let component: GroupschatsComponent;
  let fixture: ComponentFixture<GroupschatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupschatsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupschatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
