import { Component, Input, OnChanges, OnInit, SimpleChanges } from "@angular/core";
import { StatisticsEnum } from "../../../models/statistics/stats.enum";
import { StatisticsService } from "../../../services/statistics.service";

@Component({
  selector: "ngx-card-stat",
  templateUrl: "./card-stat.component.html",
  styleUrls: ["./card-stat.component.scss"],
})
export class CardStatComponent implements OnInit, OnChanges {
  @Input() icon: string;
  @Input() text: string;
  @Input() statsEnum: StatisticsEnum;
  @Input() from: Date;
  @Input() to: Date;
  number: number = 0;
  constructor(private statisticsService: StatisticsService) {}
  
  ngOnInit() {
  }
  ngOnChanges(changes: SimpleChanges) {
    if(changes.from){
      this.from = changes.from.currentValue;
    }
    if(changes.to){
      this.to = changes.to.currentValue;
    }
    this.getStatistcs();
  }
  getStatistcs() {
    this.statisticsService
      .getNumberStatistics(this.statsEnum, this.from, this.to)
      .subscribe((res) => {
        this.number = res.entity;
      });
  }
}
