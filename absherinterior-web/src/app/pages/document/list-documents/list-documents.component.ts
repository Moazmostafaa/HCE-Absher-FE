import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService, NbToastrService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import { DocumentSearchModel } from "../../../models/document/document-search.model";
import { DocumentModel } from "../../../models/document/document.model";
import { KnowledgeCenterService } from "../../../services/KnowledgeCenter.service";
import { ToastrService } from "../../../services/toastr.service";
import { UserService } from "../../../services/user.service";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { EntityNames } from "../../../shared/Entity-Names";

@Component({
  selector: "ngx-list-documents",
  templateUrl: "./list-documents.component.html",
  styleUrls: ["./list-documents.component.scss"],
})
export class ListDocumentsComponent implements OnInit {
  active = true;
  dir = "ltr";
  constructor(
    private knowledgeCenterService: KnowledgeCenterService,
    private dialogService: NbDialogService,
    private toastrService: ToastrService,
    private translate: TranslateService,
  ) {
    if (translate.currentLang == "ar") this.dir = "rtl";
  }
  documents: DocumentModel[] = [];
  searchModel: DocumentSearchModel;
  totalRecords: number = 0;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "name",
    "description",
    "creationDate",
    "isKnowledgeCenterOwner",
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
    this.knowledgeCenterService.searchDocuments(this.searchModel)
      .subscribe((res) => {
        this.documents = res.entity.entities;
        this.totalRecords = res.entity.totalRecords;
      });
  }
  async delete(id: string, name: string) {
    let title = await this.translate.get("DeleteTitle", { entity: name }).toPromise();
    let body = await this.translate.get("DeleteMessage", { entity: name }).toPromise();
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `${title}`,
          body: `${body}?`
        },
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.knowledgeCenterService.deleteDocument(id).subscribe((result) => {
            this.documents = this.documents.filter((x) => x.docId != id);
            this.toastrService.Delete(EntityNames.Document);
          });
        }
      });
  }
}
