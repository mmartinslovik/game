import { Component, OnInit } from '@angular/core';
import { ICharacterData } from '../service/character-service/character.service';
import { FightService } from '../service/fight-service/fight.service';

@Component({
  selector: 'app-deathmatch',
  templateUrl: './deathmatch.component.html',
  styleUrls: ['./deathmatch.component.css'],
})
export class DeathmatchComponent implements OnInit {
  fighters: ICharacterData[] = this.fightService.getFighters();
  fightLog: string | undefined;

  constructor(private fightService: FightService) {}

  ngOnInit(): void {
    console.log(this.fighters)
  }

  public startDeathmatch() {
    this.fightService.startDeathmatch().subscribe((response) => {
      this.fightLog = response.data.log;
    });
  }
}
