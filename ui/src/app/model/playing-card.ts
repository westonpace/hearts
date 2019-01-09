export interface PlayingCard {
    suit: 'hearts' | 'clubs' | 'spades' | 'diamonds';
    rank: number;
}

export interface ShuffledDeckResponse {
    shuffleCount: number;
    cards: PlayingCard[];
}
