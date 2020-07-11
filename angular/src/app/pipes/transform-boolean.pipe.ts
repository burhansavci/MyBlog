import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'transformBoolean',
})
export class TransformBooleanPipe implements PipeTransform {
  transform(value: any) {
    return value ? 'True' : 'False';
  }
}
