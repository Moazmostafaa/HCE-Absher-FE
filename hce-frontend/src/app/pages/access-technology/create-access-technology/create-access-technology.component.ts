import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { AccessTechnologyCreateModel } from "../../../models/access-technology/AccessTechnology.model";
import { ActionTypeEnum, GetToastTitleAndMessage } from "../../../shared/messages";
import { AccessTechnologyService } from "../access-technology.service";

@Component({
  selector: "ngx-create-access-technology",
  templateUrl: "./create-access-technology.component.html",
  styleUrls: ["./create-access-technology.component.scss"],
})
export class CreateAccessTechnologyComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private service: AccessTechnologyService,
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
      GoalName: ["", Validators.required],
      GoalDesc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.createForm.invalid) {
      return;
    }
    const model = this.createForm.value as AccessTechnologyCreateModel;
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(ActionTypeEnum.Created, "Access Technology");
        this.toastrService.info(message.message,message.title);
        this.router.navigate(["/pages/access-technology/"]);
      }
    });
  }
  get fc() {
    return this.createForm.controls;
  }
}
