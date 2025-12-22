import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  Input,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { BehaviorSubject } from 'rxjs';
import { SweetAlertOptions } from 'sweetalert2';
import { CityService, } from 'src/app/_services/city.service';
import { NgbActiveModal, } from '@ng-bootstrap/ng-bootstrap';
import {
  DistrictService,
} from 'src/app/_services/district.service';
import { WardService } from 'src/app/_services/ward.service';
import {
  VillageService,
} from 'src/app/_services/village.service';
import {
  CustomerService,
} from 'src/app/_services/customer.service';
import { ISupplierModel, SupplierService } from 'src/app/_services/supplier.service';


@Component({
  selector: 'app-supplier-detail',
  templateUrl: './supplier-detail.component.html',
  styleUrls: ['./supplier-detail.component.scss'],
})
export class SupplierDetailComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id: string = '';
  isLoading = false;
  // Single model

  supplierModel: ISupplierModel;

  constructor(
    private apiService: SupplierService,
    private cdr: ChangeDetectorRef,
    public modal: NgbActiveModal
  ) { }

  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.getData();
  }


  getData() {
    this.apiService.get(this.id).subscribe((val) => {
      this.supplierModel = val.value;
    });
  }

  ngOnDestroy(): void { }
}
