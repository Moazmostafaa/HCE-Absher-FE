import { AttachmentModel } from "../attachment/attachment.model";

export interface UserProfileModel {
  userId: string;
  userName: string;
  fullName: string;
  phoneNumber: string;
  gender: number;
  profileAttachmentId: string;
  identificationAttachmentId: string;
  nationalId: string;
  isActive: boolean;
  department?: string;
  grand?: string;
  education?: string;
  joinDate?: string;
  dateOfRegistration?: string;
  dateofRetirement?: string;
  moiCardExpiryDate?: string;
  noOfCommittees?: string;
  noOfTrainings?: string;
  noOfParticipation?: string;
  nicCardExpiryDate?: string;
  drivingLicenseExpiryDate?: string;
  profileAttachment: AttachmentModel;
  identificationAttachment: AttachmentModel;
}
