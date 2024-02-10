import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { NbDialogService, NbToastrService } from '@nebular/theme';
import { environment } from '../../../../environments/environment';
import { ClusterModel, ClusterSearchModel } from '../../../models/cluster/cluster.model';
import { ConfirmDialogComponent } from '../../../shared/components/showcase-dialog/confirm-dialog.component';
import { GetToastTitleAndMessage, ActionTypeEnum } from '../../../shared/messages';
import { ClusterService } from '../cluster.service';

@Component({
  selector: 'ngx-list-cluster',
  templateUrl: './list-cluster.component.html',
  styleUrls: ['./list-cluster.component.scss']
})
export class ListClusterComponent implements OnInit {

  constructor(
    private service: ClusterService,
    private dialogService: NbDialogService,
    private toasterService: NbToastrService
  ) {}
  clusters: ClusterModel[] = [];
  totalRecords: number = 0;
  searchModel: ClusterSearchModel;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "clusterNameEn",
    "clusterNameAr",
    "districtNameEn",
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
      this.clusters = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, name: string) {
    const title = "Delete cluster";
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
            this.clusters = this.clusters.filter(
              (x) => x.clusterId != id
            );
            var message = GetToastTitleAndMessage(
              ActionTypeEnum.Deleted,
              "Cluster"
            );
            this.toasterService.danger(message.message, message.title);
          });
        }
      });
  }
}
