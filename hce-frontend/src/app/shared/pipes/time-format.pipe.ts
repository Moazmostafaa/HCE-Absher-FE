import { Injectable, Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({ name: 'timeFormatter' })
@Injectable({
  providedIn: 'root'
})  
export class TimeFormatterPipe implements PipeTransform {
  transform(time: string): string {
    time = time?.substring(0,8);
    return time;
  }
}