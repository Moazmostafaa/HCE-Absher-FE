import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { AccessTechnologyModel } from "../../../models/access-technology/AccessTechnology.model";
import { GoalModel } from "../../../models/goal/goal.model";
import { GoalService } from "../goal.service";

@Component({
  selector: 'ngx-details-goal',
  templateUrl: './details-goal.component.html',
  styleUrls: ['./details-goal.component.scss']
})
export class DetailsGoalComponent implements OnInit {

  constructor(
    private service: GoalService,
    private route: ActivatedRoute
  ) {}
  goal: GoalModel = {
    goalId: "",
    goalName: "",
    goalDesc: "",
    creationDate: "",
    createdBy: "",
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let goalId = params["id"];
      this.service.getById(goalId).subscribe((res) => {
        this.goal = res.entity;
      });
    });
  }
}

