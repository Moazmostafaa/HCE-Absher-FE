import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { DistrictModel } from "../../../models/district/district.model";
import { DistrictService } from "../district.service";

@Component({
  selector: "ngx-details-district",
  templateUrl: "./details-district.component.html",
  styleUrls: ["./details-district.component.scss"],
})
export class DetailsDistrictComponent implements OnInit {
  constructor(
    private service: DistrictService,
    private route: ActivatedRoute
  ) {}
  district: DistrictModel = {
    districtId: "",
    districtNameAr: "",
    districtNameEn: "",
    districtNameLang: "",
    districtDesc: "",
    createdBy: "",
    creationDate: "",
    cityId: "",
    cityNameAr: "",
    cityNameEn: "",
    cityNameLang: "",
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let districtId = params["id"];
      this.service.getById(districtId).subscribe((res) => {
        this.district = res.entity;
      });
    });
  }
}
