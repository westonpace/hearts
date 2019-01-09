import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { PlayingCardComponent } from './components/card/playing-card.component';
import { HeartsClient } from './services/hearts-client';

@NgModule({
  declarations: [
    AppComponent,
    PlayingCardComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [HeartsClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
