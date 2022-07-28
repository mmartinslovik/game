import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { HttpClient } from '@angular/common/http';
import {
  CharacterService,
  ICharacter,
  ICharacterData,
} from '../service/character-service/character.service';
import { FightService } from '../service/fight-service/fight.service';

@Component({
  selector: 'app-character-details',
  templateUrl: './character-details.component.html',
  styleUrls: ['./character-details.component.css'],
})
export class CharacterDetailsComponent implements OnInit, OnDestroy {
  @Input() character: ICharacter | any;

  constructor(
    private route: ActivatedRoute,
    private characterService: CharacterService,
    private fightService: FightService
  ) {}

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const characterIdFromRoute = routeParams.get('characterId');

    this.characterService
      .getById(characterIdFromRoute)
      .subscribe((response) => {
        this.character = response.data;
        this.characterService.setAdditional(this.character);
        console.log(this.character.color)
      });
  }

  ngOnDestroy(): void {
    this.fightService.saveFighters();
  }

  public addToDeathmatch(character: ICharacter): void {
    this.fightService.addToDeathmatch(character);
    console.log(this.fightService.getFighters());
  }
}
