import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateClusterComponent } from './create-cluster/create-cluster.component';
import { DetailsClusterComponent } from './details-cluster/details-cluster.component';
import { ListClusterComponent } from './list-cluster/list-cluster.component';
import { UpdateClusterComponent } from './update-cluster/update-cluster.component';

const routes: Routes = [
  {
    path: "",
    component: ListClusterComponent,
  },
  {
    path: "details/:id",
    component: DetailsClusterComponent,
  },
  {
    path: "create",
    component: CreateClusterComponent,
  },
  {
    path: "update/:id",
    component: UpdateClusterComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClusterRoutingModule { }
