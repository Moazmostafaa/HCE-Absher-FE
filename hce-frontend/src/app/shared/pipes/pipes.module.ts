import { NgModule } from "@angular/core";
import { TimeFormatterPipe } from "./time-format.pipe";

@NgModule({
  imports: [],
  declarations: [TimeFormatterPipe],
  exports: [TimeFormatterPipe],
})
export class PipeModule {
  static forRoot() {
    return {
      ngModule: PipeModule,
      providers: [TimeFormatterPipe],
    };
  }
}
