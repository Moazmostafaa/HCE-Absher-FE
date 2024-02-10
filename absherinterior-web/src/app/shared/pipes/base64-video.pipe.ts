import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'base64Video'
})
export class Base64VideoPipe implements PipeTransform {

  transform(base64: string, extension: string): string {
    if (!base64) return "assets/images/placeholder.png";
    let result = "";
     if (
      extension == ".mp4" ||
      extension == ".mov" ||
      extension == ".avi" ||
      extension == ".3gp"
    ){
      result = `data:video/${extension.substring(1)};base64,${base64}`;
    } else {
      result = "assets/images/video-placeholder.png";
    }

    return result;
  }

}
