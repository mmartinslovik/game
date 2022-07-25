import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

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
  data: ICharacterScore[] | null
  success: boolean;
  message: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class FightService {

  constructor(private http: HttpClient) {}

  public getFightScore(): Observable<IFightResponse> {
    return this.http.get<IFightResponse>(FIGHT_API + 'score');
  }
}
