import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CountryModel } from '../../../models/country/country.model';
import { CountryService } from '../country.service';

@Component({
  selector: 'ngx-details-country',
  templateUrl: './details-country.component.html',
  styleUrls: ['./details-country.component.scss']
})
export class DetailsCountryComponent implements OnInit {

  constructor(
    private service: CountryService,
    private route: ActivatedRoute
  ) {}
  country: CountryModel = {
   countryNameEn:"",
   countryNameAr:"",
   countryDesc:"",
   countryNameLang:"",
    creationDate: "",
    createdBy: "",
    countryId:"",
    worldRegionId:"",
    worldRegionNameAr:"",
    worldRegionNameEn:"",
    worldRegionNameLang:"",
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let worldRegionId = params["id"];
      this.service.getById(worldRegionId).subscribe((res) => {
        this.country = res.entity;
      });
    });
  }
}
