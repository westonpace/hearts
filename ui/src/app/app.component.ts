import { Component, OnInit } from '@angular/core';

import { PlayingCard } from './model/playing-card';
import { HeartsClient } from './services/hearts-client';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {

  cards: PlayingCard[] = [];
  shuffleCount = 0;

  constructor(private heartsClient: HeartsClient) {

  }

  ngOnInit() {
    this.heartsClient.getShuffledDeck().subscribe(shuffledDeckResponse => {
      this.cards = shuffledDeckResponse.cards;
      this.shuffleCount = shuffledDeckResponse.shuffleCount;
    });
  }

}
