import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddBlockLawComponent } from './add-block-law/add-block-law.component';
import { EditBlockLawComponent } from './edit-block-law/edit-block-law.component';
import { ListBlockLawsComponent } from './list-block-laws/list-block-laws.component';

const routes: Routes = [
  {
    path: '',
    component: ListBlockLawsComponent,
  },
  {
    path: 'add',
    component: AddBlockLawComponent,
  },
  {
    path: 'edit/:id',
    component: EditBlockLawComponent,
  },
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BlockLawRoutingModule { }
