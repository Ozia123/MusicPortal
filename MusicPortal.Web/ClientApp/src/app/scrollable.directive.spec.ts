import { ScrollableDirective } from './scrollable.directive';
import { ElementRef } from '@angular/core';

describe('ScrollableDirective', () => {
  it('should create an instance', () => {
    const directive = new ScrollableDirective(new ElementRef(this));
    expect(directive).toBeTruthy();
  });
});
