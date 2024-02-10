import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import {
  NbActionsModule,
  NbAlertModule,
  NbButtonModule,
  NbCardModule,
  NbCheckboxModule,
  NbDialogModule,
  NbInputModule,
  NbPopoverModule,
  NbSelectModule,
  NbTabsetModule,
  NbTooltipModule,
  NbWindowModule,
} from "@nebular/theme";
import { ThemeModule } from "../../@theme/theme.module";
import { ConfirmDialogComponent } from "./showcase-dialog/confirm-dialog.component";
import { LoginComponent } from "../../login/login.component";
import { ChangePasswordComponent } from "../../change-password/change-password.component";

@NgModule({
  declarations: [ConfirmDialogComponent, LoginComponent, ChangePasswordComponent],
  imports: [
    FormsModule,
    ThemeModule,
    NbDialogModule.forChild(),
    NbWindowModule.forChild(),
    NbCardModule,
    ThemeModule,
    NbInputModule,
    NbAlertModule,
    NbCardModule,
    NbButtonModule,
    NbActionsModule,
  ],
  exports: [ConfirmDialogComponent],
  entryComponents: [ConfirmDialogComponent],
})
export class SharedModule {}
