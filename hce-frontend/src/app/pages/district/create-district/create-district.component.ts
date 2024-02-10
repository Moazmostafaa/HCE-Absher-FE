import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NbToastrService } from '@nebular/theme';
import { CityModel } from '../../../models/city/City.model';
import { CountryModel } from '../../../models/country/country.model';
import { StateRegionModel } from '../../../models/state-region/state-region.model';
import { WorldRegionModel } from '../../../models/world-region/WorldRegion.model';
import { LookupService } from '../../../services/lookup.service';
import { GetToastTitleAndMessage, ActionTypeEnum } from '../../../shared/messages';
import { DistrictService } from '../district.service';

@Component({
  selector: 'ngx-create-district',
  templateUrl: './create-district.component.html',
  styleUrls: ['./create-district.component.scss']
})
export class CreateDistrictComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: DistrictService,
    private router: Router,
    private toastrService: NbToastrService,
    private lookupService: LookupService
  ) {}
  form: FormGroup;
  submitted = false;

  countries: CountryModel[] = [];
  worldRegions: WorldRegionModel[] = [];
  stateRegions: StateRegionModel[] = [];
  cities: CityModel[] = [];

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

  onChangeWorldRegion(worldRegionId: any) {
    this.form.controls.countryId.setValue("");
    this.form.controls.stateRegionId.setValue("");
    this.form.controls.cityId.setValue("");
    this.countries = [];
    this.stateRegions = [];
    this.cities = [];
    if (worldRegionId) this.getAllCountriesByWorldRegionId(worldRegionId);
  }

  onChangeCounty(countryId: any) {
    this.form.controls.stateRegionId.setValue("");
    this.form.controls.cityId.setValue("");
    this.stateRegions = [];
    this.cities = [];
    if (countryId) this.getAllStateRegionsByCountryId(countryId);
  }

  onChangeStateRegion(stateRegionId: any) {
    this.form.controls.cityId.setValue("");
    this.cities = [];
    if (stateRegionId) this.getAllCitiesByStateRegionId(stateRegionId);
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
      cityId: formData.cityId,
    };
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(ActionTypeEnum.Created, "District");
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/district/"]);
      }
    });
  }

  get fc() {
    return this.form.controls;
  }
}
