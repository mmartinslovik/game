import { Injectable } from '@angular/core';

const TOKEN: string = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJqc29uRGVydWxvIiwicm9sZSI6IlBsYXllciIsIm5iZiI6MTY1ODczMzgxOCwiZXhwIjoxNjU4ODIwMjE4LCJpYXQiOjE2NTg3MzM4MTh9.-6w3uE_x-7P4kmnboeRFuWrRg6Cw-WbiZfi-WLAxjWvlFYrP_MTaXUw8VnIk-hR5kJ2SOJLHxD3cnHJz3Lm8kA"

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() {}

  public getToken(): string | null {
    return TOKEN;
  }
}


