const messages = {
  CreatedTitle: "Created",
  UpdatedTitle: "Updated",
  DeletedTitle: "Deleted",
  CreatedMessage: "New {0} has been created successfully.",
  UpdatedMessage: "{0} has been updated successfully.",
  DeletedMessage: "{0} has been deleted successfully.",
};

export function GetToastTitleAndMessage(
  ActionType: ActionTypeEnum,
  EntityName: string
): ToasterModel {
  switch (ActionType) {
    case ActionTypeEnum.Created:
      return {
        title: messages.CreatedTitle,
        message: messages.CreatedMessage.replace("{0}", EntityName),
      };
    case ActionTypeEnum.Updated:
      return {
        title: messages.UpdatedTitle,
        message: messages.UpdatedMessage.replace("{0}", EntityName),
      };
    case ActionTypeEnum.Deleted:
      return {
        title: messages.DeletedTitle,
        message: messages.DeletedMessage.replace("{0}", EntityName),
      };
  }
}

export enum ActionTypeEnum {
  Created = 1,
  Updated = 2,
  Deleted = 3,
}
export interface ToasterModel {
  title: string;
  message: string;
}
