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
import { FormBuilder, NgForm } from '@angular/forms';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import {  Observable } from 'rxjs';
import { DataTablesResponse } from 'src/app/_fake/services/user-service';
import { SweetAlertOptions } from 'sweetalert2';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IPermissionModel, PermisionService } from 'src/app/_services/permision.service';

@Component({
    selector: 'app-permission-save',
    templateUrl: './permission-save.component.html',
    styleUrls: ['./permission-save.component.scss'],
    standalone: false
})
export class PermissionSaveComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id:string ="";
  isCollapsed1 = false;
  isCollapsed2 = true;
  isLoading = false;
  // Single model
  aUser: Observable<IPermissionModel>;
  permissionModel: IPermissionModel = {
    id:"",
    description:"",
    code:"",
    groupCode:"",
    groupName:""
   };

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;
  swalOptions: SweetAlertOptions = {};
  roles$: Observable<DataTablesResponse>;
  constructor(
    private apiService: PermisionService,
    private cdr: ChangeDetectorRef,
    private fb:FormBuilder,
    public modal: NgbActiveModal
  ) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    if(this.id != ""){
        this.getData(this.id);
    }else{
      this.create();
    }
  }

  getData(id: string) {
    this.aUser = this.apiService.getPermission(id);
    this.aUser.subscribe((user: any) => {
      this.permissionModel = user.value;
    });
  }

  create() {
    this.permissionModel ={
      id:"",
      description:"",
      code:"",
      groupCode:"",
      groupName:""
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
      text:
        this.permissionModel.id !=""
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
      this.apiService.updatePermission(this.permissionModel.id, this.permissionModel).subscribe({
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
      this.apiService.createPermission(this.permissionModel).subscribe({
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

    if (this.permissionModel.id !=  "") {
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
