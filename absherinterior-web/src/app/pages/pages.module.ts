import { NgModule } from "@angular/core";
import { NbButtonModule, NbCardModule, NbFormFieldModule, NbInputModule, NbMenuModule, NbUserModule } from "@nebular/theme";
import { ThemeModule } from "../@theme/theme.module";
import { PagesComponent } from "./pages.component";
import { PagesRoutingModule } from "./pages-routing.module";
import { MiscellaneousModule } from "./miscellaneous/miscellaneous.module";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { UserModule } from "./user/user.module";
import { EventModule } from "./event/event.module";
import { PostModule } from "./post/post.module";
import { ChatModule } from "./chat/chat.module";
import { CategoryModule } from "./category/category.module";
import { DocumentModule } from "./document/document.module";
import { TranslateModule } from "@ngx-translate/core";
import { ProfileComponent } from './profile/profile.component';
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";

@NgModule({
  imports: [
    PagesRoutingModule,
    ThemeModule,
    CommonModule,
    NbMenuModule,
    NbInputModule,
    NbCardModule,
    NbButtonModule,
    NbUserModule,
    NbFormFieldModule,
    FormsModule,
    MiscellaneousModule,
    UserModule,
    EventModule,
    PostModule,
    ChatModule,
    CategoryModule,
    DocumentModule,
    TranslateModule.forChild(),
  ],
  declarations: [PagesComponent, DashboardComponent, ProfileComponent],
})
export class PagesModule {}
