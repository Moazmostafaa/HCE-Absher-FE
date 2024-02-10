import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { CountryModel } from "../../../models/country/country.model";
import { StateRegionUpdateModel } from "../../../models/state-region/state-region.model";
import { WorldRegionModel } from "../../../models/world-region/WorldRegion.model";
import { LookupService } from "../../../services/lookup.service";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { CountryService } from "../../country/country.service";
import { StateRegionService } from "../state-region.service";

@Component({
  selector: "ngx-update-state-region",
  templateUrl: "./update-state-region.component.html",
  styleUrls: ["./update-state-region.component.scss"],
})
export class UpdateStateRegionComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: StateRegionService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute,
    private lookupService: LookupService,
    private countryService: CountryService
  ) {}
  form: FormGroup;
  submitted = false;
  regionId: string;
  countries: CountryModel[] = [];
  worldRegions: WorldRegionModel[] = [];

  loading: boolean = false;
  async ngOnInit() {
    this.initForm();
    this.loading = true;
    await Promise.all([this.getAllWorldRegions(), this.getAllCountries()]);

    this.loading = false;
    this.route.params.subscribe((params) => {
      this.regionId = params["id"];
      this.service.getById(this.regionId).subscribe(async (res) => {
        var country = this.countries.find(
          (x) => x.countryId == res.entity.countryId
        );
        this.countries = this.countries.filter(
          (x) => x.worldRegionId == country.worldRegionId
        );

        this.form.patchValue({
          countryId: res.entity.countryId,
          nameAr: res.entity.regionNameAr,
          nameEn: res.entity.regionNameEn,
          nameLang: res.entity.regionNameLang,
          desc: res.entity.regionDesc,
          worldRegionId: country.worldRegionId,
        });
      });
    });
  }

  async getAllWorldRegions() {
    await this.lookupService
      .worldRegions()
      .toPromise()
      .then((data) => {
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

  getAllCountriesByWorldRegionId(worldRegionId: string) {
    this.lookupService.countries(worldRegionId).subscribe((data) => {
      this.countries = data.entity;
    });
  }

  onChangeWorldRegion(worldRegionId: any) {
    this.form.controls.countryId.setValue("");
    this.countries = [];
    if (worldRegionId) this.getAllCountriesByWorldRegionId(worldRegionId);
  }
  initForm() {
    this.form = this.formBuilder.group({
      countryId: ["", Validators.required],
      nameAr: ["", Validators.required],
      nameEn: ["", Validators.required],
      nameLang: ["", Validators.required],
      desc: ["", Validators.required],
      worldRegionId: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const formData = this.form.value;
    var model = {
      regionId: this.regionId,
      nameAr: formData.nameAr,
      nameEn: formData.nameEn,
      nameLang: formData.nameLang,
      desc: formData.desc,
      countryId: formData.countryId,
    };
    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
          "State Region"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/state-region/"]);
      }
    });
  }
  get fc() {
    return this.form.controls;
  }
}
