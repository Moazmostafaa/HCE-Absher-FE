import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { environment } from "../../../../environments/environment";
import {
  CoreTypeModel,
  CoreTypeSearchModel,
} from "../../../models/core-type/CoreType.model";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import {
  GetToastTitleAndMessage,
  ActionTypeEnum,
} from "../../../shared/messages";
import { CoreTypeService } from "../core-type.service";

@Component({
  selector: "ngx-list-core-type",
  templateUrl: "./list-core-type.component.html",
  styleUrls: ["./list-core-type.component.scss"],
})
export class ListCoreTypeComponent implements OnInit {
  constructor(
    private service: CoreTypeService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  coreTypes: CoreTypeModel[] = [];
  totalRecords: number = 0;
  searchModel: CoreTypeSearchModel;
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
      this.coreTypes = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete Core Type";
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
            this.coreTypes = this.coreTypes.filter(
              (x) => x.npskpiWeightId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "Core Type"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
