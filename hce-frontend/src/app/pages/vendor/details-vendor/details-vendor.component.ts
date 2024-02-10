import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { AccessTechnologyModel } from "../../../models/access-technology/AccessTechnology.model";
import { VendorModel } from "../../../models/vendor/vendor.model";
import { VendorService } from "../vendor.service";



@Component({
  selector: 'ngx-details-vendor',
  templateUrl: './details-vendor.component.html',
  styleUrls: ['./details-vendor.component.scss']
})
export class DetailsVendorComponent implements OnInit {

  constructor(
    private service: VendorService,
    private route: ActivatedRoute
  ) {}
  vendor: VendorModel = {
    vendorId: "",
    vendorName: "",
    vendorDesc: "",
    creationDate: "",
    createdBy: "",
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let vendorId = params["id"];
      this.service.getById(vendorId).subscribe((res) => {
        this.vendor = res.entity;
      });
    });
  }
}

