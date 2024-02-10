import { Component, OnInit } from "@angular/core";
import { environment } from "../../../../environments/environment";

@Component({
  selector: "ngx-list-chats",
  templateUrl: "./list-chats.component.html",
  styleUrls: ["./list-chats.component.scss"],
})
export class ListChatsComponent implements OnInit {
  constructor() {}

  totalCount: number = 0;
  searchModel = {
    PageSize: 10,
    PageNumber: 1,
  };
  pageSizeOptions = environment.DEFAULT_PAGE_SIZE_OPTIONS;

  ngOnInit() {}
}
