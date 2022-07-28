import { Component, OnInit } from '@angular/core';
import { CharacterService, ICharacter, ICharacterData } from '../service/character-service/character.service';
import { FightService } from '../service/fight-service/fight.service';

@Component({
  selector: 'app-deathmatch',
  templateUrl: './deathmatch.component.html',
  styleUrls: ['./deathmatch.component.css'],
})
export class DeathmatchComponent implements OnInit {
  fighters: ICharacter[] = [];
  logs: string[] | undefined;

  constructor(private fightService: FightService, private characterService: CharacterService) {}

  ngOnInit(): void {
    this.fighters = this.fightService.getFighters();
    this.fighters.forEach((f) => {
      this.characterService.setAdditional(f)
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
    this.fighters = this.fightService.clearDeathmatch();
  }
}
