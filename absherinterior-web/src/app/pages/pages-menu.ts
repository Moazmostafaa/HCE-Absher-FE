import { NbMenuItem } from "@nebular/theme";
import { title } from "process";

var currentLang = localStorage.getItem("language");
export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: currentLang == "en" ? "Dashboard" : "لوحة التحكم",
    icon: "home-outline",
    link: "/pages/dashboard",
    home: true,
  },
  {
    title: currentLang == "en" ? "User management" : "إدارة المستخدم",
    icon: "people-outline",
    children: [
      {
        title: currentLang == "en" ? "Users" : "المستخدمين",
        icon: "people-outline",
        link: "/pages/user",
      },
      {
        title: currentLang == "en" ? "Block laws" : "قوانين الحظر",
        icon: "lock-outline",
        link: "/pages/block-law",
      },
    ],
  },
  {
    title: currentLang == "en" ? "Event management" : "إدارة الفعاليات",
    icon: "layers-outline",
    link: "/pages/event",
  },

  {
    title: currentLang == "en" ? "Post management" : "إدارة المنشورات",
    icon: "edit-2-outline",
    link: "/pages/post",
  },
  {
    title: currentLang == "en" ? "Chatting management" : "إدارة المحادثات",
    icon: "message-square-outline",
    link: "/pages/chat/usersChats",
  },
  {
    title: currentLang == "en" ? "Knowledge center" : "مركز المعرفة",
    icon: "bulb-outline",
    children: [
      {
        title: currentLang == "en" ? "Document" : "الملفات",
        icon: "file-add-outline",
        link: "/pages/document",
      },
    ],
  },
  {
    title: currentLang == "en" ? "Category" : "الفئات",
    icon: "folder-add-outline",
    link: "/pages/category",
  },
  {
    title: currentLang == "en" ? "Notification" : "الإشعارات",
    icon: "bell-outline",
    link: "/pages/notification",
  },
];
