import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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
  data: ICharacterData | ICharacterData[] | null
  success: boolean
  message: string | null
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
    return this.http.get<ICharacterResponse>(CHARACTERS_API + Number(characterId));
  }
}
