import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ShuffledDeckResponse } from '../model/playing-card';

@Injectable()
export class HeartsClient {

    constructor(private httpClient: HttpClient) {

    }

    getShuffledDeck() {
        return this.httpClient.get('/api') as Observable<ShuffledDeckResponse>;
    }

}
