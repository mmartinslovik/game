import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as data from '../../../assets/data.json';

const CHARACTERS_API = 'https://localhost:7196/api/characters/';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

export interface ICharacterData {
  id: number;
  name: string;
  hitPoints: number;
  strength: number;
  defense: number;
  intelligence: number;
  class: number;
  weapon: IWeaponData | {};
  skills: ISkillData[] | [];
}

export interface IWeaponData {
  name: string;
  damage: number;
}

export interface ISkillData {
  name: string;
  damage: number;
}

export interface ICharacterResponse {
  data: ICharacterData | ICharacterData[] | null;
  success: boolean;
  message: string | null;
}

export interface ICharacter extends ICharacterData {
  color: string;
  className: string;
  image: string
}

interface IJsonData {
  class: string,
  color: string,
  image: string,

}

@Injectable({
  providedIn: 'root',
})
export class CharacterService {
  response: any;

  constructor(private http: HttpClient) {}

  public getAll(): Observable<ICharacterResponse> {
    return this.http.get<ICharacterResponse>(CHARACTERS_API);
  }

  public getById(characterId: string | null): Observable<ICharacterResponse> {
    return this.http.get<ICharacterResponse>(
      CHARACTERS_API + Number(characterId)
    );
  }

  public setAdditional(character: ICharacter): ICharacter {
    // @ts-ignore
    const characterData: IJsonData = data?.[character.name];

    character.className = characterData.class;
    character.color = characterData.color;
    character.image = characterData.image;

    return character;
  }
}
