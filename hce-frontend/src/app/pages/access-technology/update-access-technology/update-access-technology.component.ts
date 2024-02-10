import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { AccessTechnologyUpdateModel } from "../../../models/access-technology/AccessTechnology.model";
import {
  ActionTypeEnum,
  GetToastTitleAndMessage,
} from "../../../shared/messages";
import { AccessTechnologyService } from "../access-technology.service";

@Component({
  selector: "ngx-update-access-technology",
  templateUrl: "./update-access-technology.component.html",
  styleUrls: ["./update-access-technology.component.scss"],
})
export class UpdateAccessTechnologyComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: AccessTechnologyService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute
  ) {}
  updateForm: FormGroup;
  submitted = false;
  serviceId: string;
  ngOnInit() {
    this.initForm();
    this.route.params.subscribe((params) => {
      this.serviceId = params["id"];
      this.service.getById(this.serviceId).subscribe((res) => {
        this.updateForm.patchValue({
          serviceId: res.entity.serviceId,
          serviceName: res.entity.serviceName,
          serviceDesc: res.entity.serviceDesc,
        });
      });
    });
  }
  initForm() {
    this.updateForm = this.formBuilder.group({
      serviceId: ["", Validators.required],
      serviceName: ["", Validators.required],
      serviceDesc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.updateForm.invalid) {
      return;
    }
    const model = this.updateForm.value as AccessTechnologyUpdateModel;
    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
          "Access Technology"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/access-technology/"]);
      }
    });
  }
  get fc() {
    return this.updateForm.controls;
  }
}
