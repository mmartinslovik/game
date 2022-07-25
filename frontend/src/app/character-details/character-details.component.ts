import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { HttpClient } from '@angular/common/http';
import {
  CharacterService,
  ICharacterData,
} from '../service/character-service/character.service';

@Component({
  selector: 'app-character-details',
  templateUrl: './character-details.component.html',
  styleUrls: ['./character-details.component.css'],
})
export class CharacterDetailsComponent implements OnInit {
  character: ICharacterData | any;

  constructor(
    private route: ActivatedRoute,
    private characterService: CharacterService
  ) {}

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    const characterIdFromRoute = routeParams.get('characterId');

    this.characterService.getById(characterIdFromRoute).subscribe((data) => {
      this.character = data.data;
    });
  }
}
