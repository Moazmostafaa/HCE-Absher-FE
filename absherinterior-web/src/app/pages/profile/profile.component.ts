import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { NbAuthService } from "@nebular/auth";
import { TranslateService } from "@ngx-translate/core";
import { UserProfileModel } from "../../models/User/user-profile.model";
import { UserService } from "../../services/user.service";
import { Base64ImagePipe } from "../../shared/pipes/base64-image.pipe";

@Component({
  selector: "ngx-profile",
  templateUrl: "./profile.component.html",
  styleUrls: ["./profile.component.scss"],
  providers: [Base64ImagePipe],
})
export class ProfileComponent implements OnInit {
  constructor(
    private userService: UserService,
    private nbAuthService: NbAuthService,
    private base64ImagePipe: Base64ImagePipe,
    private translate: TranslateService,
    private router: Router
  ) {}
  profileImage = "";
  identificationImage = "";
  user: UserProfileModel;
  gender = "";
  ngOnInit() {
    this.user = {
      userId: "",
      fullName: "",
      userName: "",
      gender: 1,
      isActive: true,
      nationalId: "",
      phoneNumber: "",
      profileAttachmentId: "",
      identificationAttachmentId: "",
      profileAttachment: null,
      identificationAttachment: null,
    };
    this.userService.getUserProfile().subscribe((res) => {
      this.user = res.entity;
      this.profileImage = this.base64ImagePipe.transform(
        res.entity.profileAttachment?.fileData,
        res.entity.profileAttachment?.extention
      );
      this.identificationImage = this.base64ImagePipe.transform(
        res.entity.identificationAttachment?.fileData,
        res.entity.identificationAttachment?.extention
      );
      this.getGender(res.entity.gender);
    });
  }
  getGender(gender: number) {
    if (gender == 1) {
      this.translate.get("Male").subscribe((res) => {
        this.gender = res;
      });
    } else if (gender == 2) {
      return this.translate.get("Female").subscribe((res) => {
        this.gender = res;
      });
    }
    return "";
  }
  edit(){
    this.router.navigate([`/pages/user/edit/${this.user.userId}`]);
  }
}
