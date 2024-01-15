import { ChangeDetectionStrategy, Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush //just for issue NG0100 expression change after it was checked
})
export class MemberMessagesComponent implements OnInit{
  @ViewChild('messageForm') messageForm?: NgForm;
  @Input() userName?: string;
  messageContent = '';
  
  constructor(public messagesService: MessageService) { }

  ngOnInit(): void {
  }

  sendMessage() {
    if(!this.userName){
      return;
    }

    this.messagesService.sendMessage(this.userName, this.messageContent).then(() => {
      this.messageForm?.reset();
    })
  }
}
