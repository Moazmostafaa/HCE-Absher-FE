import { Injectable } from "@angular/core";
import { NbToastrService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";

@Injectable({
  providedIn: "root",
})
export class ToastrService {
  constructor(
    private toastrService: NbToastrService,
    private translateService: TranslateService
  ) {}

  async Delete(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("DeletedSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Deleted").toPromise()
    this.toastrService.primary(body, title);
  }
  async Create(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("CreatedSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Created").toPromise()
    this.toastrService.success(body, title);
  }
  async Update(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("UpdatedSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Updated").toPromise()
    this.toastrService.success(body, title);
  }
  async UpdateFailed(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("UpdateFailed", { entity: name}).toPromise();
    const title = await this.translateService.get("Failed").toPromise()
    this.toastrService.danger(body, title);
  }
  async Accept(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("AcceptedSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Accepted").toPromise()
    this.toastrService.success(body, title);
  }
  async Reject(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("RejectedSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Rejected").toPromise()
    this.toastrService.primary(body, title);
  }
  async Cancel(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("CanceledSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Canceled").toPromise()
    this.toastrService.success(body, title);
  }
  async Postpone(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("PostponedSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Postponed").toPromise()
    this.toastrService.success(body, title);
  }
  async Deactivate(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("DeactivateSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Deactivate").toPromise()
    this.toastrService.warning(body, title);
  }
  async Activate(entity: string) {
    const name = await this.translateService.get(entity).toPromise();
    const body = await this.translateService.get("ActivateSuccessfully", { entity: name}).toPromise();
    const title = await this.translateService.get("Activate").toPromise()
    this.toastrService.primary(body, title);
  }
  danger(message: string, title?: string){
    this.toastrService.danger(message, title);
  }
  warning(message: string, title?: string){
    this.toastrService.warning(message, title);
  }
}
