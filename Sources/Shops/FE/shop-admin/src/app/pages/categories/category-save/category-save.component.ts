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
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { CategoryService, ICreateCategoryModel } from 'src/app/_services/category.service';
import { TreeNode } from 'primeng/api';

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
  selectedNode: any;
  isRequired = false;
  isParent = false;
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
  categorySignal = signal<TreeNode<any>[]>([]);

  constructor(
    private apiService: CategoryService,
    private cdr: ChangeDetectorRef,
    public modal: NgbActiveModal
  ) { }


  ngAfterViewInit(): void { }

  ngOnInit(): void {
    this.getTree();
  }
  // Khi sử dụng trong template, đảm bảo bạn lấy giá trị thực:
  get categorySignalValue(): TreeNode<any>[] {
    return this.categorySignal();
  }

  handleChange(event: any) {
    if (event) {
      this.createModel.parentId = event.data;
    }
  }

  toTreeNode(data: any[]): TreeNode[] {
    return data
      .filter((x) => x.id != this.id)
      .map((item) => ({
        label: item.name, // 👈 bạn đổi label ở đây
        key: item.id, // key để select
        data: item.id, // giữ lại data gốc nếu cần
        children: item.children ? this.toTreeNode(item.children) : [],
      }));
  }

  getTree() {
    return this.apiService.getTree().subscribe({
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

  getData(id: string) {
    this.apiService.get(id).subscribe((value: any) => {
      this.createModel = value.value;
      if (this.createModel.parentId == "" || this.createModel.parentId == null || this.createModel.parentId == undefined) {
        this.isParent = true;
      }
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
