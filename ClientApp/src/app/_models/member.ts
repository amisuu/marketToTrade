import { Photo } from "./photo";
import { Metal } from "./metal";

export interface Member {
  username:   string;
  knownAs:    string;
  created:    Date;
  lastActive: Date;
  phone:      number;
  email:      string;
  photoUrl:   string;
  photos:     Photo[];
  assets:     Metal[];
}
