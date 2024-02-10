import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddDocumentComponent } from './add-document/add-document.component';
import { EditDocumentComponent } from './edit-document/edit-document.component';
import { ListDocumentsComponent } from './list-documents/list-documents.component';





const routes: Routes = [
  {
    path: '',
    component: ListDocumentsComponent,
  },
  {
    path: 'add',
    component: AddDocumentComponent,
  },
  {
    path: 'edit/:id',
    component: EditDocumentComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})


export class DocumentRoutingModule { }
