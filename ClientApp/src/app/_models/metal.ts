import { Photo } from "./photo";

export interface Metal {
  id: number;
  type:	string;
  form:	string;
  photoUrl: string;
  title: string;
  mass:	string;
  fineness: string;
  quantity: number;
  producer: string;
  price: string;
  year: number;
  condition: string;
  isOriginalPackage: string;
  isReceipt: string;
  publicationDate: Date;
  photos: Photo[];
  appUserId: number;
}
