import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbNavModule, NgbDropdownModule, NgbCollapseModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { SharedModule } from 'src/app/_metronic/shared/shared.module';
import { CrudModule } from 'src/app/modules/crud/crud.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { WarehouseListingComponent } from './warehouse-listing/warehouse-listing.component';
import { WarehouseSaveComponent } from './warehouse-save/warehouse-save.component';



@NgModule({
  declarations: [WarehouseListingComponent, WarehouseSaveComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {
        path: '',
        component: WarehouseListingComponent,
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
export class WarehouseModule { }