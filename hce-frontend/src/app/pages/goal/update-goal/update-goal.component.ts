import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { AccessTechnologyUpdateModel } from "../../../models/access-technology/AccessTechnology.model";
import { GoalUpdateModel } from "../../../models/goal/goal.model";
import {
  ActionTypeEnum,
  GetToastTitleAndMessage,
} from "../../../shared/messages";
import { GoalService } from "../goal.service";

@Component({
  selector: 'ngx-update-goal',
  templateUrl: './update-goal.component.html',
  styleUrls: ['./update-goal.component.scss']
})
export class UpdateGoalComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private service: GoalService,
    private router: Router,
    private toastrService: NbToastrService,
    private route: ActivatedRoute
  ) {}
  updateForm: FormGroup;
  submitted = false;
  goalId: string;
  ngOnInit() {
    this.initForm();
    this.route.params.subscribe((params) => {
      this.goalId = params["id"];
      this.service.getById(this.goalId).subscribe((res) => {
        this.updateForm.patchValue({
          goalId: res.entity.goalId,
          goalName: res.entity.goalName,
          goalDesc: res.entity.goalDesc,
        });
      });
    });
  }
  initForm() {
    this.updateForm = this.formBuilder.group({
      goalId: ["", Validators.required],
      goalName: ["", Validators.required],
      goalDesc: ["", Validators.required],
    });
  }
  onSubmit() {
    this.submitted = true;
    if (this.updateForm.invalid) {
      return;
    }
    const model = this.updateForm.value as GoalUpdateModel;
    this.service.update(model).subscribe((res) => {
      if (res.status == 200) {
        var message = GetToastTitleAndMessage(
          ActionTypeEnum.Updated,
          "Goal"
        );
        this.toastrService.info(message.message, message.title);
        this.router.navigate(["/pages/goal/"]);
      }
    });
  }
  get fc() {
    return this.updateForm.controls;
  }
}
