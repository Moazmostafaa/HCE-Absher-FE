import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PostFilesComponent } from './post-files.component';

describe('PostFilesComponent', () => {
  let component: PostFilesComponent;
  let fixture: ComponentFixture<PostFilesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PostFilesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PostFilesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
