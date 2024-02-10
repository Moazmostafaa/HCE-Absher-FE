import { Component, HostListener, OnInit, TemplateRef } from "@angular/core";
import { ChatService } from "../../../../services/chats.service";
import { NbToastrService, NbDialogService } from "@nebular/theme";
import { UserService } from "../../../../services/user.service";
import { ConfirmDialogComponent } from "../../../../shared/components/showcase-dialog/confirm-dialog.component";

@Component({
  selector: "ngx-groupschats",
  templateUrl: "./groupschats.component.html",
  styleUrls: ["./groupschats.component.scss"],
})
@HostListener("scroll", ["$event"])
export class GroupschatsComponent implements OnInit {
  showMessageEvent = null;
  rooms = [];
  messages = [];
  members = [];
  message = "";
  blockPeriod = 0;
  userBlockLawId;
  laws = [];
  defaultImg = "./assets/images/group.png";
  defaultImg2 = "./assets/images/placeholder.png";
  totalRecords = 0;
  currentRoom: number = null;
  selectedRoom: any;
  showAttachArea = false;
  showMembers = false;
  searchModal = {
    pageNumber: 1,
    pageSize: 10,
  };
  userId = "";
  loading = false;
  constructor(
    private chatService: ChatService,
    private dialogService: NbDialogService,
    private toastr: NbToastrService,
    private userService: UserService
  ) {
    this.getAllChats();
    this.getUserData();
    this.getBlockLaws();
  }

  ngOnInit(): void {}
  getUserData() {
    this.userService.getUserState().subscribe((res) => {
      console.log("res : ", res);
      this.userId = res.userId;
    });
  }
  getBlockLaws() {
    this.userService.getBlockLaws().subscribe((res: any) => {
      this.laws = res.entity.entities;
    });
  }
  getAllChats() {
    this.loading = true;
    this.chatService.getGroupsChats(this.searchModal).subscribe((res: any) => {
      this.rooms.push.apply(this.rooms, res.entity.entities);
      this.totalRecords = res.entity.totalRecords;
      this.loading = false;
    });
  }
  getRoomMembers(roomId) {
    this.chatService
      .getRoomMembers({ chatGroupId: roomId, pageNumber: 1, pageSize: 100 })
      .subscribe((res: any) => {
        this.members = res.entity.entities;
      });
  }
  getRoomMessages(roomId) {
    this.chatService
      .getRoomMessages({ chatGroupId: roomId, pageNumber: 1, pageSize: 100 })
      .subscribe((res: any) => {
        this.messages = res.entity.entities;
      });
  }
  onScroll(event: any) {
    if (
      event.target.offsetHeight + event.target.scrollTop >=
      event.target.scrollHeight
    ) {
      if (this.rooms.length < this.totalRecords && !this.loading) {
        this.searchModal.pageNumber = this.searchModal.pageNumber + 1;
        this.getAllChats();
      }
    }
  }
  getroomMessages(room) {
    this.selectedRoom = room;
    this.currentRoom = room.chatGroupId;
    this.getRoomMembers(room.chatGroupId);
    this.getRoomMessages(room.chatGroupId);
    this.showAttachArea = false;
    this.showMembers = false;
  }
  viewDialog(dialog: TemplateRef<any>) {
    this.dialogService.open(dialog);
  }
  deacivate(ref) {
    this.userService
      .userDeActivation({
        userId: this.userId,
        userBlockLawId: this.userBlockLawId,
        blockPeriod: this.blockPeriod,
      })
      .subscribe((res) => {
        ref.close();
        this.toastr.success("User banned successfuly");
      });
  }
  delete(roomId: string, roomName: string) {
    this.dialogService
      .open(ConfirmDialogComponent, {
        context: {
          title: `Delete ${roomName}`,
          body: `Are you sure you want to delete ${roomName}?`,
        },
      })
      .onClose.subscribe((res) => {
        if (res) {
          this.chatService.deleteChat(roomId).subscribe((result) => {
            this.rooms = this.rooms.filter((x) => x.chatGroupId != roomId);
            this.toastr.info("Chat deleted successfuly", "Delete");
          });
        }
      });
  }
  showAction(e, actionContainer, index) {
    e.preventDefault();
    console.log("actionContainer : ", actionContainer);

    this.showMessageEvent == actionContainer + index
      ? (this.showMessageEvent = null)
      : (this.showMessageEvent = actionContainer + index);
  }
  makeAction(actionContainer, index, action) {
    this.showMessageEvent = null;
    if (actionContainer == "r" && action == "delete") {
      this.delete(index, this.selectedRoom.chatGroupName);
    } else if (actionContainer == "m" && action == "delete") {
      this.deleteMessage(index);
    }
  }
  deleteMessage(messageId) {
    this.messages = this.messages.filter((x) => x.chatMessageId != messageId);
  }
  sendMessage() {
    if (this.message.length > 0) {
      console.log("will send " + this.message);
      this.message = "";
    }
  }
}
