
import { MatTableModule } from "@angular/material/table";
import { MatIconModule } from "@angular/material/icon";
import {
  NbInputModule,
  NbCardModule,
  NbButtonModule,
  NbActionsModule,
  NbFormFieldModule,
  NbIconModule,
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { PipeModule } from "../../shared/pipes/pipes.module";
import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatSortModule } from "@angular/material/sort";
import { MatPaginatorModule } from "@angular/material/paginator";
import { GoalRoutingModule } from "./goal-routing.module";
import { ListGoalComponent } from "./list-goal/list-goal.component";
import { DetailsGoalComponent } from "./details-goal/details-goal.component";
import { CreateGoalComponent } from "./create-goal/create-goal.component";
import { UpdateGoalComponent } from "./update-goal/update-goal.component";

@NgModule({
  declarations: [
    ListGoalComponent,
    DetailsGoalComponent,
    CreateGoalComponent,
    UpdateGoalComponent
  ],
  imports: [
    CommonModule,
    GoalRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    ThemeModule,
    NbInputModule,
    NbCardModule,
    NbButtonModule,
    NbActionsModule,
    NbFormFieldModule,
    NbIconModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    SharedModule,
    PipeModule.forRoot(),
  ],
})
export class GoalModule {}
