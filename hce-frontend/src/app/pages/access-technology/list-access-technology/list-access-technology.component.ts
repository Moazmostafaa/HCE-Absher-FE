import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { environment } from "../../../../environments/environment";
import {
  AccessTechnologyModel,
  AccessTechnologySearchModel,
} from "../../../models/access-technology/AccessTechnology.model";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import {
  ActionTypeEnum,
  GetToastTitleAndMessage,
} from "../../../shared/messages";
import { AccessTechnologyService } from "../access-technology.service";

@Component({
  selector: "ngx-list-access-technology",
  templateUrl: "./list-access-technology.component.html",
  styleUrls: ["./list-access-technology.component.scss"],
})
export class ListAccessTechnologyComponent implements OnInit {
  constructor(
    private service: AccessTechnologyService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  technologies: AccessTechnologyModel[] = [];
  totalRecords: number = 0;
  searchModel: AccessTechnologySearchModel;
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
      this.technologies = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete Access Technology";
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
            this.technologies = this.technologies.filter(
              (x) => x.serviceId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "Access Technology"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
