import { User } from 'src/app/_models/user';

export class UserParams {
  pageNumber = 1;
  pageSize = 12;
  search?: string;
  orderBy = 'lastActive';
}
