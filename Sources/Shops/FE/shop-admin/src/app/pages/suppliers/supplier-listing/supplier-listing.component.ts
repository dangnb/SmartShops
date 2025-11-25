import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  Signal,
  ViewChild,
} from '@angular/core';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { BehaviorSubject } from 'rxjs';
import { SweetAlertOptions } from 'sweetalert2';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { SupplierService } from 'src/app/_services/supplier.service';
import { SupplierUploadComponent } from '../supplier-upload/supplier-upload.component';
import { SupplierSaveComponent } from '../supplier-save/supplier-save.component';
import { ICityModel } from 'src/app/_services/city.service';

@Component({
    selector: 'supplier-listing',
    templateUrl: './supplier-listing.component.html',
    styleUrls: ['./supplier-listing.component.scss'],
    standalone: false
})
export class SupplierListingComponent implements OnInit, AfterViewInit, OnDestroy {


  @ViewChild('deleteSwal')
  public readonly deleteSwal!: SwalComponent;
  @ViewChild('successSwal')
  public readonly successSwal!: SwalComponent;

  isLoading = false;

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;

  swalOptions: SweetAlertOptions = {};

  searchTerm: string = "";

  private _items$ = new BehaviorSubject<any>([]);
  public items$ = this._items$.asObservable();

  public pageIndex = 1;
  public pageSize = 10;
  public total = 0;

  modalConfig: NgbModalOptions = {
    modalDialogClass: 'modal-dialog modal-dialog-centered mw-650px',
  };

  constructor(
    private apiService: SupplierService,
    private cdr: ChangeDetectorRef,
    private modalService: NgbModal
  ) { }

  ngAfterViewInit(): void { }

  changePage(page: number) {
    this.pageIndex = page;
    this.filter(this.searchTerm);
  }

  changeSearTerm() {
    this.filter(this.searchTerm);
  }

  filter(searchTerm: string) {
    this.apiService
      .gets({
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

  delete(id: string) {
    this.deleteSwal.fire().then((clicked) => {
      if (clicked.isConfirmed) {
        this.apiService.delete(id).subscribe(() => {
          this.successSwal.fire().then((val) => {
            this.filter(this.searchTerm);
          });
        });
      }
    });
  }

  edit(id: string) {
    const modalRef = this.modalService.open(SupplierSaveComponent, {
      size: 'lg',
      backdrop: 'static',
      keyboard: true,
    });
    modalRef.componentInstance.id = id;
    modalRef.result.then(
      () => this.filter(this.searchTerm),
      () => { }
    );
  }

  upload() {
    const modalRef = this.modalService.open(SupplierUploadComponent, {
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
    const modalRef = this.modalService.open(SupplierSaveComponent, {
      size: 'xl',
      backdrop: 'static',
      keyboard: true,
    });
    modalRef.componentInstance.id = 0;
    modalRef.result.then(
      () => this.filter(this.searchTerm),
      () => { }
    );
  }
  ngOnDestroy(): void {
  }
}
