import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { NbAuthService } from "@nebular/auth";
import { NbToastrService } from "@nebular/theme";
import { TranslateService } from "@ngx-translate/core";
import { FileModuleEnum } from "../../../models/file-module";
import { RoleLookUp } from "../../../models/lookups/roles-lookup.model";
import { FileService } from "../../../services/file.service";
import { LookupsService } from "../../../services/lookups.service";
import { ToastrService } from "../../../services/toastr.service";
import { UserService } from "../../../services/user.service";
import { EntityNames } from "../../../shared/Entity-Names";
import { WhiteSpaceValidator } from "../../../shared/validators/white-space.validator";

@Component({
  selector: "ngx-edit-user",
  templateUrl: "./edit-user.component.html",
  styleUrls: ["./edit-user.component.scss"],
})
export class EditUserComponent implements OnInit {
  constructor(
    private userService: UserService,
    private fileService: FileService,
    private formBuilder: FormBuilder,
    private router: Router,
    private nbToaster: NbToastrService,
    private toaster: ToastrService,
    private lookupSerivce: LookupsService,
    private route: ActivatedRoute,
    private translate: TranslateService,
    private authService: NbAuthService,
  ) {}
  userForm: FormGroup;
  rolesList: RoleLookUp[] = [];
  userId: string = null;
  user: any;
  profileAttachment = "";
  profileExtension = "";
  identificationAttachment = "";
  identificationExtension = "";
  isSubmitted=false;
  isCurrentUser: boolean = false;
  ngOnInit() {
    this.userId = this.route.snapshot.params.id;
    if (!this.userId) {
      this.router.navigate(["/pages/user/"]);
      this.nbToaster.danger("Invalid User", "Error");
    } else {
      this.initForm();
      this.getRoles();
      this.userService.getUserById(this.userId).subscribe((res) => {
        this.user = res.entity;
        this.BindForm();
      });
      let currentUser = JSON.parse(localStorage.getItem("userData"));
      if(currentUser.userId==this.userId){
        this.isCurrentUser=true;
      }
    }
  }
  initForm() {
    this.userForm = this.formBuilder.group({
      fullName: ["", [Validators.required, Validators.minLength(3),WhiteSpaceValidator.noWhiteSpace]],
      email: ["", [Validators.required, Validators.email,WhiteSpaceValidator.noWhiteSpace]],
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
      profileImage: [""],
      identificationImage: [""],
    });
  }
  BindForm() {
    this.userForm.setValue({
      fullName: this.user.fullName,
      email: this.user.email,
      phoneNumber: this.user.phoneNumber,
      gender: this.user.gender,
      nationalId: this.user.nationalId,
      roleId: this.user.roles ? this.user.roles.map((role) => role.roleId) : [],
      profileImage: this.user.profileAttachmentId
        ? this.user.profileAttachmentId
        : "",
      identificationImage: this.user.identificationAttachmentId
        ? this.user.identificationAttachmentId
        : "",
    });
    if (this.user.profileAttachmentId) {
      this.lookupSerivce
        .getAttachmentById(this.user.profileAttachmentId)
        .subscribe((res: any) => {
          this.profileAttachment = res.entity.fileData;
          this.profileExtension = res.entity.extention;
        });
    }
    if (this.user.identificationAttachmentId) {
      this.lookupSerivce
        .getAttachmentById(this.user.identificationAttachmentId)
        .subscribe((res: any) => {
          this.identificationAttachment = res.entity.fileData;
          this.identificationExtension = res.entity.extention;
        });
    }
  }
  onSubmit() {
    this.isSubmitted=true;
    if (this.userForm.invalid) {
      return;
    }
    let dto = this.userForm.value;
    let model = {
      userId: this.userId,
      fullName: dto.fullName,
      email: dto.email,
      phoneNumber: dto.phoneNumber,
      gender: dto.gender,
      nationalId: dto.nationalId,
      roles: this.userForm.value.roleId.map((role) => role),
      profileAttachmentId: dto.profileImage ? dto.profileImage : null,
      identificationAttachmentId: dto.identificationImage
        ? dto.identificationImage
        : null,
    };
    this.userService.editUser(model).subscribe((userResponse) => {
      this.toaster.Update(EntityNames.User);
      this.router.navigate(["pages/user/"]);
    });
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
  getRoles() {
    this.lookupSerivce.getAllRoles().subscribe((res) => {
      this.rolesList = res.entity.entities;
    });
  }
  get fc() {
    return this.userForm.controls;
  }
}
