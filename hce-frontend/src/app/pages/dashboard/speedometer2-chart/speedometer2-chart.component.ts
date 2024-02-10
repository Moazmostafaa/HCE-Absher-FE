import { Component, Input, OnInit, ViewEncapsulation } from "@angular/core";

declare var d3: any;

@Component({
  selector: 'ngx-speedometer2-chart',
  template: `<ejs-circulargauge [title]="value" width="100%" centerY="200" centerX="120">
    <e-axes>
      <e-axis
        minimum="0"
        maximum="100"
        startAngle="325"
        endAngle="150"
        [majorTicks]="majorTicks"
        [minorTicks]="minorTicks"
      >
        <e-ranges>
          <e-range
            start="0"
            end="25"
            startWidth="10"
            endWidth="10"
            color="#E02D19"
          ></e-range>
          <e-range
            start="25"
            end="50"
            startWidth="10"
            endWidth="10"
            color="#FF8100"
          ></e-range>
          <e-range
            start="50"
            end="75"
            startWidth="10"
            endWidth="10"
            color="#FFE701"
          ></e-range>
          <e-range
            start="75"
            end="100"
            startWidth="10"
            endWidth="10"
            color="#41f47f"
          ></e-range>
        </e-ranges>
        <e-pointers>
          <e-pointer [value]="value"></e-pointer>
          <e-pointer [value]="value" type="Marker" markerShape="InvertedTriangle" radius="100%"></e-pointer>
        </e-pointers>
      </e-axis>
    </e-axes>
  </ejs-circulargauge>`,
  encapsulation: ViewEncapsulation.None,
})


  
  
 

export class Speedometer2ChartComponent implements OnInit {

  constructor() {}
  @Input("value") value: number;
  majorTicks = {
    interval: 10,
  };
  minorTicks = {
    interval: 2,
  };
  ngOnInit() {}
}