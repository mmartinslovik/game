import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { HttpClient } from '@angular/common/http';
import {
  CharacterService,
  ICharacterData,
} from '../service/character-service/character.service';
import { FightService } from '../service/fight-service/fight.service';

@Component({
  selector: 'app-character-details',
  templateUrl: './character-details.component.html',
  styleUrls: ['./character-details.component.css'],
})
export class CharacterDetailsComponent implements OnInit, OnDestroy {
  character: ICharacterData | any;

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
      });
  }

  ngOnDestroy(): void {
    this.fightService.saveFighters()
  }

  public addToDeathmatch(character: ICharacterData): void {
    this.fightService.addToDeathmatch(character);
    console.log(this.fightService.getFighters());
    window.alert('Character added to the deathmatch!');
  }
}
