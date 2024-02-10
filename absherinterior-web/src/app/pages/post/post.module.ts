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
  NbSpinnerModule,
  NbAccordionModule,
  NbPopoverModule,
  NbProgressBarModule,
  NbWindowModule,
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { SharedModule } from "../../shared/components/shared.module";
import { Ng2TelInputModule } from "ng2-tel-input";
import { ListPostsComponent } from "./list-posts/list-posts.component";
import { AddPostComponent } from "./add-post/add-post.component";
import { PostRoutingModule } from "./post-routing.module";
import { EditPostComponent } from './edit-post/edit-post.component';
import { PipeModule } from "../../shared/pipes/pipes.module";
import { DirectivesModule } from "../../shared/directives/directives.module";
import { ViewPostComponent } from './view-post/view-post.component';
import { PostCommentsComponent } from './post-comments/post-comments.component';
import { PostFilesComponent } from './post-files/post-files.component';
import { SharedKnowledgeCenterDocComponent } from './shared-knowledge-center-doc/shared-knowledge-center-doc.component';


@NgModule({
  declarations: [ListPostsComponent , AddPostComponent, EditPostComponent, ViewPostComponent, PostCommentsComponent, PostFilesComponent, SharedKnowledgeCenterDocComponent],
  imports: [
    NbAccordionModule,
    CommonModule,
    PostRoutingModule,
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
    NbSpinnerModule,
    FormsModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    NbPopoverModule,
    MatSortModule,
    MatPaginatorModule,
    ReactiveFormsModule,
    Ng2TelInputModule,
    NbWindowModule,
    SharedModule,
    NbProgressBarModule,
    NbToggleModule,
    PipeModule.forRoot(),
    DirectivesModule.forRoot(),
  ],
})
export class PostModule {}
