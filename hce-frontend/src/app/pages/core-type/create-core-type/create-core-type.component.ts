import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NbToastrService } from '@nebular/theme';
import { CoreTypeCreateModel } from '../../../models/core-type/CoreType.model';
import { GetToastTitleAndMessage, ActionTypeEnum } from '../../../shared/messages';
import { CoreTypeService } from '../core-type.service';

@Component({
  selector: 'ngx-create-core-type',
  templateUrl: './create-core-type.component.html',
  styleUrls: ['./create-core-type.component.scss']
})
export class CreateCoreTypeComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: CoreTypeService,
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
      npskpiWeightName: ["", Validators.required],
      npskpiWeightDesc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const model = this.form.value as CoreTypeCreateModel;
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(ActionTypeEnum.Created, "Core Type");
        this.toastrService.info(message.message,message.title);
        this.router.navigate(["/pages/core-type/"]);
      }
    });
  }
  get fc() {
    return this.form.controls;
  }
}
