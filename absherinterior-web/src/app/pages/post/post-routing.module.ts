import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AddPostComponent } from "./add-post/add-post.component";
import { ListPostsComponent } from "./list-posts/list-posts.component";
import { EditPostComponent } from "./edit-post/edit-post.component";
import { ViewPostComponent } from "./view-post/view-post.component";

const routes: Routes = [
  {
    path: "",
    component: ListPostsComponent,
  },
  {
    path: "add",
    component: AddPostComponent,
  },
  {
    path: "edit/:id",
    component: EditPostComponent,
  },
  {
    path: 'view/:id',
    component: ViewPostComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PostRoutingModule {}
