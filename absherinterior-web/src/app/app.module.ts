import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LOCALE_ID, NgModule } from "@angular/core";
import {
  HttpClientModule,
  HTTP_INTERCEPTORS,
  HttpClient,
} from "@angular/common/http";
import { CoreModule } from "./@core/core.module";
import { ThemeModule } from "./@theme/theme.module";
import { AppComponent } from "./app.component";
import { AppRoutingModule } from "./app-routing.module";
import {
  NbCardModule,
  NbChatModule,
  NbDatepickerModule,
  NbDialogModule,
  NbMenuModule,
  NbSidebarModule,
  NbToastrModule,
  NbWindowModule,
} from "@nebular/theme";
import { NbAuthModule } from "@nebular/auth";
import { authConfig } from "./@core/configurations/auth-config";
import { AuthInterceptor } from "./@core/configurations/auth-interceptor";
import { toastConfig } from "./@core/configurations/toast-config";
import { ConfirmDialogComponent } from "./shared/components/showcase-dialog/confirm-dialog.component";
import { DatePipe } from "@angular/common";
import { Ng2TelInputModule } from "ng2-tel-input";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { MatPaginatorIntl } from "@angular/material/paginator";
import { MatPaginatorI18nService } from "./services/MatPaginatorI18n.service";
import { registerLocaleData } from '@angular/common';
import localeAr from '@angular/common/locales/ar-EG';
import { ChangePasswordComponent } from './change-password/change-password.component';
registerLocaleData(localeAr);

export function HttpLoaderFactory(http: HttpClient) {
    return new TranslateHttpLoader(
      http, 
      './assets/i18n/', // or whatever path you're using
      '.json'
    );
  }
@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    Ng2TelInputModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
    NbSidebarModule.forRoot(),
    NbMenuModule.forRoot(),
    NbDatepickerModule.forRoot(),
    NbDialogModule.forRoot(),
    NbWindowModule.forRoot(),
    NbToastrModule.forRoot(toastConfig),
    NbChatModule.forRoot({
      messageGoogleMapKey: "AIzaSyA_wNuCzia92MAmdLRzmqitRGvCF7wCZPY",
    }),
    CoreModule.forRoot(),
    ThemeModule.forRoot(),
    NbAuthModule.forRoot(authConfig),
    NbCardModule,
  ],
  bootstrap: [AppComponent],
  exports: [],
  entryComponents: [ConfirmDialogComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    // { provide: LOCALE_ID, useValue: "ar-EG" },
    DatePipe,
    {
      provide: MatPaginatorIntl,
      useClass: MatPaginatorI18nService,
    },
  ],
})
export class AppModule {}
