import { Component, OnInit } from '@angular/core';
import { Metal } from '../_models/metal';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { AssetsService } from '../_services/assets.service';
import { take } from 'rxjs';
import { Pagination } from '../_models/pagination';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  assets: Partial<Metal[]>;
  predicate = 'liked';
  user: User;
  userResponse: Partial<User[]>;
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination;

  constructor(private assetsService: AssetsService,
              private accountService: AccountService) {
                this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
              }

  ngOnInit(): void {
    this.loadLikes();
    this.loadUsersWithLikes();
  }

  loadLikes() {
    console.log(this.user.id);
    this.assetsService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe(response => {
      this.assets = response.result;
      this.pagination = response.pagination;
    });
  }

  loadUsersWithLikes() {
    console.log(this.user.id);
    this.assetsService.getUsersWithLikes(this.predicate, this.pageNumber, this.pageSize).subscribe(response => {
      this.userResponse = response.result;
      this.pagination = response.pagination;
    });
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadLikes();
    this.loadUsersWithLikes();
  }
}
