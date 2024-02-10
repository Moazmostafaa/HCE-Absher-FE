import { Component, Input } from '@angular/core';
import { NbDialogRef } from '@nebular/theme';

@Component({
  selector: 'ngx-confirm-dialog',
  templateUrl: 'confirm-dialog.component.html',
  styleUrls: ['confirm-dialog.component.scss'],
})
export class ConfirmDialogComponent {

  @Input() title: string;
  @Input() body: string;
  @Input() buttonText: string;

  constructor(protected ref: NbDialogRef<ConfirmDialogComponent>) {}

  dismiss() {
    this.ref.close();
  }
  confirm() {
    this.ref.close(true);
  }
}
