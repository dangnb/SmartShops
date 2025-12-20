import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { BehaviorSubject } from 'rxjs';
import { SweetAlertOptions } from 'sweetalert2';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { CategorySaveComponent } from '../category-save/category-save.component';
import { CategoryUploadComponent } from '../category-upload/category-upload.component';
import { CategoryService } from 'src/app/_services/category.service';
@Component({
  selector: 'category-listing',
  templateUrl: './category-listing.component.html',
  styleUrls: ['./category-listing.component.scss'],
})
export class CategoryListingComponent implements OnInit, AfterViewInit, OnDestroy {
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
    private apiService: CategoryService,
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
      .getTree()
      .subscribe((val) => {
        debugger
        this._items$.next(val.value);
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
    const modalRef = this.modalService.open(CategorySaveComponent, {
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
    const modalRef = this.modalService.open(CategoryUploadComponent, {
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
    const modalRef = this.modalService.open(CategorySaveComponent, {
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
