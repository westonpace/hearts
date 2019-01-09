import { TestBed, async } from '@angular/core/testing';
import { PlayingCardComponent } from './playing-card.component';

describe('PlayingCardComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        PlayingCardComponent
      ],
    }).compileComponents();
  }));

  it('should display a playing card correctly', () => {
    const fixture = TestBed.createComponent(PlayingCardComponent);
    const playingCard = fixture.debugElement.componentInstance as PlayingCardComponent;
    expect(playingCard).toBeTruthy();
    if (playingCard) {
        playingCard.card = { rank: 3, suit: 'hearts' };
        playingCard.ngOnChanges();
        fixture.detectChanges();
        expect(playingCard.numberWord).toBe('Three');
        expect(fixture.debugElement.nativeElement.textContent).toContain('Three of hearts');
    }
  });

});
