import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { CountryModel } from "../../../models/country/country.model";
import { StateRegionCreateModel } from "../../../models/state-region/state-region.model";
import { WorldRegionModel } from "../../../models/world-region/WorldRegion.model";
import { LookupService } from "../../../services/lookup.service";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { StateRegionService } from "../state-region.service";

@Component({
  selector: "ngx-create-state-region",
  templateUrl: "./create-state-region.component.html",
  styleUrls: ["./create-state-region.component.scss"],
})
export class CreateStateRegionComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: StateRegionService,
    private router: Router,
    private toastrService: NbToastrService,
    private lookupService: LookupService
  ) {}
  form: FormGroup;
  submitted = false;
  countries: CountryModel[] = [];
  worldRegions: WorldRegionModel[] = [];

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

  onChangeWorldRegion(worldRegionId: any) {
    this.form.controls.countryId.setValue("");
    this.countries = [];
    if (worldRegionId) this.getAllCountriesByWorldRegionId(worldRegionId);

  }

  initForm() {
    this.form = this.formBuilder.group({
      nameAr: ["", Validators.required],
      nameEn: ["", Validators.required],
      nameLang: ["", Validators.required],
      desc: ["", Validators.required],
      countryId: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const model = this.form.value as StateRegionCreateModel;
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Created,
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
