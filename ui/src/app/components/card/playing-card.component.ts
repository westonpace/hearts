import { Component, Input, OnChanges } from '@angular/core';

import { PlayingCard } from 'src/app/model/playing-card';

@Component({
  selector: 'jop-card',
  templateUrl: './playing-card.component.html'
})
export class PlayingCardComponent implements OnChanges {

  private static NUMBERS = ['Ace', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten', 'Jack', 'Queen', 'King'];

  @Input()
  card!: PlayingCard;
  numberWord = 'Error';

  ngOnChanges() {
    if (!this.card) {
      throw new Error('card is a required field');
    }
    this.numberWord = PlayingCardComponent.NUMBERS[this.card.rank - 1];
  }

}
