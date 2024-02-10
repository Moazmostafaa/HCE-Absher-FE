import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserschatsComponent } from './userschats.component';

describe('UserschatsComponent', () => {
  let component: UserschatsComponent;
  let fixture: ComponentFixture<UserschatsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserschatsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserschatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
