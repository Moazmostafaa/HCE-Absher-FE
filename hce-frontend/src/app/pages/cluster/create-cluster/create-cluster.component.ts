import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { CityModel } from "../../../models/city/City.model";
import { CountryModel } from "../../../models/country/country.model";
import { DistrictModel } from "../../../models/district/district.model";
import { StateRegionModel } from "../../../models/state-region/state-region.model";
import { WorldRegionModel } from "../../../models/world-region/WorldRegion.model";
import { LookupService } from "../../../services/lookup.service";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { ClusterService } from "../cluster.service";

@Component({
  selector: "ngx-create-cluster",
  templateUrl: "./create-cluster.component.html",
  styleUrls: ["./create-cluster.component.scss"],
})
export class CreateClusterComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: ClusterService,
    private router: Router,
    private toastrService: NbToastrService,
    private lookupService: LookupService
  ) {}
  form: FormGroup;
  submitted = false;

  worldRegions: WorldRegionModel[] = [];
  countries: CountryModel[] = [];
  stateRegions: StateRegionModel[] = [];
  cities: CityModel[] = [];
  districts: DistrictModel[] = [];

  ngOnInit() {
    this.getAllWorldRegions();
    this.initForm();
  }

  initForm() {
    this.form = this.formBuilder.group({
      nameAr: ["", Validators.required],
      nameEn: ["", Validators.required],
      nameLang: ["", Validators.required],
      desc: ["", Validators.required],
      cityId: ["", Validators.required],
      worldRegionId: ["", Validators.required],
      countryId: ["", Validators.required],
      stateRegionId: ["", Validators.required],
      districtId: ["", Validators.required],
    });
  }
  getAllWorldRegions() {
    this.lookupService.worldRegions().subscribe((data) => {
      this.worldRegions = data.entity;
    });
  }

  getAllCountriesByWorldRegionId(worldRegionId: string) {
    this.lookupService.countries(worldRegionId).subscribe((data) => {
      this.countries = data.entity;
    });
  }

  getAllStateRegionsByCountryId(countryId: string) {
    this.lookupService.stateRegions(countryId).subscribe((data) => {
      this.stateRegions = data.entity;
    });
  }

  getAllCitiesByStateRegionId(stateRegionId: string) {
    this.lookupService.cities(stateRegionId).subscribe((data) => {
      this.cities = data.entity;
    });
  }

  getAllDistrictsByCityId(cityId: string) {
    this.lookupService.districts(cityId).subscribe((data) => {
      this.districts = data.entity;
    });
  }

  onChangeWorldRegion(worldRegionId: any) {
    this.form.controls.countryId.setValue("");
    this.form.controls.stateRegionId.setValue("");
    this.form.controls.cityId.setValue("");
    this.form.controls.districtId.setValue("");
    this.countries = [];
    this.stateRegions = [];
    this.cities = [];
    this.districts = [];
    if (worldRegionId) this.getAllCountriesByWorldRegionId(worldRegionId);
  }

  onChangeCounty(countryId: any) {
    this.form.controls.stateRegionId.setValue("");
    this.form.controls.cityId.setValue("");
    this.form.controls.districtId.setValue("");
    this.stateRegions = [];
    this.cities = [];
    this.districts = [];
    if (countryId) this.getAllStateRegionsByCountryId(countryId);
  }

  onChangeStateRegion(stateRegionId: any) {
    this.form.controls.cityId.setValue("");
    this.form.controls.districtId.setValue("");
    this.cities = [];
    this.districts = [];
    if (stateRegionId) this.getAllCitiesByStateRegionId(stateRegionId);
  }

  onChangeCity(cityId: any) {
    this.form.controls.districtId.setValue("");
    this.districts = [];
    if (cityId) this.getAllDistrictsByCityId(cityId);
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    let formData = this.form.value;

    let model = {
      nameAr: formData.nameAr,
      nameEn: formData.nameEn,
      nameLang: formData.nameLang,
      desc: formData.desc,
      districtId: formData.districtId,
    };
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Created,
          "Cluster"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/cluster/"]);
      }
    });
  }

  get fc() {
    return this.form.controls;
  }
}
