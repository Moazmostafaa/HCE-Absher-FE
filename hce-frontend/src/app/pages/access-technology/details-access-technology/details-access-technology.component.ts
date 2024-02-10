import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { AccessTechnologyModel } from "../../../models/access-technology/AccessTechnology.model";
import { AccessTechnologyService } from "../access-technology.service";

@Component({
  selector: "ngx-details-access-technology",
  templateUrl: "./details-access-technology.component.html",
  styleUrls: ["./details-access-technology.component.scss"],
})
export class DetailsAccessTechnologyComponent implements OnInit {
  constructor(
    private service: AccessTechnologyService,
    private route: ActivatedRoute
  ) {}
  technology: AccessTechnologyModel = {
    serviceId: "",
    serviceName: "",
    serviceDesc: "",
    creationDate: "",
    createdBy: "",
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let serviceId = params["id"];
      this.service.getById(serviceId).subscribe((res) => {
        this.technology = res.entity;
      });
    });
  }
}
