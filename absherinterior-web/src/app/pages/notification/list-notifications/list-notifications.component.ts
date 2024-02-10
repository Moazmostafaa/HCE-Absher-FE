import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { NbDialogService } from '@nebular/theme';
import { TranslateService } from '@ngx-translate/core';
import { environment } from '../../../../environments/environment';
import { DocumentSearchModel } from '../../../models/document/document-search.model';
import { DocumentModel } from '../../../models/document/document.model';
import { NotificationModel } from '../../../models/notification/notification.model';
import { PagnationRequest } from '../../../models/pagination.request';
import { NotificationService } from '../../../services/notification.service';
import { ToastrService } from '../../../services/toastr.service';

@Component({
  selector: 'ngx-list-notifications',
  templateUrl: './list-notifications.component.html',
  styleUrls: ['./list-notifications.component.scss']
})
export class ListNotificationsComponent implements OnInit {
  dir = "ltr";
  constructor(
    private notificationService: NotificationService,
    private dialogService: NbDialogService,
    private toastrService: ToastrService,
    private translate: TranslateService,
  ) {
    if (translate.currentLang == "ar") this.dir = "rtl";
  }
  notifications: NotificationModel[] = [];
  searchModel: PagnationRequest;
  totalRecords: number = 0;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "title",
    "message",
    "creationDate",
    "isBulk"
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
    this.notificationService.searchDocuments(this.searchModel)
      .subscribe((res) => {
        this.notifications = res.entity.entities;
        this.totalRecords = res.entity.totalRecords;
      });
  }

}
