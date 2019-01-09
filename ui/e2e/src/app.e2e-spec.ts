import { AppPage } from './app.po';

describe('workspace-project App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display fifty-two cards', async () => {
    await page.navigateTo();
    const cards = await page.getCards();
    expect(cards.length).toBe(52);
  });

  it('should display the shuffle count', async () => {
    await page.navigateTo();
    const shuffleCount = await page.getShuffleCount();
    expect(shuffleCount.startsWith('Shuffle Count: ')).toBeTruthy();
    const shuffleCountNumber = Number(shuffleCount.substr(15));
    expect(shuffleCountNumber).toBeGreaterThan(0);
  });

});
