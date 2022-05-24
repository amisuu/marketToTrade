import { Component, Input, OnInit } from '@angular/core';
import { Metal } from 'src/app/_models/metal';

@Component({
  selector: 'app-assets-card',
  templateUrl: './assets-card.component.html',
  styleUrls: ['./assets-card.component.css']
})
export class AssetsCardComponent implements OnInit {
  @Input() asset: Metal;

  constructor() { }

  ngOnInit(): void {
    console.log(this.asset);
  }

}
