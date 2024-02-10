import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { GoalCreateModel, GoalModel } from "../../../models/goal/goal.model";
import { ActionTypeEnum, GetToastTitleAndMessage } from "../../../shared/messages";
import { GoalService } from "../goal.service";

@Component({
  selector: 'ngx-create-goal',
  templateUrl: './create-goal.component.html',
  styleUrls: ['./create-goal.component.scss']
})
export class CreateGoalComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private service: GoalService,
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
      goalName: ["", Validators.required],
      goalDesc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.createForm.invalid) {
      return;
    }
    const model = this.createForm.value as GoalCreateModel;
    this.service.create(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(ActionTypeEnum.Created, "Goal");
        this.toastrService.info(message.message,message.title);
        this.router.navigate(["/pages/goal/"]);
      }
    });
  }
  get fc() {
    return this.createForm.controls;
  }
}