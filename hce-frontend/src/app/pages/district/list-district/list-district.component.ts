import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { NbDialogService, NbToastrService } from '@nebular/theme';
import { environment } from '../../../../environments/environment';
import { DistrictModel, DistrictSearchModel } from '../../../models/district/district.model';
import { ConfirmDialogComponent } from '../../../shared/components/showcase-dialog/confirm-dialog.component';
import { GetToastTitleAndMessage, ActionTypeEnum } from '../../../shared/messages';
import { DistrictService } from '../district.service';

@Component({
  selector: 'ngx-list-district',
  templateUrl: './list-district.component.html',
  styleUrls: ['./list-district.component.scss']
})
export class ListDistrictComponent implements OnInit {

  constructor(
    private service: DistrictService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  districts: DistrictModel[] = [];
  totalRecords: number = 0;
  searchModel: DistrictSearchModel;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "districtNameEn",
    "districtNameAr",
    "cityNameEn",
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
      this.districts = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete district";
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
            this.districts = this.districts.filter(
              (x) => x.districtId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "District"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
