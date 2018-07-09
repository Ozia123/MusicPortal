import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FilteredArtistsComponent } from './filtered-artists.component';

describe('FilteredArtistsComponent', () => {
  let component: FilteredArtistsComponent;
  let fixture: ComponentFixture<FilteredArtistsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FilteredArtistsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FilteredArtistsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
