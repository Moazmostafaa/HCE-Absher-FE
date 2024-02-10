import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { GoalModel } from "../../../models/goal/goal.model";
import { VendorModel } from "../../../models/vendor/vendor.model";
import { ActionTypeEnum, GetToastTitleAndMessage } from "../../../shared/messages";
import { VendorService } from "../vendor.service";



@Component({
  selector: 'ngx-create-vendor',
  templateUrl: './create-vendor.component.html',
  styleUrls: ['./create-vendor.component.scss']
})
export class CreateVendorComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private service: VendorService,
    private router: Router,
    private toastrService: NbToastrService
  ) {}
  createForm: FormGroup;
  submitted = false;
  ngOnInit() {
    this.initForm();
  }
  initForm() {
    this.createForm = this.formBuilder.group({
      vendorName: ["", Validators.required],
      vendorDesc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.createForm.invalid) {
      return;
    }
    const model = this.createForm.value as VendorModel;
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(ActionTypeEnum.Created, "Vendor");
        this.toastrService.info(message.message,message.title);
        this.router.navigate(["/pages/vendor/"]);
      }
    });
  }
  get fc() {
    return this.createForm.controls;
  }
}