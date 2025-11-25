import {
    AfterViewInit,
    Component,
    ElementRef,
    OnDestroy,
    OnInit,
    ViewChild,
  } from '@angular/core';
  import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
  import flatpickr from 'flatpickr';
  import { Vietnamese } from 'flatpickr/dist/l10n/vn';
  
  @Component({
    selector: 'app-filter-form',
    templateUrl: './filter-form.component.html',
  })
  export class FilterFormComponent implements OnInit, AfterViewInit, OnDestroy {
    
    @ViewChild('datepicker', { static: true }) datepicker!: ElementRef;
    @ViewChild('datepicker2', { static: true }) datepicker2!: ElementRef;
    dateOptions: any = {
        dateFormat: 'd/m/Y', // Định dạng ngày thành dd/MM/yyyy
        
      };
    constructor(
        public modal: NgbActiveModal
      ) {
      }
    
      ngAfterViewInit(): void {
        flatpickr(this.datepicker.nativeElement, {
            dateFormat: 'd/m/Y', // Định dạng thành dd/MM/yyyy
            locale: Vietnamese,
          });
          flatpickr(this.datepicker2.nativeElement, {
            dateFormat: 'd/m/Y', // Định dạng thành dd/MM/yyyy
            locale: Vietnamese,
          });
      }
      ngOnDestroy(): void {
          throw new Error('Method not implemented.');
      }
      ngOnInit(): void {
          throw new Error('Method not implemented.');
      }
}