import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { StateRegionModel } from "../../../models/state-region/state-region.model";
import { StateRegionService } from "../state-region.service";

@Component({
  selector: "ngx-details-state-region",
  templateUrl: "./details-state-region.component.html",
  styleUrls: ["./details-state-region.component.scss"],
})
export class DetailsStateRegionComponent implements OnInit {
  constructor(
    private service: StateRegionService,
    private route: ActivatedRoute
  ) {}
  stateRegion: StateRegionModel = {
    regionNameAr: "",
    regionNameEn: "",
    regionDesc: "",
    creationDate: "",
    createdBy: "",
    regionNameLang: "",
    regionId: "",
    countryId: "",
    countryNameAr: "",
    countryNameEn: "",
    countryNameLang: "",
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let stateRegionId = params["id"];
      this.service.getById(stateRegionId).subscribe((res) => {
        this.stateRegion = res.entity;
      });
    });
  }
}
