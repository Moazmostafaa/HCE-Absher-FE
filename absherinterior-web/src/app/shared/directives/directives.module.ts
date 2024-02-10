import { NgModule } from "@angular/core";
import { CaptureVideoThumbnailDirective } from "./canvas.video-capture.directive";

@NgModule({
  imports: [],
  declarations: [CaptureVideoThumbnailDirective],
  exports: [CaptureVideoThumbnailDirective],
})
export class DirectivesModule {
  static forRoot() {
    return {
      ngModule: DirectivesModule,
      providers: [CaptureVideoThumbnailDirective],
    };
  }
}
