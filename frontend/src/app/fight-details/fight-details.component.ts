import { Component, OnInit } from '@angular/core';
import { FightService, ICharacterScore } from '../service/fight-service/fight.service';

@Component({
  selector: 'app-fight-details',
  templateUrl: './fight-details.component.html',
  styleUrls: ['./fight-details.component.css']
})
export class FightDetailsComponent implements OnInit {
  scores: ICharacterScore[] | any; 

  constructor(private fightService: FightService) { }

  ngOnInit(): void {
    this.fightService.getFightScore().subscribe((data) => {
      this.scores = data.data;
    });
  }

}
