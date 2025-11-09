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
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { WardService } from 'src/app/_services/ward.service';
import { VillageService } from 'src/app/_services/village.service';
import { CustomerSaveComponent } from '../customer-save/customer-save.component';
import { CustomerService } from 'src/app/_services/customer.service';
import { CustomerUploadComponent } from '../customer-upload/customer-upload.component';
@Component({
  selector: 'customer-listing',
  templateUrl: './customer-listing.component.html',
  styleUrls: ['./customer-listing.component.scss'],
})
export class CustomerListingComponent implements OnInit, AfterViewInit, OnDestroy {
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
    private apiService: CustomerService,
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
      .getCustomers({
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

  delete(id: number) {
    this.deleteSwal.fire().then((clicked) => {
      if (clicked.isConfirmed) {
        this.apiService.deleteCustomer(id).subscribe(() => {
          this.successSwal.fire().then((val) => {
            this.filter(this.searchTerm);
          });
        });
      }
    });
  }

  edit(id: number) {
    const modalRef = this.modalService.open(CustomerSaveComponent, {
      size: 'lg',
      backdrop: 'static',
      keyboard: true,
    });
    modalRef.componentInstance.id = id;
    modalRef.result.then(
      () => this.filter(this.searchTerm),
      () => {}
    );
  }

  upload() {
    const modalRef = this.modalService.open(CustomerUploadComponent, {
      size: 'lg',
      backdrop: 'static',
      keyboard: true,
    });
    modalRef.result.then(
      () => this.filter(this.searchTerm),
      () => { }
    );
  }

  public create() {
    const modalRef = this.modalService.open(CustomerSaveComponent, {
      size: 'xl',
      backdrop: 'static',
      keyboard: true,
    });
    modalRef.componentInstance.id = 0;
    modalRef.result.then(
      () => this.filter(this.searchTerm),
      () => {}
    );
  }
  ngOnDestroy(): void {
  }
}
