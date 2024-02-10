import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "base64Document",
})
export class Base64DocumentPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) {}

  transform(base64: string, extension: string): SafeHtml {
    if (!base64) return "assets/images/placeholder.png";
    let result:string = "";
    if (extension == ".pptx") {
      result = `data:application/vnd.openxmlformats-officedocument.presentationml.presentation/${extension.substring(1)};base64,${base64}`;

    }  else  if (extension == ".ppt") {
       result = `data:application/vnd.ms-powerpoint/${extension.substring(1)};base64,${base64}`;
    }  else  if (extension == ".xls") {
      result = `data:application/vnd.ms-excel/${extension.substring(1)};base64,${base64}`;

    }  else  if (extension == ".xlsx") {
      result = `data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet/${extension.substring(1)};base64,${base64}`;

    } else  if (extension == ".doc") {
      result = `data:application/msword/${extension.substring(1)};base64,${base64}`;

    } else  if (extension == ".docx") {
      result = `data:application/vnd.openxmlformats-officedocument.wordprocessingml.document/${extension.substring(1)};base64,${base64}`;

    }else  if (extension == ".pdf") {
      result = `data:application/pdf/${extension.substring(1)};base64,${base64}`;

    }else  if (extension == ".csv") {
      result = `data:text/csv/${extension.substring(1)};base64,${base64}`;

    }else  if (extension == ".txt") {
      result = `data:text/plain/${extension.substring(1)};base64,${base64}`;

    }
    return result;
  }
}
