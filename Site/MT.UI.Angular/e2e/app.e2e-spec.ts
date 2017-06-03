import { AppExemploPage } from './app.po';

describe('app-exemplo App', () => {
  let page: AppExemploPage;

  beforeEach(() => {
    page = new AppExemploPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
