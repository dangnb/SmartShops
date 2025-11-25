import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  Input,
  OnDestroy,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { SweetAlertOptions } from 'sweetalert2';
import { CityService } from 'src/app/_services/city.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { WardService } from 'src/app/_services/ward.service';
import { ISupplierModel, SupplierService } from 'src/app/_services/supplier.service';

type Tabs = 'Customer' | 'Payment';

@Component({
  selector: 'app-supplier-save',
  templateUrl: './supplier-save.component.html',
  styleUrls: ['./supplier-save.component.scss'],
})
export class SupplierSaveComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id: string = '';
  isLoading = false;
  // Single model
  supplierModel: ISupplierModel = {
    id: '',
    code: '',
    name: '',
    shortName: '',
    taxCode: '',
    phone: '',
    email: '',
    website: '',
    contactName: '',
    contactPhone: '',
    contactEmail: '',
    provinceId: '',
    wardId: '',
    addressLine: '',
    fullAddress: '',
    bankName: '',
    bankAccountNo: '',
    bankAccountName: '',
    paymentTermDays: 0,
    note: '',
    isActive: false
  };

  provinces = signal([]);


  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;
  swalOptions: SweetAlertOptions = {};


  constructor(
    private apiService: SupplierService,
    private apiCityService: CityService,
    private apiWardService: WardService,
    private cdr: ChangeDetectorRef,
    public modal: NgbActiveModal
  ) { }


  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.getProvide();
    if (this.id != '') {
      this.getData(this.id);
    } else {
      this.create();
    }
  }


  private async getProvide() {
    var response = await this.apiCityService.getAll()
    debugger
  }



  getData(id: string) {
    this.apiService.get(id).subscribe((value: any) => {
      this.supplierModel = value.value;
    });
  }

  create() {
  }

  onSubmit(event: Event, myForm: NgForm) {
    if (myForm && myForm.invalid) {
      return;
    }

    this.isLoading = true;

    const successAlert: SweetAlertOptions = {
      icon: 'success',
      title: 'Success!',
      text:
        this.supplierModel.id != ''
          ? 'User updated successfully!'
          : 'User created successfully!',
    };
    const errorAlert: SweetAlertOptions = {
      icon: 'error',
      title: 'Error!',
      text: '',
    };

    const completeFn = () => {
      this.isLoading = false;
    };

    const updateFn = () => {
      this.apiService
        .update(this.supplierModel.id, this.supplierModel)
        .subscribe({
          next: () => {
            this.showAlert(successAlert);
          },
          error: (error) => {
            errorAlert.text = this.extractText(error.error);
            this.showAlert(errorAlert);
            this.isLoading = false;
          },
          complete: completeFn,
        });
    };

    const createFn = () => {
      this.apiService.create(this.supplierModel).subscribe({
        next: () => {
          this.showAlert(successAlert);
        },
        error: (error) => {
          errorAlert.text = this.extractText(error.error);
          this.showAlert(errorAlert);
          this.isLoading = false;
        },
        complete: completeFn,
      });
    };

    if (this.id != '') {
      updateFn();
    } else {
      createFn();
    }
  }

  extractText(obj: any): string {
    var textArray: string[] = [];
    for (var key in obj) {
      if (typeof obj[key] === 'string') {
        // If the value is a string, add it to the 'textArray'
        textArray.push(obj[key]);
      } else if (typeof obj[key] === 'object') {
        // If the value is an object, recursively call the function and concatenate the results
        textArray = textArray.concat(this.extractText(obj[key]));
      }
    }

    // Use a Set to remove duplicates and convert back to an array
    var uniqueTextArray = Array.from(new Set(textArray));

    // Convert the uniqueTextArray to a single string with line breaks
    var text = uniqueTextArray.join('\n');

    return text;
  }

  showAlert(swalOptions: SweetAlertOptions) {
    let style = swalOptions.icon?.toString() || 'success';
    if (swalOptions.icon === 'error') {
      style = 'danger';
    }
    this.swalOptions = Object.assign(
      {
        buttonsStyling: false,
        confirmButtonText: 'Ok, got it!',
        customClass: {
          confirmButton: 'btn btn-' + style,
        },
      },
      swalOptions
    );
    this.cdr.detectChanges();
    this.noticeSwal.fire().then((val) => {
      this.modal.close();
    });
  }

  ngOnDestroy(): void { }
}
