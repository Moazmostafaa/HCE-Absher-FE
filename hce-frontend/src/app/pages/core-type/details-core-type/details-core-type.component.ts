import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CoreTypeModel } from '../../../models/core-type/CoreType.model';
import { CoreTypeService } from '../core-type.service';

@Component({
  selector: 'ngx-details-core-type',
  templateUrl: './details-core-type.component.html',
  styleUrls: ['./details-core-type.component.scss']
})
export class DetailsCoreTypeComponent implements OnInit {
  constructor(
    private service: CoreTypeService,
    private route: ActivatedRoute
  ) {}
  coreType: CoreTypeModel = {
    npskpiWeightId: "",
    createdBy: "",
    creationDate: "",
    npskpiWeightDesc: "",
    npskpiWeightName: ""
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let serviceId = params["id"];
      this.service.getById(serviceId).subscribe((res) => {
        this.coreType = res.entity;
      });
    });
  }
}
