import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Member } from '../_models/member';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  currentUser: User;
  member: Member;

  constructor(public accountService: AccountService, 
              private router: Router, 
              private memberService: MembersService) {}

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(user => {
      this.memberService.getMember(user.username).subscribe(member => {
        this.member = member;
      });
    })
  }

  login() {
    this.accountService.login(this.model).subscribe(response => {
      this.router.navigateByUrl('/assets');
    })
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
