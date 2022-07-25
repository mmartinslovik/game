import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeathmatchComponent } from './deathmatch.component';

describe('DeathmatchComponent', () => {
  let component: DeathmatchComponent;
  let fixture: ComponentFixture<DeathmatchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeathmatchComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeathmatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
