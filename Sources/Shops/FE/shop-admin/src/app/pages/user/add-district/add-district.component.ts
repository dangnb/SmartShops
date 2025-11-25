import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import moment from 'moment';
import { IPermissionModel, PermisionService } from 'src/app/_services/permision.service';
import { IRoleModel, RoleService } from 'src/app/_services/role.service';
import { SweetAlertOptions } from 'sweetalert2';
import { NgbActiveModal, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { NgForm } from '@angular/forms';
import { CityService, ICityModel } from 'src/app/_services/city.service';
import { DistrictService, IDistrictModel } from 'src/app/_services/district.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
    selector: 'app-add-district',
    templateUrl: './add-district.component.html',
    standalone: false
})
export class AddDistrictComponent implements OnInit {
  @Input() id: string = '';
  private districtCodes: string[] = [];
  isLoading = false;
  private _city$ = new BehaviorSubject<ICityModel[]>([]);
  public city$ = this._city$.asObservable();

  private _district$ = new BehaviorSubject<IDistrictModel[]>([]);
  public district$ = this._district$.asObservable();

  @ViewChild('formModal')
  formModal: TemplateRef<any>;

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;

  swalOptions: SweetAlertOptions = {};

  modalConfig: NgbModalOptions = {
    modalDialogClass: 'modal-dialog modal-dialog-centered mw-650px',
  };
  constructor(
    public modal: NgbActiveModal,
    private cdr: ChangeDetectorRef,
    private apiService: UserService,
    private cityService: CityService,
    private districtService: DistrictService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.getDistrictCode();
  }

  getDistrictCode() {
    this.apiService.getDistrictCode(this.id).subscribe((val) => {
      this.districtCodes = val.value;
      this.getCity();
    });
  }

  getCity() {
    this.cityService.getAll().subscribe((val) => {
      this._city$.next(val.value);
      this.getDistrict(val.value[0].id);
    });
  }

  changeCity(data: any) {
    this.getDistrict(data.value);
  }

  private getDistrict(cityId: number) {
    this.districtService.getByCity(cityId).subscribe((val) => {
      this._district$.next(val.value);
    });
  }

  checkSelectId(code: string) {
    return this.districtCodes && this.districtCodes.includes(code);
  }

  selectPermision(code: string) {
    if (!this.districtCodes) this.districtCodes = [];
    if (this.districtCodes.includes(code)) {
      this.districtCodes = this.districtCodes.filter((x) => x != code);
    } else {
      this.districtCodes.push(code);
    }
  }

  onSubmit(event: Event, myForm: NgForm) {
    if (myForm && myForm.invalid) {
      return;
    }

    this.isLoading = true;

    const successAlert: SweetAlertOptions = {
      icon: 'success',
      title: 'Success!',
      text: 'Gán quyền quản lý quận thành công',
    };
    const errorAlert: SweetAlertOptions = {
      icon: 'error',
      title: 'Error!',
      text: '',
    };

    const completeFn = () => {
      this.isLoading = false;
      this.cdr.detectChanges();
    };

    const adDistrict = () => {
      this.apiService
        .addDistrictForUser(this.id, this.districtCodes)
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
    adDistrict();
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
        confirmButtonText: 'Đồng ý!',
        customClass: {
          confirmButton: 'btn btn-' + style,
        },
      },
      swalOptions
    );
    this.cdr.detectChanges();
    this.noticeSwal.fire().then((val)=>{
      this.modal.close();
    });
  }
}
