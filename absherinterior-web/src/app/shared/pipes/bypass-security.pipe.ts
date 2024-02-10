import { Pipe, PipeTransform, SecurityContext } from "@angular/core";
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Pipe({
    name: 'byPassSecurity'
})
export class ByPassSecurityPipe implements PipeTransform {

    constructor(private sanitizer: DomSanitizer) {}

    transform (value: string,type:string): SafeHtml {
      let safe:any
      if (type == "url"){
         safe =  this.sanitizer.bypassSecurityTrustUrl(value);}
        else {
           safe =  this.sanitizer.bypassSecurityTrustScript(value);}
        return safe;
    }
}
