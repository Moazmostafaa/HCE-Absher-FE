import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
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
  selector: "ngx-update-city",
  templateUrl: "./update-city.component.html",
  styleUrls: ["./update-city.component.scss"],
})
export class UpdateCityComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: CityService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute,
    private lookupService: LookupService
  ) {}
  form: FormGroup;
  submitted = false;
  cityId: string;
  countries: CountryModel[] = [];
  worldRegions: WorldRegionModel[] = [];
  stateRegions: StateRegionModel[] = [];
  loading: boolean = false;

  async ngOnInit() {
    this.initForm();
    // Parallel calls to get all countries, world regions, state regions
    this.loading = true;
    await Promise.all([
      this.getAllWorldRegions(),
      this.getAllCountries(),
      this.getAllStateRegions(),
    ]);

    this.route.params.subscribe((params) => {
      this.cityId = params["id"];
      this.service.getById(this.cityId).subscribe((res) => {
        this.loading = false;
        // selected state region
        var stateRegion = this.stateRegions.find(
          (x) => x.regionId == res.entity.stateRegionId
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
          nameAr: res.entity.cityNameAr,
          nameEn: res.entity.cityNameEn,
          nameLang: res.entity.cityNameLang,
          desc: res.entity.cityDesc,
          stateRegionId: res.entity.stateRegionId,
          worldRegionId: country.worldRegionId,
          countryId: country.countryId,
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
    const formData = this.form.value;

    var model = {
      cityId: this.cityId,
      nameAr: formData.nameAr,
      nameEn: formData.nameEn,
      nameLang: formData.nameLang,
      desc: formData.desc,
      stateRegionId: formData.stateRegionId,
    };

    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(ActionTypeEnum.Updated, "City");
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/city/"]);
      }
    });
  }
  get fc() {
    return this.form.controls;
  }
}
