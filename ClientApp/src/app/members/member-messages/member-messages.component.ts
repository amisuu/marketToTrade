import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit{
  @ViewChild('messageForm') messageForm?: NgForm;
  @Input() userName?: string;
  @Input() messages: Message[] = [];
  messageContent = '';

  constructor(private messagesService: MessageService) { }

  ngOnInit(): void {
  }

  sendMessage() {
    if(!this.userName){
      return;
    }

    this.messagesService.sendMessage(this.userName, this.messageContent).subscribe({
      next: response => {
        this.messages.push(response)
        this.messageForm?.reset();
      }
    });
  }
}