import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  signal,
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
import { CategoryService } from 'src/app/_services/category.service';
import { TreeNode } from 'node_modules/primeng/api/public_api';

@Component({
  selector: 'app-product-save',
  templateUrl: './product-save.component.html',
  styleUrls: ['./product-save.component.scss'],
})
export class ProductSaveComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id: string | undefined = undefined;
  isLoading = false;
  isParent = false;
  isRequired = false;
  // Single model
  productModel: IProductModel = {
    id: 0, name: '',
    code: '',
    barCode: '',
    categoryId: undefined,
    isActive: true
  };

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;
  selectedNode: any;
  swalOptions: SweetAlertOptions = {};
  roles$: Observable<DataTablesResponse>;
  categorySignal = signal<TreeNode<any>[]>([]);

  constructor(
    private categoryService: CategoryService,
    private apiService: ProductService,
    private cdr: ChangeDetectorRef,
    private fb: FormBuilder,
    public modal: NgbActiveModal
  ) { }


  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.getTree();
  }

  get categorySignalValue(): TreeNode<any>[] {
    return this.categorySignal();
  }

  handleChange(event: any) {
    if (event) {
      this.productModel.categoryId = event.data;
    }
  }

  getData(id: string | undefined) {
    this.apiService.getProduct(id ?? "").subscribe((user: any) => {
      this.productModel = user.value;
      this.selectedNode = this.findNodeById(this.categorySignalValue);
    });
  }

  findNodeById(nodes: any): TreeNode | null {
    for (let node of nodes) {
      if (node.data === this.productModel.categoryId) {
        return node;
      }
      if (node.children?.length! > 0) {
        const found = this.findNodeById(node.children);
        if (found) return found;
      }
    }
    return null;
  }

  private getTree() {
    return this.categoryService.getTree().subscribe({
      next: (val: any) => {
        this.categorySignal.set(this.toTreeNode(val.value));
        if (this.id != '' || this.id != undefined) {
          this.getData(this.id);
        } else {
          this.create();
        }
      },
      complete: () => { },
    });
  }

  create() {
    this.productModel = { id: 0, name: '', code: '', barCode: '', categoryId: undefined };
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
      this.apiService.updateProduct(this.productModel.id?.toString() ?? "", this.productModel).subscribe({
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
    this.noticeSwal.fire().then((val) => {
      this.modal.close();
    });
  }

  private toTreeNode(data: any[]): TreeNode[] {
    return data
      .filter((x) => x.id != this.id)
      .map((item) => ({
        label: item.name, // 👈 bạn đổi label ở đây
        key: item.id, // key để select
        data: item.id, // giữ lại data gốc nếu cần
        children: item.children ? this.toTreeNode(item.children) : [],
      }));
  }

  ngOnDestroy(): void {
  }
}
