import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { By } from '@angular/platform-browser';
import { PlayingCardComponent } from './components/card/playing-card.component';
import { ShuffledDeckResponse } from './model/playing-card';
import { of } from 'rxjs';
import { HeartsClient } from './services/hearts-client';

const mockHeartsClient = {
    getShuffledDeck: () => of(<ShuffledDeckResponse>{ shuffleCount: 3, cards: [{ rank: 3, suit: 'hearts' }, { rank: 4, suit: 'spades' }] })
};

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        PlayingCardComponent
      ],
      providers: [
          {
              provide: HeartsClient,
              useValue: mockHeartsClient
          }
      ]
    }).compileComponents();
  }));

  describe('Basic tests', () => {

    let fixture: ComponentFixture<AppComponent>;

    beforeEach(() => {
        fixture = TestBed.createComponent(AppComponent);
        expect(fixture).toBeTruthy();
        fixture.detectChanges();
    });

    it('Should display a list of cards', () => {
        expect(fixture.debugElement.queryAll(By.css('jop-card')).length).toBe(2);
    });

    it('Should display the shuffle count', () => {
        expect(fixture.debugElement.query(By.css('span')).nativeElement.textContent).toContain('3');
    });

  });

});
