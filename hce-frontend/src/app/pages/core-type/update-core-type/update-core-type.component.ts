import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NbToastrService } from '@nebular/theme';
import { CoreTypeUpdateModel } from '../../../models/core-type/CoreType.model';
import { GetToastTitleAndMessage, ActionTypeEnum } from '../../../shared/messages';
import { CoreTypeService } from '../core-type.service';

@Component({
  selector: 'ngx-update-core-type',
  templateUrl: './update-core-type.component.html',
  styleUrls: ['./update-core-type.component.scss']
})
export class UpdateCoreTypeComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: CoreTypeService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute
  ) {}
  form: FormGroup;
  submitted = false;
  npskpiWeightId: string;
  ngOnInit() {
    this.initForm();
    this.route.params.subscribe((params) => {
      this.npskpiWeightId = params["id"];
      this.service.getById(this.npskpiWeightId).subscribe((res) => {
        this.form.patchValue({
          npskpiWeightId: res.entity.npskpiWeightId,
          npskpiWeightName: res.entity.npskpiWeightName,
          npskpiWeightDesc: res.entity.npskpiWeightDesc,
        });
      });
    });
  }
  initForm() {
    this.form = this.formBuilder.group({
      npskpiWeightId: ["", Validators.required],
      npskpiWeightName: ["", Validators.required],
      npskpiWeightDesc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    const model = this.form.value as CoreTypeUpdateModel;
    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
          "Core Type"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/core-type/"]);
      }
    });
  }
  get fc() {
    return this.form.controls;
  }
}
