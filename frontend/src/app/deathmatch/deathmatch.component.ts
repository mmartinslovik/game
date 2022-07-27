import { Component, OnInit } from '@angular/core';
import {
  ClassColor,
  ClassNumber,
  ICharacter,
} from '../character-list/character-list.component';
import { ICharacterData } from '../service/character-service/character.service';
import { FightService } from '../service/fight-service/fight.service';

@Component({
  selector: 'app-deathmatch',
  templateUrl: './deathmatch.component.html',
  styleUrls: ['./deathmatch.component.css'],
})
export class DeathmatchComponent implements OnInit {
  fighters: ICharacter[] = [];
  logs: string[] | undefined;

  constructor(private fightService: FightService) {}

  ngOnInit(): void {
    this.fighters = this.fightService.getFighters();
    this.fighters.forEach((f) => {
      f.className = ClassNumber[Number(f.class)];
      f.color = ClassColor[Number(f.class)];
    });
    console.log('fighters', this.fighters);
  }

  public startDeathmatch() {
    this.fightService.startDeathmatch().subscribe((response) => {
      this.logs = response.data.log;
      console.log('log', this.logs);
    });
  }

  public clearDeathmatch() {
    this.fightService.clearDeathmatch();
  }
}
