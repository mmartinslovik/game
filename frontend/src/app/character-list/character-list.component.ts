import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CharacterService,
  ICharacterData,
  ICharacterResponse
} from '../service/character-service/character.service';

@Component({
  selector: 'app-character-list',
  templateUrl: './character-list.component.html',
  styleUrls: ['./character-list.component.css'],
})
export class CharacterListComponent implements OnInit {
  characters: ICharacterData[] | any;

  constructor(private characterService: CharacterService) {}

  ngOnInit(): void {
    this.characterService.getAll().subscribe((response) => {
      this.characters = response.data;
    });
  }
}
