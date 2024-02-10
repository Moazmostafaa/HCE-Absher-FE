import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { CountryCreateModel } from "../../../models/country/country.model";
import { WorldRegionCreateModel, WorldRegionModel } from "../../../models/world-region/WorldRegion.model";
import { LookupService } from "../../../services/lookup.service";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { CountryService } from "../country.service";

@Component({
  selector: "ngx-create-country",
  templateUrl: "./create-country.component.html",
  styleUrls: ["./create-country.component.scss"],
})
export class CreateCountryComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: CountryService,
    private router: Router,
    private toastrService: NbToastrService,
    private lookupService: LookupService
  ) {}
  form: FormGroup;
  submitted = false;
  regions: WorldRegionModel[] = [];

  ngOnInit() {
    this.getRegions();
    
    this.initForm();

  }
  initForm() {
    this.form = this.formBuilder.group({
      nameEn: ["", Validators.required],
      nameAr: ["", Validators.required],
      nameLang: ["", Validators.required],
      desc: ["", Validators.required],
      worldRegionId: ["", Validators.required],
    });
  }

  getRegions(){
    this.lookupService.worldRegions().subscribe(data => {
      this.regions = data.entity;
    })
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const model = this.form.value as CountryCreateModel;
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Created,
          "Country"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/country/"]);
      }
    });
  }
  get fc() {
    return this.form.controls;
  }
}
