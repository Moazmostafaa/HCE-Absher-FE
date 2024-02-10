import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";

import { PagesComponent } from "./pages.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { NotFoundComponent } from "./miscellaneous/not-found/not-found.component";
import { ProfileComponent } from "./profile/profile.component";

const routes: Routes = [
  {
    path: "",
    component: PagesComponent,
    children: [
      {
        path: "profile",
        component: ProfileComponent,
      },
      {
        path: "dashboard",
        loadChildren: () =>
          import("./statistcs-dashboard/statistcs-dashboard.module").then(
            (m) => m.StatistcsDashboardModule
          ),
      },
      
      {
        path: "miscellaneous",
        loadChildren: () =>
          import("./miscellaneous/miscellaneous.module").then(
            (m) => m.MiscellaneousModule
          ),
      },
      {
        path: "user",
        loadChildren: () =>
          import("./user/user.module").then((m) => m.UserModule),
      },
      {
        path: "block-law",
        loadChildren: () =>
          import("./block-law/block-law.module").then((m) => m.BlockLawModule),
      },
      {
        path: "event",
        loadChildren: () =>
          import("./event/event.module").then((m) => m.EventModule),
      },
      {
        path: "post",
        loadChildren: () =>
          import("./post/post.module").then((m) => m.PostModule),
      },
      {
        path: "chat",
        loadChildren: () =>
          import("./chat/chat.module").then((m) => m.ChatModule),
      },
      {
        path: "category",
        loadChildren: () =>
          import("./category/category.module").then((m) => m.CategoryModule),
      },
      {
        path: "document",
        loadChildren: () =>
          import("./document/document.module").then((m) => m.DocumentModule),
      },
      {
        path: "notification",
        loadChildren: () =>
          import("./notification/notification.module").then((m) => m.NotificationModule),
      },
      
      {
        path: "",
        redirectTo: "dashboard",
        pathMatch: "full",
      },
      {
        path: "**",
        component: NotFoundComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
