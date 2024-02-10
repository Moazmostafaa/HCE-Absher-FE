import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { NbDialogService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { environment } from "../../../../environments/environment";
import { BlockLawModel } from "../../../models/block-law/block-law.model";
import { CategorySearchModel } from "../../../models/category/category-search.model";
import { PagnationRequest } from "../../../models/pagination.request";
import { BlockLawService } from "../../../services/block-law.service";
import { ToastrService } from "../../../services/toastr.service";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { EntityNames } from "../../../shared/Entity-Names";

@Component({
  selector: "ngx-list-block-laws",
  templateUrl: "./list-block-laws.component.html",
  styleUrls: ["./list-block-laws.component.scss"],
})
export class ListBlockLawsComponent implements OnInit {
  dir = "ltr";
  constructor(
    private dialogService: NbDialogService,
    private toastrService: ToastrService,
    private translate: TranslateService,
    private blockLawService: BlockLawService,
  ) {
    if (translate.currentLang == "ar") this.dir = "rtl";
  }
  blockLaws: BlockLawModel[] = [];
  totalRecords: number = 0;
  searchModel: PagnationRequest;
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;
  displayedColumns: string[] = [
    "lawNumber",
    "messageAr",
    "messageEn",
    "actions",
  ];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  ngOnInit(){
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
    this.blockLawService.searchBlockLaws(this.searchModel).subscribe((res) => {
      this.blockLaws = res.entity.entities;
      this.totalRecords = res.entity.totalRecords;
    });
  }
  async delete(id: string, nameAr: string, nameEn: string) {
    let name = this.translate.currentLang == "ar" ? nameAr : nameEn;
    name = await this.translate.get(name).toPromise();
    let title = await this.translate.get("DeleteTitle", { entity: name }).toPromise();
    let body = await this.translate.get("DeleteMessage", { entity: name }).toPromise();
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `${title}`,
          body: `${body}?`,
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.blockLawService.deleteBlockLaw(id).subscribe((result) => {
            this.blockLaws = this.blockLaws.filter((x) => x.userBlockLawId != id);
            this.toastrService.Delete(EntityNames.Category);
          });
        }
      });
  }
}
