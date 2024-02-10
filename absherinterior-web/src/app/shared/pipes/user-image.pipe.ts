import { Base64ImagePipe } from "./base64-image.pipe";
import { LookupsService } from "./../../services/lookups.service";
import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "userImage",
  pure: false

})
export class UserImagePipe implements PipeTransform {
  private userimageResult: any = null;
  private cachedId = '';

  constructor(
    private lookupSerivce: LookupsService,
    private base64ImagePipe: Base64ImagePipe
  ) {}
  transform(profileAttachmentId: string): string {

    if (profileAttachmentId && profileAttachmentId !== this.cachedId) {
      this.userimageResult = null;
      this.cachedId = profileAttachmentId;
       this.lookupSerivce
         .getAttachmentById(profileAttachmentId)
         .subscribe((res: any) => {
           this.userimageResult =  this.base64ImagePipe.transform(
             res.entity.fileData,
             res.entity.extention
           );
         })

    }

    return this.userimageResult;
  }
}
