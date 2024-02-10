import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { NbDialogService, NbToastrService } from '@nebular/theme';
import { environment } from '../../../../environments/environment';
import { CityModel, CitySearchModel } from '../../../models/city/City.model';
import { ConfirmDialogComponent } from '../../../shared/components/showcase-dialog/confirm-dialog.component';
import { GetToastTitleAndMessage, ActionTypeEnum } from '../../../shared/messages';
import { CityService } from '../city.service';

@Component({
  selector: 'ngx-list-city',
  templateUrl: './list-city.component.html',
  styleUrls: ['./list-city.component.scss']
})
export class ListCityComponent implements OnInit {

  constructor(
    private service: CityService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  cities: CityModel[] = [];
  totalRecords: number = 0;
  searchModel: CitySearchModel;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "cityNameEn",
    "cityNameAr",
    "stateRegionNameEn",
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
      this.cities = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete city";
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
            this.cities = this.cities.filter(
              (x) => x.cityId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "City"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
