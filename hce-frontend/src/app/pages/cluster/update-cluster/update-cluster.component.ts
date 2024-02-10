import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
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
  selector: "ngx-update-cluster",
  templateUrl: "./update-cluster.component.html",
  styleUrls: ["./update-cluster.component.scss"],
})
export class UpdateClusterComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: ClusterService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute,
    private lookupService: LookupService
  ) {}
  form: FormGroup;
  submitted = false;
  clusterId: string;

  countries: CountryModel[] = [];
  worldRegions: WorldRegionModel[] = [];
  stateRegions: StateRegionModel[] = [];
  cities: CityModel[] = [];
  districts: DistrictModel[] = [];

  loading: boolean = false;
  async ngOnInit() {
    this.loading = true;
    this.initForm();
    await Promise.all([
      this.getAllWorldRegions(),
      this.getAllCountries(),
      this.getAllStateRegions(),
      this.getAllCities(),
      this.getAllDistricts(),
    ]);
    this.route.params.subscribe((params) => {
      this.clusterId = params["id"];
      this.service.getById(this.clusterId).subscribe((res) => {

        //selected district
        var district = this.districts.find((x) => x.districtId == res.entity.districtId);

        //selected city
        var city = this.cities.find((x) => x.cityId == district.cityId);

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
          nameAr: res.entity.clusterNameAr,
          nameEn: res.entity.clusterNameEn,
          nameLang: res.entity.clusterNameLang,
          desc: res.entity.clusterDesc,
          districtId: res.entity.districtId,
          cityId: city.cityId,
          stateRegionId: city.stateRegionId,
          countryId: country.countryId,
          worldRegionId: country.worldRegionId,
        });
        this.loading = false;
      });
    });
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

  //#region get all lookups

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

  async getAllDistricts() {
    await this.lookupService
      .districts()
      .toPromise()
      .then((response) => {
        this.districts = response.entity;
      });
  }

   onChangeWorldRegion(worldRegionId: any) {
    this.form.controls.stateRegionId.setValue("");
    this.form.controls.countryId.setValue("");
    this.countries = [];
    this.stateRegions = [];
    if (worldRegionId) this.getAllCountriesByWorldRegionId(worldRegionId);
  }

  getAllCountriesByWorldRegionId(worldRegionId: string) {
    this.lookupService.countries(worldRegionId).subscribe((data) => {
      this.countries = data.entity;
    });
  }

  onChangeCounty(countryId: any) {
    this.form.controls.stateRegionId.setValue("");
    this.stateRegions = [];
    if (countryId) this.getAllStateRegionsByCountryId(countryId);
  }

  getAllStateRegionsByCountryId(countryId: string) {
    this.lookupService.stateRegions(countryId).subscribe((data) => {
      this.stateRegions = data.entity;
    });
  }

  onChangeStateRegion(stateRegionId: any) {
    this.form.controls.cityId.setValue("");
    this.stateRegions = [];
    if (stateRegionId) this.getAllCitiesByStateRegionId(stateRegionId);
  }

  getAllCitiesByStateRegionId(stateRegionId: string) {
    this.lookupService.cities(stateRegionId).subscribe((data) => {
      this.cities = data.entity;
    });
  }

  onChangeCity(cityId: any) {
    this.form.controls.districtId.setValue("");
    this.districts = [];
    if (cityId) this.getAllDistrictsByCityId(cityId);
  }

  getAllDistrictsByCityId(cityId: string) {
    this.lookupService.districts(cityId).subscribe((data) => {
      this.districts = data.entity;
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const formData = this.form.value;

    var model = {
      clusterId: this.clusterId,
      nameAr: formData.nameAr,
      nameEn: formData.nameEn,
      nameLang: formData.nameLang,
      desc: formData.desc,
      districtId: formData.districtId,
    };

    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
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
