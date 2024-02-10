import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { environment } from "../../../../environments/environment";
import { StateRegionModel, StateRegionSearchModel } from "../../../models/state-region/state-region.model";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { ActionTypeEnum, GetToastTitleAndMessage } from "../../../shared/messages";
import { StateRegionService } from "../state-region.service";


@Component({
  selector: 'ngx-list-state-region',
  templateUrl: './list-state-region.component.html',
  styleUrls: ['./list-state-region.component.scss']
})
export class ListStateRegionComponent implements OnInit {

  constructor(
    private service: StateRegionService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  StateRegions: StateRegionModel[] = [];
  totalRecords: number = 0;
  searchModel: StateRegionSearchModel;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "regionNameEn",
    "regionNameAr",
    "countryNameEn",
    "creationDate",
    "actions"
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
      this.StateRegions = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete State Region";
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
            this.StateRegions = this.StateRegions.filter(
              (x) => x.regionId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "State Region"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
