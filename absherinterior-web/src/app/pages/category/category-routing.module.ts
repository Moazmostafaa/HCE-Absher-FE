import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddCategorieComponent } from './add-categorie/add-categorie.component';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { ListCategoriesComponent } from './list-categories/list-categories.component';




const routes: Routes = [
  {
    path: '',
    component: ListCategoriesComponent,
  },
  {
    path: 'add',
    component: AddCategorieComponent,
  },
  {
    path: 'edit/:id',
    component: EditCategoryComponent,
  },
 
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})


export class CategoryRoutingModule { }
