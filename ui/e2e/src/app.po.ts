import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get('/');
  }

  getCards() {
    return element(by.css('app-root')).all(by.css('jop-card'));
  }

  getShuffleCount() {
    return element(by.css('app-root span')).getText();
  }
}
