import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorldRegionModel } from '../../../models/world-region/WorldRegion.model';
import { WorldRegionService } from '../world-region.service';

@Component({
  selector: 'ngx-details-world-region',
  templateUrl: './details-world-region.component.html',
  styleUrls: ['./details-world-region.component.scss']
})
export class DetailsWorldRegionComponent implements OnInit {
  constructor(
    private service: WorldRegionService,
    private route: ActivatedRoute
  ) {}
  worldRegion: WorldRegionModel = {
    regionNameAr: "",
    regionNameEn: "",
    regionDesc: "",
    creationDate: "",
    createdBy: "",
    regionNameLang: "",
    regionId: ""
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let worldRegionId = params["id"];
      this.service.getById(worldRegionId).subscribe((res) => {
        this.worldRegion = res.entity;
      });
    });
  }
}
