import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { environment } from "../../../../environments/environment";
import { VendorModel, VendorSearchModel } from "../../../models/vendor/vendor.model";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import {
  ActionTypeEnum,
  GetToastTitleAndMessage,
} from "../../../shared/messages";
import { VendorService } from "../vendor.service";


@Component({
  selector: 'ngx-list-vendor',
  templateUrl: './list-vendor.component.html',
  styleUrls: ['./list-vendor.component.scss']
})
export class ListVendorComponent implements OnInit {

  constructor(
    private service: VendorService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  vendors: VendorModel[] = [];
  totalRecords: number = 0;
  searchModel: VendorSearchModel;
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
      this.vendors = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete Vendor";
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
            this.vendors = this.vendors.filter(
              (x) => x.vendorId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "Vendor"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
