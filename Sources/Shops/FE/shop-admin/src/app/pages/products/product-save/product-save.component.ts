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
import { IProductModel, ProductService } from 'src/app/_services/product.service';

@Component({
  selector: 'app-product-save',
  templateUrl: './product-save.component.html',
  styleUrls: ['./product-save.component.scss'],
})
export class ProductSaveComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id:number =0;
  isCollapsed1 = false;
  isCollapsed2 = true;
  isLoading = false;
  // Single model
  productModel: IProductModel = { id:0, name: '', code: '', price: "0",isActive: true, productType: 0 };

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;
  swalOptions: SweetAlertOptions = {};
  roles$: Observable<DataTablesResponse>;
  constructor(
    private apiService: ProductService,
    private cdr: ChangeDetectorRef,
    private fb:FormBuilder,
    public modal: NgbActiveModal
  ) {}


  ngAfterViewInit(): void {}

  ngOnInit(): void {
    if(this.id >0){
        this.getData(this.id);
    }else{
      this.create();
    }
  }

  getData(id: number) {
     this.apiService.getProduct(id).subscribe((user: any) => {
      this.productModel = user.value;
    });
  }

  create() {
    this.productModel = { id:0, name: '', code: '', price: "0",isActive: true, productType: 0 };
  }
  onNumberChange(value: string) {
    // Loại bỏ dấu chấm khi cập nhật giá trị thực trong component
    this.productModel.price = value.replace(/\./g, '');
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
        this.productModel.id > 0
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
      this.apiService.updateProduct(this.productModel.id, this.productModel).subscribe({
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
      this.apiService.createProduct(this.productModel).subscribe({
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

    if (this.productModel.id > 0) {
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
    this.noticeSwal.fire().then((val)=>{
      this.modal.close();
    });
  }

  ngOnDestroy(): void {
  }
}
