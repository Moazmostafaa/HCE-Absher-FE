import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEventComponent } from './add-event/add-event.component';
import { EditEventComponent } from './edit-event/edit-event.component';
import { ListEventsComponent } from './list-events/list-events.component';
import { ViewEventComponent } from './view-event/view-event.component';

const routes: Routes = [
  {
    path: '',
    component: ListEventsComponent,
  },
  {
    path: 'add',
    component: AddEventComponent,
  },
  {
    path: 'edit/:id',
    component: EditEventComponent,
  },
  {
    path: 'view/:id',
    component: ViewEventComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})


export class EventRoutingModule { }
