import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { environment } from "../../../../environments/environment";
import {
  AccessTechnologyModel,
  AccessTechnologySearchModel,
} from "../../../models/access-technology/AccessTechnology.model";
import { GoalModel, GoalSearchModel } from "../../../models/goal/goal.model";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import {
  ActionTypeEnum,
  GetToastTitleAndMessage,
} from "../../../shared/messages";
import { GoalService } from "../goal.service";


@Component({
  selector: 'ngx-list-goal',
  templateUrl: './list-goal.component.html',
  styleUrls: ['./list-goal.component.scss']
})
export class ListGoalComponent implements OnInit {

  constructor(
    private service: GoalService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  goals: GoalModel[] = [];
  totalRecords: number = 0;
  searchModel: GoalSearchModel;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "name",
    "description",
    "creationDate",
    "actions",
  ];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  ngOnInit() {
    this.searchModel = {
      PageNumber: 1,
      PageSize: environment.DEFAULT_PAGE_SIZE,
    };
    this.search();
  }
  search(page?: PageEvent) {
    if (page) {
      this.searchModel.PageNumber = page.pageIndex + 1;
      this.searchModel.PageSize = page.pageSize;
    }
    this.service.search(this.searchModel).subscribe((res) => {
      this.goals = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete Goal";
    const message = `Are you sure you want to delete ${name}?`;
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `${title}`,
          body: `${message}?`,
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.service.delete(id).subscribe((result) => {
            this.goals = this.goals.filter(
              (x) => x.goalId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "Goal"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
