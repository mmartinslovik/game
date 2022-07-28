import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CharacterService,
  ICharacter,
} from '../service/character-service/character.service';

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
        this.characterService.setAdditional(ch);
      });
    });
  }
}
