import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { BehaviorSubject, Observable } from 'rxjs';
import { DataTablesResponse } from 'src/app/_fake/services/user-service';
import { SweetAlertOptions } from 'sweetalert2';
import { RoleService } from 'src/app/_fake/services/role.service';
import { CityService, ICityModel } from 'src/app/_services/city.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { PaymentService } from 'src/app/_services/payment.service';
import { FilterFormComponent } from '../filter-form/filter-form.component';

@Component({
    selector: 'app-payment-listing',
    templateUrl: './payment-listing.component.html',
    styleUrls: ['./payment-listing.component.scss'],
    standalone: false
})
export class PaymentListingComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild('deleteSwal')
  public readonly deleteSwal!: SwalComponent;
  @ViewChild('successSwal')
  public readonly successSwal!: SwalComponent;

  isCollapsed1 = false;
  isCollapsed2 = true;
  isLoading = false;

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;

  swalOptions: SweetAlertOptions = {};

  
  searchTerm: string= "";

  private _items$ = new BehaviorSubject<any>([]);
  public items$ = this._items$.asObservable();

  public pageIndex = 1;
  public pageSize = 10;
  public total = 0;

  modalConfig: NgbModalOptions = {
    modalDialogClass: 'modal-dialog modal-dialog-centered mw-650px',
  };

  constructor(
    private apiService: PaymentService,
    private roleService: RoleService,
    private cdr: ChangeDetectorRef,
    private modalService: NgbModal
  ) {}

  ngAfterViewInit(): void {}

  changePage(page: number) {
    this.pageIndex = page;
    this.filter(this.searchTerm);
  }

  changeSearTerm() {
    this.filter(this.searchTerm);
  }

  filter(searchTerm: string) {
    this.apiService
      .getCities({
        searchTerm,
        pageIndex: this.pageIndex,
        pageSize: this.pageSize,
      })
      .subscribe((val) => {
        this._items$.next(val.value.items);
        this.pageIndex = val['value'].pageIndex;
        this.pageSize = val['value'].pageSize;
        this.total = val['value'].totalCount;
      });
  }

  ngOnInit(): void {
    this.filter(this.searchTerm);
  }

  
  openModalFilter() {
    const modalRef = this.modalService.open(FilterFormComponent, {
      size: 'lg',
      backdrop: 'static',
      keyboard: true,
    });
    modalRef.result.then(
      () => this.filter(this.searchTerm),
      () => {}
    );
  }

  delete(id: number) {
    // this.deleteSwal.fire().then((clicked) => {
    //   if (clicked.isConfirmed) {
    //     this.apiService.deleteCity(id).subscribe(() => {
    //       this.successSwal.fire().then((val) => {
    //         this.filter(this.searchTerm);
    //       });
    //     });
    //   }
    // });
  }
  ngOnDestroy(): void {
  }
}
