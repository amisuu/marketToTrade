import { Component, Input, OnInit, Self } from '@angular/core';
import { NgControl, ControlValueAccessor } from '@angular/forms';
import { YesOrNo } from 'src/app/enum/yesOrNoEnum';

@Component({
  selector: 'app-yes-no-select',
  templateUrl: './yes-no-select.component.html',
  styleUrls: ['./yes-no-select.component.css']
})
export class YesNoSelectComponent implements ControlValueAccessor {
  @Input() label: string;
  @Input() type = 'text';
  yesNoList = [{value: 'Yes'}, {value: 'No'}];
  public enumTypes = Object.values(YesOrNo);

  constructor(@Self() public ngControl: NgControl) {
    this.ngControl.valueAccessor = this;
  }

  writeValue(obj: any): void {
  }

  registerOnChange(fn: any): void {
  }

  registerOnTouched(fn: any): void {
  }
}
