import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { BlockLawService } from "../../../services/block-law.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { WhiteSpaceValidator } from "../../../shared/validators/white-space.validator";

@Component({
  selector: "ngx-add-block-law",
  templateUrl: "./add-block-law.component.html",
  styleUrls: ["./add-block-law.component.scss"],
})
export class AddBlockLawComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastrService: ToastrService,
    private translate: TranslateService,
    private blockLawService: BlockLawService
  ) {}
  lawForm: FormGroup;
  submitted = false;
  isSubmitted=false;
  ngOnInit(){
    this.initForm();
  }
  initForm() {
    this.lawForm = this.formBuilder.group({
      lawNumber: ["",[ Validators.required, WhiteSpaceValidator.noWhiteSpace]],
      messageAr: ["", [Validators.required,WhiteSpaceValidator.noWhiteSpace]],
      messageEng: ["",[ Validators.required,WhiteSpaceValidator.noWhiteSpace]],
    });
  }
  onSubmit() {
    this.submitted = true;
    this.isSubmitted = true;
    if (this.lawForm.invalid) {
      return;
    }
    const model = this.lawForm.value;
    this.blockLawService.addBlockLaw(model).subscribe((res) => {
      if (res.status == 200) {
        this.toastrService.Create(EntityNames.BlockLaw);
        this.router.navigate(["/pages/block-law/"]);
      }
    });
  }
  get fc() {
    return this.lawForm.controls;
  }
}
