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
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css'],
})
export class TextInputComponent implements OnInit, ControlValueAccessor {
  @ViewChild('input', { static: true }) input: ElementRef;
  @Input() type = 'text';
  @Input() label: string;
  @Input() placeholder: string;
  @Input() class: string;
  @Input() labelClass: string;

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
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}
