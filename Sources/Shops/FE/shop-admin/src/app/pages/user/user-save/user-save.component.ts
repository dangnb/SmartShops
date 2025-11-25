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
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { IUserModel, UserService } from 'src/app/_services/user.service';
import { IRoleModel, RoleService } from 'src/app/_services/role.service';

@Component({
  selector: 'app-user-save',
  templateUrl: './user-save.component.html',
  styleUrls: ['./user-save.component.scss'],
})
export class UserSaveComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() id:string ="";
  isLoading = false;
  // Single model
  aUser: Observable<IUserModel>;

  private _role$ = new BehaviorSubject<IRoleModel[]>([])
  public role$ = this._role$.asObservable();

  userModel: IUserModel = { 
    id: "",
    userName:"",
    fullName: "",
    taxCode:"",
    lastName: "",
    dayOfBirth: "",
    email: "",
    phoneNumber: "",
    address: "",
    roleCodes: []
   };

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;
  swalOptions: SweetAlertOptions = {};
  roles$: Observable<DataTablesResponse>;
  constructor(
    private apiService: UserService,
    private roleService: RoleService,
    private cdr: ChangeDetectorRef,
    private fb:FormBuilder,
    public modal: NgbActiveModal
  ) {}

  ngAfterViewInit(): void {}

  ngOnInit(): void {
    this.getList();
    if(this.id !=""){
        this.getData(this.id);
    }else{
      this.create();
    }
  }

  checkSelectId(code:string){
    return this.userModel.roleCodes && this.userModel.roleCodes.includes(code);
 }

 selectPermision(code:string){
   if(!this.userModel.roleCodes) 
     this.userModel.roleCodes=[];
   if(this.userModel.roleCodes.includes(code)){
     this.userModel.roleCodes= this.userModel.roleCodes.filter(x=>x != code);
   }else{
     this.userModel.roleCodes.push(code)
   }
 }

  getList(){
    this.roleService.getRoles().subscribe(val=>{
      this._role$.next(val.value);
    })
  }

  getData(id: string) {
    this.aUser = this.apiService.getUser(id);
    this.aUser.subscribe((user: any) => {
      this.userModel = user.value;
    });
  }

  create() {
    this.userModel = { 
      id: "",
      userName:"",
      fullName: "",
      taxCode:"",
      lastName: "",
      dayOfBirth: "",
      email: "",
      phoneNumber: "",
      address: "",
      roleCodes:[]
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
        this.userModel.id != ""
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
      this.apiService.updateUser(this.userModel.id, this.userModel).subscribe({
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
      this.apiService.createUser(this.userModel).subscribe({
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

    if ( this.userModel.id != "") {
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
