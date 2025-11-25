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


@Component({
    selector: 'app-customer-upload',
    templateUrl: './customer-upload.component.html',
    styleUrls: ['./customer-upload.component.scss'],
    standalone: false
})
export class CustomerUploadComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id: string = '';
  isLoading = false;
  // Single model
  customerModel = {
    cityId: 0,
    districtId: 0,
    wardId: 0,
    villageId: 0,
  };

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;
  swalOptions: SweetAlertOptions = {};
  private _district$ = new BehaviorSubject<any>([]);
  public district$ = this._district$.asObservable();
  // @ViewChild('form', { static: true }) form: NgForm;

  private _city$ = new BehaviorSubject<any>([]);
  public city$ = this._city$.asObservable();

  private _ward$ = new BehaviorSubject<any>([]);
  public ward$ = this._ward$.asObservable();

  private _village$ = new BehaviorSubject<any>([]);
  public village$ = this._village$.asObservable();


  constructor(
    private apiService: CustomerService,
    private apiVillageService: VillageService,
    private apiDistricyService: DistrictService,
    private apiCityService: CityService,
    private apiWardService: WardService,
    private cdr: ChangeDetectorRef,
    public modal: NgbActiveModal
  ) { }

  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.getCities();
    this.create();
  }

  getCities() {
    this.apiCityService.getAll().subscribe((val) => {
      this._city$.next(val.value);
    });
  }

  getVillages(wardId: number) {
    this.apiVillageService.getByWard(wardId).subscribe((val) => {
      this._village$.next(val.value);
    });
  }

  getWards(districtId: number) {
    this.apiWardService.getByProvince(districtId).subscribe((val) => {
      this._ward$.next(val.value);
    });
  }

  changeCity() {
    this.getDistrict(this.customerModel.cityId);
  }

  changeWard() {
    this.getVillages(this.customerModel.wardId);
  }

  changeDistrict() {
    this.getWards(this.customerModel.districtId);
  }

  getDistrict(cityId: number) {
    this.apiDistricyService.getByCity(cityId).subscribe((val) => {
      this._district$.next(val.value);
    });
  }
  selectedFile: File | null = null;
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }
  create() {
    this.customerModel = {
      districtId: 0,
      cityId: 0,
      wardId: 0,
      villageId: 0,
    };
  }

  onSubmit(event: Event, myForm: NgForm) {
    if (myForm && myForm.invalid) {
      return;
    }

    this.isLoading = true;

    const successAlert: SweetAlertOptions = {
      icon: 'success',
      title: 'Success!',
      text: 'Customer upload successfully!'
    };
    const errorAlert: SweetAlertOptions = {
      icon: 'error',
      title: 'Error!',
      text: '',
    };

    const completeFn = () => {
      this.isLoading = false;
    };


    const uploadFn = () => {
      const formData = new FormData();
      formData.append('file', this.selectedFile!);
      formData.append('villageId', `${this.customerModel.villageId}`);
      this.apiService
        .uploadCustomer(formData)
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

    uploadFn();

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
