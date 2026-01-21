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
import { BehaviorSubject, Observable } from 'rxjs';
import { SweetAlertOptions } from 'sweetalert2';
import { NgbCollapseModule, NgbDropdownModule, NgbModal, NgbModalOptions, NgbNavModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { GoodsReceiptService, IGoodsReceiptModel } from '../../_services/goodsreceipt.service';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';
import { CrudModule } from 'src/app/modules/crud/crud.module';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'src/app/_metronic/shared/shared.module';

@Component({
    selector: 'goodsreceipt-listing',
    standalone: true,
    imports: [
        CommonModule,
        NgxPaginationModule,
        CrudModule,
        FormsModule,
        ReactiveFormsModule,
        SharedModule,
        CrudModule,
        SharedModule,
        NgbNavModule,
        NgbDropdownModule,
        NgbCollapseModule,
        NgbTooltipModule,
    ],
    templateUrl: './goodsreceipt-listing.component.html',
    styleUrls: ['./goodsreceipt-listing.component.scss'],
})
export class GoodsReceiptListingComponent implements OnInit, AfterViewInit, OnDestroy {
    isLoading = false;

    @ViewChild('noticeSwal')
    noticeSwal!: SwalComponent;

    swalOptions: SweetAlertOptions = {};

    searchTerm: string = "";

    private _items$ = new BehaviorSubject<IGoodsReceiptModel[]>([]);
    public items$ = this._items$.asObservable();

    public pageIndex = 1;
    public pageSize = 10;
    public total = 0;

    modalConfig: NgbModalOptions = {
        modalDialogClass: 'modal-dialog modal-dialog-centered mw-650px',
    };
    suppliers$!: Observable<{ id: string; name: string }[]>;
    warehouses$!: Observable<{ id: string; name: string }[]>;

    statuses = [
        { value: null, label: 'Tất cả' },
        { value: 0, label: 'Mới tạo' },
        { value: 1, label: 'Đã ghi sổ' }
    ];

    filterForm: FormGroup;
    constructor(
        private apiService: GoodsReceiptService,
        private cdr: ChangeDetectorRef,
        private modalService: NgbModal,
        private fb: FormBuilder
    ) { }

    ngAfterViewInit(): void { }

    changePage(page: number) {
        this.pageIndex = page;
        this.filter();
    }

    changeSearTerm() {
        this.filter();
    }

    filter() {
        this.apiService
            .filter({
                pageIndex: this.pageIndex,
                pageSize: this.pageSize,
                ...this.filterForm?.value,
                warehouseId: this.filterForm?.value.warehouseId || null,
                supplierId: this.filterForm?.value.supplierId || null,
                status: this.filterForm?.value.status || null
            })
            .subscribe((val) => {
                var a = val.value.items;
                this._items$.next(val.value.items);
                this.pageIndex = val['value'].pageIndex;
                this.pageSize = val['value'].pageSize;
                this.total = val['value'].totalCount;
            });
    }

    ngOnInit(): void {
        this.filterForm = this.fb.group({
            searchTerm: [''],
            supplierId: [null],
            warehouseId: [null],
            status: [null],
            fromDate: [''],
            toDate: ['']
        });
        this.filter();
    }

    formatDate(controlName: 'fromDate' | 'toDate') {
        const value = this.filterForm.get(controlName)?.value;
        if (!value) return;

        // nếu user nhập yyyy-MM-dd (date picker)
        if (/^\d{4}-\d{2}-\d{2}$/.test(value)) {
            const [y, m, d] = value.split('-');
            this.filterForm.patchValue({
                [controlName]: `${d}/${m}/${y}`
            });
        }
    }

    delete(id: string) {
        // this.deleteSwal.fire().then((clicked) => {
        //     if (clicked.isConfirmed) {
        //         this.apiService.delete(id).subscribe(() => {
        //             this.successSwal.fire().then((val) => {
        //                 this.filter(this.searchTerm);
        //             });
        //         });
        //     }
        // });
    }

    edit(id: string) {
        // const modalRef = this.modalService.open(SupplierSaveComponent, {
        //     size: 'xl',
        //     backdrop: 'static',
        //     keyboard: true,
        // });
        // modalRef.componentInstance.id = id;
        // modalRef.result.then(
        //     () => this.filter(this.searchTerm),
        //     () => { }
        // );
    }

    viewDetails(id: string) {
        // const modalRef = this.modalService.open(SupplierDetailComponent, {
        //     size: 'xl',
        //     backdrop: 'static',
        //     keyboard: true,
        // });
        // modalRef.componentInstance.id = id;
        // modalRef.result.then(
        //     () => this.filter(this.searchTerm),
        //     () => { }
        // );
    }


    upload() {
        // const modalRef = this.modalService.open(SupplierUploadComponent, {
        //     size: 'lg',
        //     backdrop: 'static',
        //     keyboard: true,
        // });
        // modalRef.result.then(
        //     () => this.filter(this.searchTerm),
        //     () => { }
        // );
    }

    public create() {
        // const modalRef = this.modalService.open(SupplierSaveComponent, {
        //     size: 'xl',
        //     backdrop: 'static',
        //     keyboard: true,
        // });
        // modalRef.componentInstance.id = 0;
        // modalRef.result.then(
        //     () => this.filter(this.searchTerm),
        //     () => { }
        // );
    }
    ngOnDestroy(): void {
    }
}
