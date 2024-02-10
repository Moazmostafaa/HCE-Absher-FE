import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatTableModule } from "@angular/material/table";
import {
  NbInputModule,
  NbCardModule,
  NbButtonModule,
  NbActionsModule,
  NbUserModule,
  NbCheckboxModule,
  NbRadioModule,
  NbDatepickerModule,
  NbSelectModule,
  NbIconModule,
  NbFormFieldModule,
  NbToggleModule,
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { Ng2TelInputModule } from "ng2-tel-input";
import { ListCategoriesComponent } from "./list-categories/list-categories.component";
import { AddCategorieComponent } from "./add-categorie/add-categorie.component";
import { CategoryRoutingModule } from "./category-routing.module";
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { PipeModule } from "../../shared/pipes/pipes.module";



@NgModule({
  declarations: [ListCategoriesComponent , AddCategorieComponent, EditCategoryComponent],
  imports: [
    CommonModule,
    CategoryRoutingModule,
    ThemeModule,
    NbInputModule,
    NbCardModule,
    NbButtonModule,
    NbActionsModule,
    NbFormFieldModule,
    NbUserModule,
    NbCheckboxModule,
    NbRadioModule,
    NbDatepickerModule,
    NbSelectModule,
    NbIconModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    Ng2TelInputModule,
    SharedModule,
    PipeModule.forRoot(),
    NbToggleModule,
  ],
})
export class CategoryModule {
  [x: string]: string;
  assigneesusers: string;
}
