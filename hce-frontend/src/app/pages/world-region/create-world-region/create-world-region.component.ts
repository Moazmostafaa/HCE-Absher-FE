import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NbToastrService } from '@nebular/theme';
import { WorldRegionCreateModel } from '../../../models/world-region/WorldRegion.model';
import { GetToastTitleAndMessage, ActionTypeEnum } from '../../../shared/messages';
import { WorldRegionService } from '../world-region.service';

@Component({
  selector: 'ngx-create-world-region',
  templateUrl: './create-world-region.component.html',
  styleUrls: ['./create-world-region.component.scss']
})
export class CreateWorldRegionComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: WorldRegionService,
    private router: Router,
    private toastrService: NbToastrService
  ) {}
  form: FormGroup;
  submitted = false;
  ngOnInit() {
    this.initForm();
  }
  initForm() {
    this.form = this.formBuilder.group({
      nameAr: ["", Validators.required],
      nameEn: ["", Validators.required],
      nameLang: ["", Validators.required],
      desc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const model = this.form.value as WorldRegionCreateModel;
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(ActionTypeEnum.Created, "World Region");
        this.toastrService.info(message.message,message.title);
        this.router.navigate(["/pages/world-region/"]);
      }
    });
  }
  get fc() {
    return this.form.controls;
  }
}
