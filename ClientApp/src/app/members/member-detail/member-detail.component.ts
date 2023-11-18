import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { TimeagoModule } from 'ngx-timeago';
import { Member } from 'src/app/_models/member';
import { Message } from 'src/app/_models/message';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs?: TabsetComponent;
  activeTab?: TabDirective;
  member: Member = {} as Member;
  messages: Message[] = [];
  //images: GalleryItem[]

  constructor(private memberService: MembersService, 
              private route: ActivatedRoute,
              private messagesService: MessageService) { }

  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => {this.member = data['member']}
    });

    this.route.queryParams.subscribe({
      next: response => {
        response['tab'] && this.selectTab(response['tab'])
      }
    })
  }

  loadMember() {
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe({
      next: member => this.member = member,
      error: error => console.log(error)
    })
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if(this.activeTab.heading === 'Messages'){
      this.loadMessages();
    }
  }

  loadMessages() {
    if(this.member) {
      this.messagesService.getMessageThread(this.member.username).subscribe({
        next: response => this.messages = response
      })
    }
  }

  selectTab(heading: string) {
    if(this.memberTabs) {
      this.memberTabs.tabs.find(m => m.heading === heading)!.active = true;
    }
  }
}