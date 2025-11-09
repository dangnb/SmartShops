import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbNavModule, NgbDropdownModule, NgbCollapseModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { SharedModule } from 'src/app/_metronic/shared/shared.module';
import { CrudModule } from 'src/app/modules/crud/crud.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { CustomerListingComponent } from './customer-listing/customer-listing.component';
import { CustomerSaveComponent } from './customer-save/customer-save.component';
import { CustomerUploadComponent } from './customer-upload/customer-upload.component';


@NgModule({
  declarations: [CustomerListingComponent,CustomerSaveComponent, CustomerUploadComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {
        path: '',
        component: CustomerListingComponent,
      }
      
    ]),
    CrudModule,
    SharedModule,
    NgbNavModule,
    NgbDropdownModule,
    NgbCollapseModule,
    NgbTooltipModule,
    NgxPaginationModule,
    SweetAlert2Module.forChild(),
  ]
})
export class CustomerModule { }
