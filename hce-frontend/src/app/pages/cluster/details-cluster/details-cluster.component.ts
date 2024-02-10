import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ClusterModel } from '../../../models/cluster/cluster.model';
import { ClusterService } from '../cluster.service';

@Component({
  selector: 'ngx-details-cluster',
  templateUrl: './details-cluster.component.html',
  styleUrls: ['./details-cluster.component.scss']
})
export class DetailsClusterComponent implements OnInit {
  constructor(
    private service: ClusterService,
    private route: ActivatedRoute
  ) {}
  cluster: ClusterModel = {
    clusterId: "",
    clusterNameAr: "",
    clusterNameEn: "",
    clusterNameLang: "",
    clusterDesc: "",
    createdBy: "",
    creationDate: "",
    districtId: "",
    districtNameAr: "",
    districtNameEn: "",
    districtNameLang: "",
  };
  ngOnInit() {
    this.route.params.subscribe((params) => {
      let clusterId = params["id"];
      this.service.getById(clusterId).subscribe((res) => {
        this.cluster = res.entity;
      });
    });
  }
}
