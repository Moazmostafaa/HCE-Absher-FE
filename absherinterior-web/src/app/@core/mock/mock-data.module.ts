import { NgModule, ModuleWithProviders } from "@angular/core";
import { CommonModule } from "@angular/common";

import { NGXUserService } from "./users.service";
import { UserActivityService } from "./user-activity.service";
import { PeriodsService } from "./periods.service";

const SERVICES = [NGXUserService, UserActivityService, PeriodsService];

@NgModule({
  imports: [CommonModule],
  providers: [...SERVICES],
})
export class MockDataModule {
  static forRoot(): ModuleWithProviders<MockDataModule> {
    return {
      ngModule: MockDataModule,
      providers: [...SERVICES],
    };
  }
}
