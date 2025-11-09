import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbNavModule, NgbDropdownModule, NgbCollapseModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { SharedModule } from 'src/app/_metronic/shared/shared.module';
import { CrudModule } from 'src/app/modules/crud/crud.module';
import { PermissionListingComponent } from '../permission/permission-listing/permission-listing.component';
import { DistrictListingComponent } from './district-listing/district-listing.component';
import { DistrictSaveComponent } from './district-save/district-save.component';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  declarations: [DistrictListingComponent,DistrictSaveComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {
        path: '',
        component: DistrictListingComponent,
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
export class DistrictModule { }
