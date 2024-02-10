import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { StatistcsDashboardRoutingModule } from "./statistcs-dashboard-routing.module";
import { ChartsModule } from "ng2-charts";
import { DashboardStatisticsComponent } from "./dashboard-statistics/dashboard-statistics.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatInputModule } from "@angular/material/input";
import {
  MatFormFieldModule,
  MAT_FORM_FIELD_DEFAULT_OPTIONS,
} from "@angular/material/form-field";
import { MatNativeDateModule, MAT_DATE_LOCALE } from "@angular/material/core";
import { ThemeModule } from "../../@theme/theme.module";
import { NbCardModule } from "@nebular/theme";
import { CardStatComponent } from './card-stat/card-stat.component';
import { PieStatComponent } from './pie-stat/pie-stat.component';
import { NgxEchartsModule } from 'ngx-echarts';

@NgModule({
  declarations: [DashboardStatisticsComponent, CardStatComponent, PieStatComponent],
  imports: [
    CommonModule,
    StatistcsDashboardRoutingModule,
    ChartsModule,
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule,
    ThemeModule,
    NgxEchartsModule,
    NbCardModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    {
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: { appearance: "fill" },
    },
    { provide: MAT_DATE_LOCALE, useValue: "en-GB" },
  ],
})
export class StatistcsDashboardModule {}
