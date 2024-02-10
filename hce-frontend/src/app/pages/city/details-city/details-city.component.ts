import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CityModel } from '../../../models/city/City.model';
import { CityService } from '../city.service';

@Component({
  selector: 'ngx-details-city',
  templateUrl: './details-city.component.html',
  styleUrls: ['./details-city.component.scss']
})
export class DetailsCityComponent implements OnInit {
  constructor(
    private service: CityService,
    private route: ActivatedRoute
  ) {}
  city: CityModel = {
    createdBy: "",
    creationDate: "",
    cityDesc:"",
    cityId:"",
    cityNameAr:"",
    cityNameEn:"",
    cityNameLang:"",
    stateRegionId:"",
    stateRegionNameAr:"",
    stateRegionNameEn:"",
    stateRegionNameLang:"",
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let serviceId = params["id"];
      this.service.getById(serviceId).subscribe((res) => {
        this.city = res.entity;
      });
    });
  }
}
