import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CharacterService,
  ICharacterData,
  ICharacterResponse,
} from '../service/character-service/character.service';

export enum ClassNumber {
  'Classless',
  'Rogue',
  'Mage',
  'Warrior',
}

export enum ClassColor {
  'bg-white',
  'bg-indigo-700',
  'bg-blue-700',
  'bg-red-800',
}

export interface ICharacter extends ICharacterData {
  color: string;
  className: string;
}

@Component({
  selector: 'app-character-list',
  templateUrl: './character-list.component.html',
  styleUrls: ['./character-list.component.css'],
})
export class CharacterListComponent implements OnInit {
  @Input() characters: ICharacter[] | any;

  constructor(private characterService: CharacterService) {}

  ngOnInit(): void {
    this.characterService.getAll().subscribe((response) => {
      this.characters = response.data;
      this.characters.forEach((ch: ICharacter) => {
        ch.className = ClassNumber[Number(ch.class)];
        ch.color = ClassColor[Number(ch.class)];
      });
    });
  }
}
