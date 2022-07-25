import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FightDetailsComponent } from './fight-details.component';

describe('FightDetailsComponent', () => {
  let component: FightDetailsComponent;
  let fixture: ComponentFixture<FightDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FightDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FightDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
