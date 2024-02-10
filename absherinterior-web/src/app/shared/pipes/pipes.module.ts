import { NgModule } from "@angular/core";
import { Base64ImagePipe } from "./base64-image.pipe";
import { ByPassSecurityPipe } from "./bypass-security.pipe";
import { Base64VideoPipe } from './base64-video.pipe';
import { Base64DocumentPipe } from './base64-document.pipe';
import { UserImagePipe } from './user-image.pipe';

@NgModule({
  imports: [],
  declarations: [ByPassSecurityPipe, Base64ImagePipe, Base64VideoPipe, Base64DocumentPipe, UserImagePipe],
  exports: [ByPassSecurityPipe, Base64ImagePipe,UserImagePipe],
})
export class PipeModule {
  static forRoot() {
    return {
      ngModule: PipeModule,
      providers: [ByPassSecurityPipe, Base64ImagePipe,Base64VideoPipe,Base64DocumentPipe,UserImagePipe],
    };
  }
}
