import { Component, Input, OnInit } from '@angular/core';
import { ICharacter } from '../service/character-service/character.service';

@Component({
  selector: 'app-character-card',
  templateUrl: './character-card.component.html',
  styleUrls: ['./character-card.component.css']
})
export class CharacterCardComponent implements OnInit {
  @Input() character: ICharacter | any;
  
  constructor() { }

  ngOnInit(): void {
  }
}
