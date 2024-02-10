import { Directive, ElementRef, Input } from "@angular/core";

@Directive({
  selector: "[captureVideoThumbnail]",
})
export class CaptureVideoThumbnailDirective {
  @Input("video") video: any;
  constructor(private canvas: ElementRef) {
    var video = document.createElement("video");

    video.src = this.video;
    video.autoplay = true;
    var context = this.canvas.nativeElement
      .getContext("2d")
      .drawImage(video, 0, 0, 1024, 768);
    this.canvas.nativeElement.toDataURL("image/png");
  }
}
