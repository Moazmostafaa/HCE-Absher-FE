import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { CountryCreateModel, CountryUpdateModel } from "../../../models/country/country.model";
import { WorldRegionCreateModel, WorldRegionModel } from "../../../models/world-region/WorldRegion.model";
import { LookupService } from "../../../services/lookup.service";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { CountryService } from "../country.service";

@Component({
  selector: 'ngx-update-country',
  templateUrl: './update-country.component.html',
  styleUrls: ['./update-country.component.scss']
})
export class UpdateCountryComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: CountryService,
    private lookupService: LookupService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute
  ) {}
  form: FormGroup;
  submitted = false;
  regions: WorldRegionModel[] = [];
  countryId: string;
  async ngOnInit() {
    this.initForm();
    await this.getRegions();
    this.route.params.subscribe((params) => {
      this.countryId = params["id"];
      this.service.getById(this.countryId).subscribe((res) => {
        this.form.patchValue({
          nameAr: res.entity.countryNameAr,
          nameEn: res.entity.countryNameEn,
          nameLang: res.entity.countryNameLang,
          desc:res.entity.countryDesc,
          worldRegionId:res.entity.worldRegionId,
          countryId:res.entity.countryId,
         
        });
      });
    });
  }
  initForm() {
    this.form = this.formBuilder.group({
      countryId : ["", Validators.required],
      nameEn: ["", Validators.required],
      nameAr: ["", Validators.required],
      nameLang: ["", Validators.required],
      desc: ["", Validators.required],
      worldRegionId: ["", Validators.required],
    });
  }
  async getRegions(){
    await this.lookupService.worldRegions().toPromise().then(data => {
      this.regions = data.entity;
    })
  }
  
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const model = this.form.value as CountryUpdateModel;
    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
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
