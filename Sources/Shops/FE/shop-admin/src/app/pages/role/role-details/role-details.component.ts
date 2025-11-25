import { ChangeDetectorRef, Component, EventEmitter, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import moment from 'moment';
import { IPermissionModel, PermisionService } from 'src/app/_services/permision.service';
import { IRoleModel, RoleService } from 'src/app/_services/role.service';
import { SweetAlertOptions } from 'sweetalert2';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.component.scss']
})
export class RoleDetailsComponent implements OnInit {
  roleModel: IRoleModel = { id: '', name: '', description: '',  permissionCodes: [] };
  role$: Observable<IRoleModel>;
  isLoading= false;
  private _permisions$= new BehaviorSubject<IPermissionModel[]>([]);
  public permissions$ = this._permisions$.asObservable();

  // Reload emitter inside datatable
  reloadEvent: EventEmitter<boolean> = new EventEmitter();
  
  @ViewChild('formModal')
  formModal: TemplateRef<any>;

  @ViewChild('noticeSwal')
  noticeSwal!: SwalComponent;

  swalOptions: SweetAlertOptions = {};

  modalConfig: NgbModalOptions = {
    modalDialogClass: 'modal-dialog modal-dialog-centered mw-650px',
  };
  constructor(private route: ActivatedRoute, private cdr: ChangeDetectorRef, 
    private apiService: RoleService, private permissionService: PermisionService,
    private modalService: NgbModal
  ) { }

  ngOnInit(): void {
    this.permissionService.getList().subscribe(val=>{
      this._permisions$.next(val.value);
    })
    this.route.params.subscribe(params => {
      const id = params['id'];
       this.apiService.getRole(id).subscribe(val=>{
        this.roleModel=val.value;
        this.cdr.detectChanges();
      });
    });
  }

  checkSelectId(code:string){
     return this.roleModel.permissionCodes && this.roleModel.permissionCodes.includes(code);
  }

  selectPermision(code:string){
    if(!this.roleModel.permissionCodes) 
      this.roleModel.permissionCodes=[];
    if(this.roleModel.permissionCodes.includes(code)){
      this.roleModel.permissionCodes= this.roleModel.permissionCodes.filter(x=>x != code);
    }else{
      this.roleModel.permissionCodes.push(code)
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
      text: this.roleModel.id !=""  ? 'User updated successfully!' : 'User created successfully!',
    };
    const errorAlert: SweetAlertOptions = {
      icon: 'error',
      title: 'Error!',
      text: '',
    };

    const completeFn = () => {
      this.isLoading = false;
      this.modalService.dismissAll();
      this.cdr.detectChanges();
    };

    const updateFn = () => {
      this.apiService.updateRole(this.roleModel.id, this.roleModel).subscribe({
        next: () => {
          this.showAlert(successAlert);
          this.reloadEvent.emit(true);
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
      this.apiService.createRole(this.roleModel).subscribe({
        next: () => {
          this.showAlert(successAlert);
          this.reloadEvent.emit(true);
        },
        error: (error) => {
          errorAlert.text = this.extractText(error.error);
          this.showAlert(errorAlert);
          this.isLoading = false;
        },
        complete: completeFn,
      });
    };

    if (this.roleModel.id != "") {
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
    this.swalOptions = Object.assign({
      buttonsStyling: false,
      confirmButtonText: "Ok, got it!",
      customClass: {
        confirmButton: "btn btn-" + style
      }
    }, swalOptions);
    this.cdr.detectChanges();
    this.noticeSwal.fire();
  }
}
