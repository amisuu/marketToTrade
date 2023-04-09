import { Metal } from 'src/app/_models/metal';

export class AssetParams {
  pageNumber = 1;
  pageSize = 5;
  search?: string;
  orderBy = 'lastAdded';
}