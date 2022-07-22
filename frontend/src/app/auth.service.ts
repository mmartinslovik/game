import { Injectable } from '@angular/core';

const TOKEN: string = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJqc29uRGVydWxvIiwicm9sZSI6IlBsYXllciIsIm5iZiI6MTY1ODQ5NTc5NSwiZXhwIjoxNjU4NTgyMTk1LCJpYXQiOjE2NTg0OTU3OTV9.XhJy4bOEs0l-jRQ5rqfoYYiRngN8XyWCWEA-colZHPni3Afr0g_uHtAsftdTWgQT85Ky_FzaufchTHiHNQl7Kg"

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() {}

  public getToken(): string | null {
    return TOKEN;
  }
}


