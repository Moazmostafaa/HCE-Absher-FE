import { Pipe, PipeTransform, SecurityContext } from "@angular/core";
import { DomSanitizer, SafeHtml } from "@angular/platform-browser";

@Pipe({
  name: "base64Image",
})
export class Base64ImagePipe implements PipeTransform {
  transform(base64: string, extension: string): string {
    if (!base64) return "assets/images/placeholder.png";
    if(base64.substring(0,10)== "data:image") return base64;
    let result = "";
    if (extension == ".jpg" || extension == ".png" || extension == ".gif" || extension == ".jpeg") {
      result = `data:image/${extension.substring(1)};base64,${base64}`;
    } else if (
      extension == ".mp4" ||
      extension == ".mov" ||
      extension == ".avi" ||
      extension == ".3gp"
    ) {
      result = "assets/images/video-placeholder.png";
    } else if (
      extension == ".mp3" ||
      extension == ".wav" ||
      extension == ".ogg"
    ) {
      result = "assets/images/audio-placeholder.png";
    } else if (extension == ".pdf") {
      result = "assets/images/pdf-placeholder.png";
    } else if (extension == ".pptx" || extension == ".ppt")  {
      result = "assets/images/powerpoint-placeholder.png";
    }  else if(extension == ".txt" || extension ==".doc" || extension == ".docx") {
      result = "assets/images/docx-placeholder.png";
    }  else if(extension == ".xls" || extension ==".xlsx" || extension == ".csv") {
      result = "assets/images/excel-placeholder.png";
    }
    else {
      result = "assets/images/placeholder.png";
    }
    return result;
  }
}
