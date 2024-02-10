import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListNotificationsComponent } from './list-notifications/list-notifications.component';
import { SendNotificationComponent } from './send-notification/send-notification.component';

const routes: Routes = [{
  path: "",
  component: ListNotificationsComponent,
},
{
  path: "send",
  component: SendNotificationComponent,
},];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NotificationRoutingModule { }
