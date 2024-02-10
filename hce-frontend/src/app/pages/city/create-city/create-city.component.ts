import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { CityCreateModel } from "../../../models/city/City.model";
import { CountryModel } from "../../../models/country/country.model";
import { StateRegionModel } from "../../../models/state-region/state-region.model";
import { WorldRegionModel } from "../../../models/world-region/WorldRegion.model";
import { LookupService } from "../../../services/lookup.service";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { CityService } from "../city.service";

@Component({
  selector: "ngx-create-city",
  templateUrl: "./create-city.component.html",
  styleUrls: ["./create-city.component.scss"],
})
export class CreateCityComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: CityService,
    private router: Router,
    private toastrService: NbToastrService,
    private lookupService: LookupService
  ) {}
  form: FormGroup;
  submitted = false;
  countries: CountryModel[] = [];
  worldRegions: WorldRegionModel[] = [];
  stateRegions: StateRegionModel[] = [];

  ngOnInit() {
    this.getAllWorldRegions();
    this.initForm();
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

  onChangeWorldRegion(worldRegionId: any) {
    this.form.controls.stateRegionId.setValue("");
    this.form.controls.countryId.setValue("");
    this.countries = [];
    this.stateRegions = [];
    if (worldRegionId) this.getAllCountriesByWorldRegionId(worldRegionId);
  }

  onChangeCounty(countryId: any) {
    this.form.controls.stateRegionId.setValue("");
    this.stateRegions = [];
    if (countryId) this.getAllStateRegionsByCountryId(countryId);
  }

  initForm() {
    this.form = this.formBuilder.group({
      nameAr: ["", Validators.required],
      nameEn: ["", Validators.required],
      nameLang: ["", Validators.required],
      desc: ["", Validators.required],
      stateRegionId: ["", Validators.required],
      worldRegionId: ["", Validators.required],
      countryId: ["", Validators.required],
    });
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
      stateRegionId: formData.stateRegionId,
    };
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(ActionTypeEnum.Created, "City");
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/city/"]);
      }
    });
  }

  get fc() {
    return this.form.controls;
  }
}
