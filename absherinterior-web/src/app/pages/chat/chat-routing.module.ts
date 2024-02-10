import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ListChatsComponent } from "./list-chats/list-chats.component";
import { MychatsComponent } from "./list-chats/mychats/mychats.component";
import { UserschatsComponent } from "./list-chats/userschats/userschats.component";
import { GroupschatsComponent } from "./list-chats/groupschats/groupschats.component";

const routes: Routes = [
  {
    path: "",
    component: ListChatsComponent,
    children: [
      {
        path: "myChats",
        component: MychatsComponent,
      },
      {
        path: "usersChats",
        component: UserschatsComponent,
      },
      {
        path: "groupsChats",
        component: GroupschatsComponent,
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ChatRoutingModule {}
