import { Component, Input, OnInit } from '@angular/core';
import { Metal } from 'src/app/_models/metal';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() asset: Metal;

  constructor() { }

  ngOnInit(): void {
  }

}
