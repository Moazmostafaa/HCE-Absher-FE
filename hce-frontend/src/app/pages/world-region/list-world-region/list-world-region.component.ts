import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { environment } from "../../../../environments/environment";
import {
  WorldRegionModel,
  WorldRegionSearchModel,
} from "../../../models/world-region/WorldRegion.model";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { WorldRegionService } from "../world-region.service";

@Component({
  selector: "ngx-list-world-region",
  templateUrl: "./list-world-region.component.html",
  styleUrls: ["./list-world-region.component.scss"],
})
export class ListWorldRegionComponent implements OnInit {
  constructor(
    private service: WorldRegionService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  worldRegions: WorldRegionModel[] = [];
  totalRecords: number = 0;
  searchModel: WorldRegionSearchModel;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "regionNameEn",
    "regionNameAr",
    "regionNameLang",
    "regionDesc",
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
      this.worldRegions = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete World Region";
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
            this.worldRegions = this.worldRegions.filter(
              (x) => x.regionId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "World Region"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
