import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
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
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { IWarehouseModel, WarehouseService } from 'src/app/_services/warehouce.service';

@Component({
  selector: 'app-warehouse-save',
  templateUrl: './warehouse-save.component.html',
  styleUrls: ['./warehouse-save.component.scss'],
})
export class WarehouseSaveComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id: string | "";
  isCollapsed1 = false;
  isCollapsed2 = true;
  isLoading = false;
  // Single model
  aUser: Observable<IWarehouseModel>;
  warehouseModel: IWarehouseModel = {
    id: '', name: '', code: '',
    address: '',
    isActive: false
  };

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;
  swalOptions: SweetAlertOptions = {};
  roles$: Observable<DataTablesResponse>;
  constructor(
    private apiService: WarehouseService,
    private cdr: ChangeDetectorRef,
    private fb: FormBuilder,
    public modal: NgbActiveModal
  ) { }

  ngAfterViewInit(): void { }

  ngOnInit(): void {
    if (this.id != "") {
      this.getData(this.id);
    } else {
      this.create();
    }
  }

  getData(id: string) {
    this.aUser = this.apiService.getWarehouse(id);
    this.aUser.subscribe((user: any) => {
      this.warehouseModel = user.value;
    });
  }

  create() {
    this.warehouseModel = { id: '', name: '', code: '', address: '', isActive: false };
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
        this.warehouseModel.id != ""
          ? 'Warehouse updated successfully!'
          : 'Warehouse created successfully!',
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
      this.apiService.updateWarehouse(this.warehouseModel.id, this.warehouseModel).subscribe({
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
      this.apiService.createWarehouse(this.warehouseModel).subscribe({
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

    if (this.warehouseModel.id != "") {
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

  ngOnDestroy(): void {
  }
}
