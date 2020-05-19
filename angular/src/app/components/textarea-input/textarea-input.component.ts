import {
  Component,
  OnInit,
  ViewChild,
  Input,
  ElementRef,
  Self,
} from '@angular/core';
import { NgControl, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'app-textarea-input',
  templateUrl: './textarea-input.component.html',
  styleUrls: ['./textarea-input.component.css'],
})
export class TextareaInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('textarea', { static: true }) input: ElementRef;
  @Input() rows: number = 5;
  @Input() label: string;
  @Input() placeholder: string;

  constructor(@Self() public controlDir: NgControl) {
    this.controlDir.valueAccessor = this;
  }

  ngOnInit(): void {
    if (!this.placeholder) {
      this.placeholder = this.label;
    }

    const control = this.controlDir.control;
    const validators = control.validator ? [control.validator] : [];

    control.setValidators(validators);
    control.updateValueAndValidity();
  }

  onChange(event) {}

  onTouched() {}

  writeValue(obj: any): void {
    this.input.nativeElement.value = obj || '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
    console.log(JSON.stringify(fn));
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}
