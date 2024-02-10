import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
  providedIn: 'root',
})
export class LanguageService {
  readonly html = document.getElementsByTagName('html')[0];

  constructor(private translate: TranslateService) {
    if (
      this.translate.currentLang == 'ar' ||
      localStorage.getItem('language') == 'ar'
    ) {
      localStorage.setItem('language', 'ar');
      translate.setDefaultLang('ar');
      translate.use('ar');
      this.html.setAttribute('lang', 'ar');
    } else {
      localStorage.setItem('language', 'en');
      translate.setDefaultLang('en');
      translate.use('en');
      this.html.setAttribute('lang', 'en');
    }
  }

  changeLange() {
    // Get HTML head element

    if (this.translate.currentLang === 'ar') {
      this.translate.use('en');
      this.html.setAttribute('lang', 'en');
      localStorage.setItem('language', 'en');
    } else {
      this.translate.use('ar');
      this.html.setAttribute('lang', 'ar');
      localStorage.setItem('language', 'ar');
    }
  }
}
