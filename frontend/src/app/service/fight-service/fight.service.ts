import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Input, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { ICharacter } from 'src/app/character-list/character-list.component';
import { ICharacterData } from '../character-service/character.service';

const FIGHT_API = 'https://localhost:7196/api/fight/';
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

export interface ICharacterScore {
  id: number;
  name: string;
  fights: number;
  victories: number;
  defeats: number;
}

export interface IFightResponse {
  data: ICharacterScore[] | null;
  success: boolean;
  message: string | null;
}

export interface IDeathmatchResponse {
  data: {
    log: string[];
  };
  success: boolean;
  message: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class FightService {
  fighters: ICharacter[] | any = [];

  constructor(private http: HttpClient) {}

  public getFightScore(): Observable<IFightResponse> {
    return this.http.get<IFightResponse>(FIGHT_API + 'score');
  }

  public addToDeathmatch(fighter: ICharacter): ICharacter[] {
    this.fighters?.push(fighter);
    return this.fighters;
  }

  public clearDeathmatch(): [] {
    this.fighters = [];
    return this.fighters;
  }

  public getFighters(): ICharacter[] | [] {
    return this.fighters;
  }

  public saveFighters(): void {
    let characterIds: number[] = [];
    let fighters: ICharacter[] = [];
    this.getFighters().forEach((f) => {
      characterIds.push(f.id);
      fighters.push(f);
    });

    localStorage.setItem('characterids', JSON.stringify(characterIds));
    localStorage.setItem('fighters', JSON.stringify(fighters));
  }

  public startDeathmatch(): Observable<IDeathmatchResponse> {
    return this.http.post<IDeathmatchResponse>(FIGHT_API, {
      characterids: JSON.parse(localStorage.getItem('characterids') || ''),
    });
  }
}
