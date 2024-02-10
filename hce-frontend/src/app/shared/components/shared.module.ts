import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import {
  NbActionsModule,
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
import { MatIconModule } from "@angular/material/icon";

@NgModule({
  declarations: [ConfirmDialogComponent],
  imports: [
    FormsModule,
    ThemeModule,
    NbDialogModule.forChild(),
    NbWindowModule.forChild(),
    NbCardModule,
    ThemeModule,
    NbInputModule,
    NbCardModule,
    NbButtonModule,
    NbActionsModule,
    MatIconModule,
  ],
  exports: [ConfirmDialogComponent],
  entryComponents: [ConfirmDialogComponent],
})
export class SharedModule {}
