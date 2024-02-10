import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { BlockLawModel } from "../../../models/block-law/block-law.model";
import { BlockLawService } from "../../../services/block-law.service";
import { ToastrService } from "../../../services/toastr.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { WhiteSpaceValidator } from "../../../shared/validators/white-space.validator";

@Component({
  selector: "ngx-edit-block-law",
  templateUrl: "./edit-block-law.component.html",
  styleUrls: ["./edit-block-law.component.scss"],
})
export class EditBlockLawComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastrService: ToastrService,
    private translate: TranslateService,
    private blockLawService: BlockLawService,
    private route: ActivatedRoute
  ) {}
  lawForm: FormGroup;
  submitted = false;
  isSubmitted= false;
  userBlockLawId: string;
  ngOnInit() {
    this.route.params.subscribe((params) => {
      this.userBlockLawId = params["id"];
      this.blockLawService
        .getBlockLawById(this.userBlockLawId)
        .subscribe((res) => {
          this.lawForm.patchValue(res.entity);
        });
    });
    this.initForm();
  }
  initForm() {
    this.lawForm = this.formBuilder.group({
      userBlockLawId: [""],
      lawNumber:["",[ Validators.required, WhiteSpaceValidator.noWhiteSpace]],
      messageAr:["",[ Validators.required, WhiteSpaceValidator.noWhiteSpace]],
      messageEng:["",[ Validators.required, WhiteSpaceValidator.noWhiteSpace]],
    });
  }
  onSubmit() {
    this.submitted = true;
    this.isSubmitted = true;
    if (this.lawForm.invalid) {
      return;
    }
    const model = this.lawForm.value;
    this.blockLawService.updateBlockLaw(model).subscribe((res) => {
      if (res.status == 200) {
        this.toastrService.Update(EntityNames.BlockLaw);
        this.router.navigate(["/pages/block-law/"]);
      }
    });
  }
  get fc() {
    return this.lawForm.controls;
  }
}
