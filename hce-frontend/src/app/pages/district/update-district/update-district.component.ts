import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { CityModel } from "../../../models/city/City.model";
import { CountryModel } from "../../../models/country/country.model";
import { StateRegionModel } from "../../../models/state-region/state-region.model";
import { WorldRegionModel } from "../../../models/world-region/WorldRegion.model";
import { LookupService } from "../../../services/lookup.service";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { DistrictService } from "../district.service";

@Component({
  selector: "ngx-update-district",
  templateUrl: "./update-district.component.html",
  styleUrls: ["./update-district.component.scss"],
})
export class UpdateDistrictComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: DistrictService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute,
    private lookupService: LookupService
  ) {}
  form: FormGroup;
  submitted = false;
  districtId: string;

  countries: CountryModel[] = [];
  worldRegions: WorldRegionModel[] = [];
  stateRegions: StateRegionModel[] = [];
  cities: CityModel[] = [];
  loading: boolean = false;
  async ngOnInit() {
    this.loading = true;
    this.initForm();
    await Promise.all([
      this.getAllWorldRegions(),
      this.getAllCountries(),
      this.getAllStateRegions(),
      this.getAllCities(),
    ]);
    this.loading = false;

    this.route.params.subscribe((params) => {
      this.districtId = params["id"];
      this.service.getById(this.districtId).subscribe((res) => {
        //selected city
        var city = this.cities.find((x) => x.cityId == res.entity.cityId);

        // selected state region
        var stateRegion = this.stateRegions.find(
          (x) => x.regionId == city.stateRegionId
        );

        // selected country
        var country = this.countries.find(
          (x) => x.countryId == stateRegion.countryId
        );

        // filter state regions
        this.stateRegions = this.stateRegions.filter(
          (x) => x.countryId == country.countryId
        );

        // filter countries
        this.countries = this.countries.filter(
          (x) => x.worldRegionId == country.worldRegionId
        );

        this.form.patchValue({
          nameAr: res.entity.districtNameAr,
          nameEn: res.entity.districtNameEn,
          nameLang: res.entity.districtNameLang,
          desc: res.entity.districtDesc,
          cityId: res.entity.cityId,
          stateRegionId: city.stateRegionId,
          countryId: country.countryId,
          worldRegionId: this.worldRegions.find(
            (x) => x.regionId == country.worldRegionId
          ).regionId,
        });
      });
    });
  }

  async getAllWorldRegions() {
    await this.lookupService
      .worldRegions()
      .toPromise()
      .then(async (data) => {
        this.worldRegions = data.entity;
      });
  }

  async getAllCountries() {
    await this.lookupService
      .countries()
      .toPromise()
      .then(async (response) => {
        this.countries = this.countries.concat(response.entity);
      });
  }

  async getAllStateRegions() {
    await this.lookupService
      .stateRegions()
      .toPromise()
      .then((srResponse) => {
        this.stateRegions = srResponse.entity;
      });
  }

  async getAllCities() {
    await this.lookupService
      .cities()
      .toPromise()
      .then((response) => {
        this.cities = response.entity;
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
    this.form.controls.stateRegionId.setValue("");
    this.form.controls.countryId.setValue("");
    this.countries = [];
    this.stateRegions = [];
    this.cities = [];
    if (worldRegionId) this.getAllCountriesByWorldRegionId(worldRegionId);
  }

  onChangeCounty(countryId: any) {
    this.form.controls.stateRegionId.setValue("");
    this.stateRegions = [];
    this.cities = [];
    if (countryId) this.getAllStateRegionsByCountryId(countryId);
  }

  onChangeStateRegion(stateRegionId: any) {
    this.form.controls.cityId.setValue("");
    this.cities = [];
    if (stateRegionId) this.getAllCitiesByStateRegionId(stateRegionId);
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

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const formData = this.form.value;

    var model = {
      districtId: this.districtId,
      nameAr: formData.nameAr,
      nameEn: formData.nameEn,
      nameLang: formData.nameLang,
      desc: formData.desc,
      cityId: formData.cityId,
    };

    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
          "District"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/district/"]);
      }
    });
  }
  get fc() {
    return this.form.controls;
  }
}
