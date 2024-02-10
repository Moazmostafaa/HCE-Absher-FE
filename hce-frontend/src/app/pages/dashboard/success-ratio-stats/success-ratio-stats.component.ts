import { AUTO_STYLE } from "@angular/animations";
import { AfterViewInit, Component, OnDestroy, OnInit } from "@angular/core";
import { NbThemeService } from "@nebular/theme";

@Component({
  selector: "ngx-success-ratio-stats",
  template: ` <div echarts [options]="options" class="echart"></div> `,
})
export class SuccessRatioStatsComponent implements AfterViewInit, OnDestroy {
  options: any = {};
  themeSubscription: any;

  constructor(private theme: NbThemeService) {}

  ngAfterViewInit() {
    this.themeSubscription = this.theme.getJsTheme().subscribe((config) => {
      const colors: any = config.variables;
      const echarts: any = config.variables.echarts;

      this.options = {
        backgroundColor: echarts.bg,
        color: [colors.danger, colors.primary, colors.info],
        tooltip: {
          trigger: "item",
          formatter: "{a} <br/>{b} : {c}",
        },
        legend: {
          left: "left",
          data: ["KBL", "MZR"],
          textStyle: {
            color: echarts.textColor,
          },
        },
        xAxis: [
          {
            type: "category",
           // interval : "auto",
           // boundaryGap:"true",
          
            
            data: [
              "1-AUG",
              "6-AUG",
              "11-AUG",
              "16-AUG",
              "21-AUG",
              "26-AUG",
              "1-SEP",
              "6-SEP",
              "11-SEP",
              "16-SEP",
              "21-SEP",
              "26-SEP",
              "1-OCT",
              "6-OCT",
              "11-OCT",
              "16-OCT",
              "21-OCT",
            ],
            axisTick: {
              alignWithLabel: true,

            },
            axisLine: {
              lineStyle: {
                color: echarts.axisLineColor,
              },
            },
            axisLabel: {
              textStyle: {
                color: echarts.textColor,
              },
            },
          },
        ],
        yAxis: [
          {
            type: "value",
            axisLine: {
              lineStyle: {
                color: echarts.axisLineColor,
              },
            },
            splitLine: {
              lineStyle: {
                color: echarts.splitLineColor,
              },
            },
            axisLabel: {
              textStyle: {
                color: echarts.textColor,
              },
            },
          },
        ],
        grid: {
          left: "3%",
          right: "4%",
          bottom: "3%",
          containLabel: true,
        },
        series: [
          {
            name: "KBL",
            type: "line",
            
            data: [
              2.8, 2.3, 4, 3.5, 4, 2.3, 4, 3.5, 4, 2.3, 4, 3.5, 2.3, 4, 3.5,
              2.3, 4
            ],
          },
          {
            name: "MZR",
            type: "line",
            data: [ 2.3, 4, 3.5, 4, 2.3, 4, 3.5, 4, 2.3, 4, 3.5, 2.3, 4, 3.5,
              2.3, 4,2.5],
          },
        ],
      };
    });
  }

  ngOnDestroy(): void {
    this.themeSubscription.unsubscribe();
  }
}
