import {
  AfterViewInit,
  Component,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  SimpleChanges,
} from "@angular/core";
import { NbThemeService } from "@nebular/theme";
import { LabelModel } from "../../../models/statistics/labels.model";
import { StatisticsEnum } from "../../../models/statistics/stats.enum";
import { StatisticsService } from "../../../services/statistics.service";

@Component({
  selector: "pie-stat",
  templateUrl: "./pie-stat.component.html"
})
export class PieStatComponent implements AfterViewInit, OnDestroy, OnChanges {
  options: any = {};
  hasData: boolean;
  themeSubscription: any;
  constructor(
    private theme: NbThemeService,
    private statisticsService: StatisticsService
  ) {}
  @Input() statsEnum: StatisticsEnum;
  @Input() from: Date;
  @Input() to: Date;
  @Input() text: string;
  number = 10;
  ngAfterViewInit() {}
  ngOnDestroy() {
    this.themeSubscription.unsubscribe();
  }
  ngOnChanges(changes: SimpleChanges) {
    if (changes.from) {
      this.from = changes.from.currentValue;
    }
    if (changes.to) {
      this.to = changes.to.currentValue;
    }
    if ((this.from && this.to) || (!this.from && !this.to)) this.getStatistcs();
  }
  getStatistcs() {
    this.statisticsService
      .getPieStatistics(this.statsEnum, this.from, this.to, this.number)
      .subscribe((res) => {
        this.hasData = res.entity.length > 0;
        this.themeSubscription = this.theme.getJsTheme().subscribe((config) => {
          const colors = config.variables;
          const echarts: any = config.variables.echarts;
          this.options = {
            backgroundColor: echarts.bg,
            color: [
              colors.warningLight,
              colors.infoLight,
              colors.dangerLight,
              colors.successLight,
              colors.primaryLight,
            ],
            tooltip: {
              trigger: "item",
              formatter: "{a} <br/>{b} : {c} ({d}%)",
            },
            legend: {
              orient: "horziontal",
              left: "left",
              data: res.entity.map((x) => `${x.name}:  ${x.number}`),
              textStyle: {
                color: echarts.textColor,
              },
            },
            series: [
              {
                name: this.text || "data",
                type: "pie",
                radius: "80%",
                center: ["70%", "50%"],
                data: res.entity.map((x) => ({
                  value: x.number,
                  name: `${x.name}:  ${x.number}`,
                })),
                itemStyle: {
                  emphasis: {
                    shadowBlur: 10,
                    shadowOffsetX: 0,
                    shadowColor: echarts.itemHoverShadowColor,
                  },
                },
                label: {
                  normal: {
                    textStyle: {
                      color: echarts.textColor,
                    },
                  },
                },
                labelLine: {
                  normal: {
                    lineStyle: {
                      color: echarts.axisLineColor,
                    },
                  },
                },
              },
            ],
          };
        });
      });
  }
}
