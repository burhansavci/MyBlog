import { TransformBooleanPipe } from './transform-boolean.pipe';

describe('TransformBooleanPipe', () => {
  it('create an instance', () => {
    const pipe = new TransformBooleanPipe();
    expect(pipe).toBeTruthy();
  });
});
