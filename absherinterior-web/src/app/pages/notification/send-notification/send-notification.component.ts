import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbDialogService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { get } from "http";
import { NotificationService } from "../../../services/notification.service";
import { ToastrService } from "../../../services/toastr.service";
import { ConfirmDialogComponent } from "../../../shared/components/showcase-dialog/confirm-dialog.component";
import { EntityNames } from "../../../shared/Entity-Names";

@Component({
  selector: "ngx-send-notification",
  templateUrl: "./send-notification.component.html",
  styleUrls: ["./send-notification.component.scss"],
})
export class SendNotificationComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private notificationService: NotificationService,
    private dialogService: NbDialogService,
    private toastrService: ToastrService,
    private translate: TranslateService
  ) {}

  notificationForm: FormGroup;
  submitted = false;
  ngOnInit() {
    this.initForm();
  }
  initForm() {
    this.notificationForm = this.formBuilder.group({
      title: ["", [Validators.required, Validators.minLength(3)]],
      message: ["", [Validators.required, Validators.minLength(3)]],
    });
  }
  get fc() {
    return this.notificationForm.controls;
  }
  async onSubmit() {
    this.submitted = true;
    if (this.notificationForm.invalid) {
      return;
    }

    let title = await this.translate.get("SendNotificationTitle").toPromise();
    let body = await this.translate.get("SendNotificationMessage").toPromise();
    let buttonText = await this.translate.get("Send").toPromise();
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `${title}`,
          body: `${body}?`,
          buttonText: `${buttonText}`,
          confirmStatus: "success"
        },
        closeOnBackdropClick: false,
      })
      .onClose.subscribe((confirmed) => {
        if (confirmed) {
          const notification = this.notificationForm.value;
          this.notificationService
            .sendNotification(notification)
            .subscribe((res) => {
              if (res.status == 200) {
                this.toastrService.Create(EntityNames.Notification);
                this.router.navigate(["/pages/notification"]);
              }
            });
        }
      });
  }
}
