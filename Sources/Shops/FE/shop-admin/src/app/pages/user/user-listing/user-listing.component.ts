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
import { UserSaveComponent } from '../user-save/user-save.component';
import { UserService } from 'src/app/_services/user.service';
import { AddDistrictComponent } from '../add-district/add-district.component';

@Component({
    selector: 'app-user-listing',
    templateUrl: './user-listing.component.html',
    styleUrls: ['./user-listing.component.scss'],
    standalone: false
})
export class UserListingComponent implements OnInit, AfterViewInit, OnDestroy {
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
    private apiService: UserService,
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
      .getUsers({
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
        this.apiService.deleteUser(id).subscribe(() => {
          this.successSwal.fire().then((val) => {
            this.filter(this.searchTerm);
          });
        });
      }
    });
  }

  addDistrictForUser(id: string) {
    const modalRef = this.modalService.open(AddDistrictComponent, {
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

  edit(id: number) {
    const modalRef = this.modalService.open(UserSaveComponent, {
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

  public create() {
    const modalRef = this.modalService.open(UserSaveComponent, {
      size: 'lg',
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
