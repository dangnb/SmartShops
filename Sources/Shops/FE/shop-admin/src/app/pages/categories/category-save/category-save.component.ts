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
import {
  DistrictService,
  IDistrictModel,
} from 'src/app/_services/district.service';
import { IWardModel, WardService } from 'src/app/_services/ward.service';
import {
  IVillageModel,
  VillageService,
} from 'src/app/_services/village.service';
import {
  CustomerService,
  ICustomerModel,
} from 'src/app/_services/customer.service';
import { CategoryService, ICreateCategoryModel } from 'src/app/_services/category.service';

type Tabs = 'Customer' | 'Payment';

@Component({
  selector: 'app-category-save',
  templateUrl: './category-save.component.html',
  styleUrls: ['./category-save.component.scss'],
})
export class CategorySaveComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id: string = '';
  isLoading = false;
  parentOptions = [];
  // Single model
  createModel: ICreateCategoryModel = {
    id: '',
    code: '',
    name: '',
    parentId: undefined,
    sortOrder: 0,
    level: 0,
    isActive: false
  };

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;
  swalOptions: SweetAlertOptions = {};


  constructor(
    private apiService: CategoryService,
    private cdr: ChangeDetectorRef,
    public modal: NgbActiveModal
  ) { }


  ngAfterViewInit(): void { }

  ngOnInit(): void {
    if (this.id != '') {
      this.getData(this.id);
    } else {
      this.create();
    }
  }

  getData(id: string) {
    this.apiService.get(id).subscribe((value: any) => {
      this.createModel = value.value;
    });
  }

  create() {
    this.createModel = {
      id: '',
      code: '',
      name: '',
      parentId: undefined,
      sortOrder: 0,
      level: 0,
      isActive: false
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
      text:
        this.createModel.id != undefined && this.createModel.id != ''
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
        .put(this.createModel.id, this.createModel)
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
      this.apiService.post(this.createModel).subscribe({
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
