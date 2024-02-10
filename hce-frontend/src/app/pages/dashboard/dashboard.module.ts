import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgxChartsModule } from "@swimlane/ngx-charts";
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { DashboardComponent } from "./dashboard.component";
import { FormsModule } from "@angular/forms";
import { NbCardModule, NbDatepickerModule, NbInputModule, NbMenuModule, NbSelectModule, NbSidebarModule } from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { NgxEchartsModule } from 'ngx-echarts';
import { SuccessRatioStatsComponent } from "./success-ratio-stats/success-ratio-stats.component";
import { SpeedometerChartComponent } from "./speedometer-chart/speedometer-chart.component";
import { CircularGaugeModule } from '@syncfusion/ej2-angular-circulargauge';
import { Speedometer2ChartComponent } from "./speedometer2-chart/speedometer2-chart.component";



@NgModule({
  declarations: [
    DashboardComponent,
    SuccessRatioStatsComponent,
    SpeedometerChartComponent,
    Speedometer2ChartComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    ThemeModule,
    NgxChartsModule,
    NgxEchartsModule,
    NbCardModule,
    FormsModule,
    SharedModule,
    CircularGaugeModule ,
    NbDatepickerModule,
    NbCardModule,
    NbInputModule,
    NbMenuModule,
    NbSidebarModule,
    NbSelectModule,
    
    
  ],

})
export class DashboardModule {}
