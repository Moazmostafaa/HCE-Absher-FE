import { Component, OnInit } from "@angular/core";

@Component({
  selector: "ngx-mychats",
  templateUrl: "./mychats.component.html",
  styleUrls: ["./mychats.component.scss"],
})
export class MychatsComponent implements OnInit {
  showMessageEvent = null;
  rooms = [];
  messages = [];
  message = "";
  currentRoom: number = null;
  showAttachArea = false;
  constructor() {
    this.rooms.length = 5;
    this.messages.length = 2;
  }

  ngOnInit(): void {}
  getroomMessages(roomIndex) {
    this.currentRoom = roomIndex;
    this.showAttachArea = false;
  }
  showAction(e, actionContainer, index) {
    e.preventDefault();
    this.showMessageEvent == actionContainer + index
      ? (this.showMessageEvent = null)
      : (this.showMessageEvent = actionContainer + index);
  }
  makeAction(actionContainer, index, action) {
    this.showMessageEvent = null;
    console.log("will" + action + actionContainer + index);
  }
  sendMessage() {
    if (this.message.length > 0) {
      console.log("will send " + this.message);
      this.message = "";
    }
  }
}
