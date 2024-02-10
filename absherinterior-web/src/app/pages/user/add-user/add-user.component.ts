import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { NbToastrService } from "@nebular/theme";
import { AddUserModel } from "../../../models/User/register-user.model";
import { RoleLookUp } from "../../../models/lookups/roles-lookup.model";
import { UserService } from "../../../services/user.service";
import { LookupsService } from "../../../services/lookups.service";
import { MustMatch } from "../../../shared/validators/must-match.validator";
import { FileService } from "../../../services/file.service";
import { FileModuleEnum } from "../../../models/file-module";
import { TranslateService } from "@ngx-translate/core";
import { EntityNames } from "../../../shared/Entity-Names";
import { ToastrService } from "../../../services/toastr.service";
import { WhiteSpaceValidator } from "../../../shared/validators/white-space.validator";

@Component({
  selector: "ngx-add-user",
  templateUrl: "./add-user.component.html",
  styleUrls: ["./add-user.component.scss"],
})
export class AddUserComponent implements OnInit {
  constructor(
    private userService: UserService,
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private router: Router,
    private nbToaster: NbToastrService,
    private toaster: ToastrService,
    private lookupSerivce: LookupsService,
    private translate: TranslateService
  ) {}
  userForm: FormGroup;
  rolesList: RoleLookUp[] = [];
  showPass = false;
  showConfirmPass = false;
  isSubmitted=false;
  profileAttachment = "";
  profileExtension = "";
  identificationAttachment = "";
  identificationExtension = "";
  ngOnInit() {
    this.initForm();
    this.getRoles();
  }
  initForm() {
    this.userForm = this.formBuilder.group(
      {
        fullName: ["", [Validators.required, Validators.minLength(3),WhiteSpaceValidator.noWhiteSpace]],
        email: ["", [Validators.required, Validators.email,WhiteSpaceValidator.noWhiteSpace]],
        password: [
          "",
          [
            Validators.required,
            Validators.minLength(8),
            Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$"),
          ],
        ],
        confirmPassword: [
          "",
          [
            Validators.required,
            Validators.minLength(8),
            Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$"),
          ],
        ],
        phoneNumber: [
          "",
          [
            Validators.required,
            Validators.minLength(10),
            Validators.maxLength(14),
            WhiteSpaceValidator.noWhiteSpace,
          ],
        ],
        gender: ["1"],
        nationalId: ["", [Validators.required, Validators.pattern("[0-9]+%?")]],
        roleId: [[], [Validators.required]],
        profileImage: ["",Validators.required],
        identificationImage: ["",Validators.required],
      },
      {
        validator: MustMatch("password", "confirmPassword"),
      }
    );
  }
  onSubmit() {
    this.isSubmitted=true;
    if (this.userForm.invalid) {
      return;
    }
    let dto = this.userForm.value;
    let model = {
      fullName: dto.fullName,
      email: dto.email,
      phoneNumber: dto.phoneNumber,
      gender: dto.gender,
      nationalId: dto.nationalId,
      roles: this.userForm.value.roleId.map((role) => role),
      profileAttachmentId: dto.profileImage ? dto.profileImage : null,
      password: dto.password,
      identificationAttachmentId: dto.identificationImage
        ? dto.identificationImage
        : null,
    };
    this.userService.addUser(model).subscribe((userResponse) => {
      this.toaster.Create(EntityNames.User);
      this.router.navigate(["pages/user/"]);
    });
  }
  getRoles() {
    this.lookupSerivce.getAllRoles().subscribe((res) => {
      this.rolesList = res.entity.entities;
    });
  }
  TogglePassword() {
    var passwordInput = <HTMLInputElement>document.getElementById("password");
    if (passwordInput.type === "password") {
      passwordInput.type = "text";
      this.showPass = true;
    } else {
      passwordInput.type = "password";
      this.showPass = false;
    }
  }
  ToggleConfirmPassword() {
    var passwordInput = <HTMLInputElement>(
      document.getElementById("confirmPassword")
    );
    if (passwordInput.type === "password") {
      passwordInput.type = "text";
      this.showConfirmPass = true;
    } else {
      passwordInput.type = "password";
      this.showConfirmPass = false;
    }
  }
  onSelectProfileImage(event) {
    if (event.target.files) {
      let isValid = true;
      const [file] = event.target.files;
      var readerProfile = new FileReader();
      readerProfile.onload = (event: any) => {
        if (isValid) {
          this.profileAttachment = event.target.result;
          this.profileExtension = "." + file.name.split(".").pop();
        } else {
          this.profileAttachment = "";
          this.profileExtension = "";
        }
      };
      readerProfile.readAsDataURL(event.target.files[0]);
      this.fileService
        .uploadImage(file, (+FileModuleEnum.User).toString())
        .subscribe(
          (res) => {
            this.userForm.controls["profileImage"].setValue(res.entity);
          },
          () => {
            this.translate.get("errorUploadImageType").subscribe((message) => {
              this.translate.get("error").subscribe((title) => {
                this.nbToaster.danger(message, title);
                isValid = false;
              });
            });
          }
        );
    }
  }
  onSelectIdImage(event) {
    if (event.target.files) {
      let isValid = true;
      const [file] = event.target.files;
      var readerProfile = new FileReader();
      readerProfile.onload = (event: any) => {
        if (isValid) {
          this.identificationAttachment = event.target.result;
          this.identificationExtension = "." + file.name.split(".").pop();
        } else {
          this.identificationAttachment = "";
          this.identificationExtension = "";
        }
      };
      readerProfile.readAsDataURL(event.target.files[0]);
      this.fileService
        .uploadImage(file, (+FileModuleEnum.User).toString())
        .subscribe(
          (res) => {
            this.userForm.controls["identificationImage"].setValue(res.entity);
          },
          () => {
            this.translate.get("errorUploadImageType").subscribe((message) => {
              this.translate.get("error").subscribe((title) => {
                this.nbToaster.danger(message, title);
                isValid = false;
              });
            });
          }
        );
    }
  }
  get fc() {
    return this.userForm.controls;
  }
}
