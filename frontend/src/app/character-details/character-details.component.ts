import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ICharacterData } from '../character-list/character-list.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-character-details',
  templateUrl: './character-details.component.html',
  styleUrls: ['./character-details.component.css']
})
export class CharacterDetailsComponent implements OnInit {

  character: ICharacterData | undefined

  constructor(private route: ActivatedRoute, public http: HttpClient) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap
    const characterIdFromRoute = Number(routeParams.get('characterId'))

    this.http
      .get<any>(`https://localhost:7196/api/characters/${characterIdFromRoute}`)
      .subscribe((data) => {
        console.log(data)
        this.character = data.data
      });
  }
}
