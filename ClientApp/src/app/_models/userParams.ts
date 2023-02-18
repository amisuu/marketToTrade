import { User } from 'src/app/_models/user';

export class UserParams {
  pageNumber = 1;
  pageSize = 5;
  search?: string;
  orderBy = 'lastActive';
}
