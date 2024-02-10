import { Component, OnInit } from "@angular/core";
import { AnalyticsService } from "../../../@core/utils/analytics.service";
import { StatisticsEnum } from "../../../models/statistics/stats.enum";

@Component({
  selector: "app-dashboard-statistics",
  templateUrl: "./dashboard-statistics.component.html",
  styleUrls: ["./dashboard-statistics.component.scss"],
})
export class DashboardStatisticsComponent implements OnInit {
  totalUsers: StatisticsEnum = StatisticsEnum.TotalUsers;
  totalActiveUsers: StatisticsEnum = StatisticsEnum.TotalActiveUsers;
  totalInactiveUsers: StatisticsEnum = StatisticsEnum.TotalInactiveUsers;
  totalPosts: StatisticsEnum = StatisticsEnum.TotalPosts;
  totalChats: StatisticsEnum = StatisticsEnum.TotalChats;
  totalEvents: StatisticsEnum = StatisticsEnum.TotalEvents;
  totalKnowledgeCenterDocuments: StatisticsEnum = StatisticsEnum.TotalKnowledgeCenterDocuments;
  attendedEvents: StatisticsEnum = StatisticsEnum.AttendedEvents;
  commentedPosts: StatisticsEnum = StatisticsEnum.CommentedPosts;
  likedPosts: StatisticsEnum = StatisticsEnum.LikedPosts;
  postOwners: StatisticsEnum = StatisticsEnum.PostOwners;
  sharedPosts: StatisticsEnum = StatisticsEnum.SharedPosts;
  likedKnowledgeCenterDocuments: StatisticsEnum = StatisticsEnum.LikedKnowledgeCenterDocuments;
  sharedKnowledgeCenterDocuments: StatisticsEnum = StatisticsEnum.SharedKnowledgeCenterDocuments;
  
  FromDate = null;
  ToDate = null;
  constructor() {}
  ngOnInit() {
  }
}
