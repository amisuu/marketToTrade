import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from 'src/app/_models/member';
import { map, of, take } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { User } from '../_models/user';
import { getPaginatedResult, getPaginationHeaders } from './pagination';
import { Message } from '../_models/message';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  userParams: UserParams;
  memberCache = new Map();
  user: User;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams();
    })
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    this.userParams = new UserParams();
    return this.userParams;
  }

  getMember(username: string) {
    const member = [...this.memberCache.values()]
      .reduce((arr, element) => arr.concat(element.result), [])
      .find((member: Member) => member.username === username);
    
    if (member) {
      return of(member);
    }
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

  getMembers(userParams: UserParams) {
    var response = this.memberCache.get(Object.values(userParams).join('-'));
    if (response) {
      return of(response);
    }

    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    if (userParams.search !== undefined) {
      params = params.append('search', userParams.search);
    }
    params = params.append('orderBy', userParams.orderBy);

    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http)
        .pipe(map(response => {
          this.memberCache.set(Object.values(userParams).join('-'), response);
          return response;
        }));
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member);
  }

  deletePhoto(photoId: number){
    return this.http.delete(this.baseUrl + "users/delete-photo/" + photoId);
  }
}
