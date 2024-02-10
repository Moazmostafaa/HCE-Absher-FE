import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { GoalUpdateModel } from "../../../models/goal/goal.model";
import { VendorUpdateModel } from "../../../models/vendor/vendor.model";
import {
  ActionTypeEnum,
  GetToastTitleAndMessage,
} from "../../../shared/messages";
import { VendorService } from "../vendor.service";


@Component({
  selector: 'ngx-update-vendor',
  templateUrl: './update-vendor.component.html',
  styleUrls: ['./update-vendor.component.scss']
})
export class UpdateVendorComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: VendorService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute
  ) {}
  updateForm: FormGroup;
  submitted = false;
  vendorId: string;
  ngOnInit() {
    this.initForm();
    this.route.params.subscribe((params) => {
      this.vendorId = params["id"];
      this.service.getById(this.vendorId).subscribe((res) => {
        this.updateForm.patchValue({
          vendorId: res.entity.vendorId,
          vendorName: res.entity.vendorName,
          vendorDesc: res.entity.vendorDesc,
        });
      });
    });
  }
  initForm() {
    this.updateForm = this.formBuilder.group({
      vendorId: ["", Validators.required],
      vendorName: ["", Validators.required],
      vendorDesc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.updateForm.invalid) {
      return;
    }
    const model = this.updateForm.value as VendorUpdateModel;
    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
          "Vendor"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/vendor/"]);
      }
    });
  }
  get fc() {
    return this.updateForm.controls;
  }
}
