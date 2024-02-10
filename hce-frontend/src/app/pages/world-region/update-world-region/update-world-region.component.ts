import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { WorldRegionUpdateModel } from "../../../models/world-region/WorldRegion.model";
import { ActionTypeEnum, GetToastTitleAndMessage } from "../../../shared/messages";
import { WorldRegionService } from "../world-region.service";
@Component({
  selector: 'ngx-update-world-region',
  templateUrl: './update-world-region.component.html',
  styleUrls: ['./update-world-region.component.scss']
})
export class UpdateWorldRegionComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private service: WorldRegionService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute
  ) {}
  updateForm: FormGroup;
  submitted = false;
  regionId: string;
  ngOnInit() {
    this.initForm();
    this.route.params.subscribe((params) => {
      this.regionId = params["id"];
      this.service.getById(this.regionId).subscribe((res) => {
        this.updateForm.patchValue({
          regionId: res.entity.regionId,
          nameAr: res.entity.regionNameAr,
          nameEn: res.entity.regionNameEn,
          nameLang: res.entity.regionNameLang,
          desc: res.entity.regionDesc,
        });
      });
    });
  }
  initForm() {
    this.updateForm = this.formBuilder.group({
      regionId: ["", Validators.required],
      nameAr: ["", Validators.required],
      nameEn: ["", Validators.required],
      nameLang: ["", Validators.required],
      desc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.updateForm.invalid) {
      return;
    }
    const model = this.updateForm.value as WorldRegionUpdateModel;
    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
          "WorldRegion"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/world-region/"]);
      }
    });
  }
  get fc() {
    return this.updateForm.controls;
  }
}
