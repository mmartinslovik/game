import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export interface ICharacterData {
  id: number
  name: string
  hitPoints: number
  strength: number
  defense: number
  intelligence: number
  class: number
  weapon: IWeaponData | {}
  skills: ISkillData[] | []
}

export interface IWeaponData {
  name: string
  damage: number
}

export interface ISkillData {
  name: string
  damage: number
}

@Component({
  selector: 'app-character-list',
  templateUrl: './character-list.component.html',
  styleUrls: ['./character-list.component.css'],
})
export class CharacterListComponent implements OnInit {
  characters: ICharacterData[] | undefined;

  constructor(public http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<any>('https://localhost:7196/api/characters')
      .subscribe((data) => {
        this.characters = data.data
      });
  }
}
