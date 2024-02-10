import { DocumentModel } from './../../../models/document/document.model';
import { Component, Input, OnInit } from '@angular/core';
import { KnowledgeCenterService } from "./../../../services/KnowledgeCenter.service";

@Component({
  selector: 'ngx-shared-knowledge-center-doc',
  templateUrl: './shared-knowledge-center-doc.component.html',
  styleUrls: ['./shared-knowledge-center-doc.component.scss']
})
export class SharedKnowledgeCenterDocComponent implements OnInit {
  @Input() shareKnowledgeCenterId: string;
  document:DocumentModel
  constructor(
    private knowledgeCenterService: KnowledgeCenterService,

  ) { }

  ngOnInit() {
    if (this.shareKnowledgeCenterId) {
      console.log(this.shareKnowledgeCenterId);

      this.getKnowledgeDocument(this.shareKnowledgeCenterId);
    }
  }
  getKnowledgeDocument(documentId) {
    this.knowledgeCenterService.getDocument(documentId).subscribe((res) => {
      console.log(res.entity);
      this.document = res.entity
    });
  }
}
