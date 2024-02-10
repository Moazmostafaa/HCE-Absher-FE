import { Base64DocumentPipe } from "./../../../shared/pipes/base64-document.pipe";
import { Base64VideoPipe } from "./../../../shared/pipes/base64-video.pipe";
import { Base64ImagePipe } from "./../../../shared/pipes/base64-image.pipe";
import { LookupsService } from "./../../../services/lookups.service";
import {
  Component,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
} from "@angular/core";
import { NbWindowService } from "@nebular/theme";

@Component({
  selector: "ngx-post-files",
  templateUrl: "./post-files.component.html",
  styleUrls: ["./post-files.component.scss"],
})
export class PostFilesComponent implements OnInit {
  files: any[] = [];
  @Input() postFiles: [];
  @ViewChild("contentTemplate") contentTemplate: TemplateRef<any>;

  constructor(
    private lookupSerivce: LookupsService,
    private base64ImagePipe: Base64ImagePipe,
    private base64VideoPipe: Base64VideoPipe,
    private base64DocumentPipe: Base64DocumentPipe,
    private windowService: NbWindowService,
  ) {}

  ngOnInit() {
    this.getPostAttachments(this.postFiles);

  }
  getPostAttachments(postFiles: any[]) {
    postFiles.map((file, i) => {
      return this.lookupSerivce
        .getAttachmentById(file.attachmentId)
        .subscribe((res: any) => {
          if (file.fileType == 1) {
            this.files.push({
              fileLink: this.base64ImagePipe.transform(
                res.entity.fileData,
                res.entity.extention
              ),
              extention: res.entity.extention,
              fileName: res.entity.fileName,
              fileType: file.fileType,
            });
          } else if (file.fileType == 2) {
            this.files.push({
              fileLink: this.base64VideoPipe.transform(
                res.entity.fileData,
                res.entity.extention
              ),
              extention: res.entity.extention,
              fileName: res.entity.fileName,
              fileType: file.fileType,
            });
          } else if (file.fileType == 4) {
            this.files.push({
              fileLink: this.base64DocumentPipe.transform(
                res.entity.fileData,
                res.entity.extention
              ),

              extention: res.entity.extention,
              fileName: res.entity.fileName,
              fileType: file.fileType,
            });
          }
        });
    });
  }

  openImageInWindow(name, source) {
    this.windowService.open(this.contentTemplate, {
      title: name,
      context: { src: source },
      hasBackdrop: true,
    });
  }
}
