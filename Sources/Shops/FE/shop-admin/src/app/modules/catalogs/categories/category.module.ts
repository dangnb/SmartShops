import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbNavModule, NgbDropdownModule, NgbCollapseModule, NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { SharedModule } from 'src/app/_metronic/shared/shared.module';
import { CrudModule } from 'src/app/modules/crud/crud.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { CategoryListingComponent } from './category-listing/category-listing.component';
import { CategorySaveComponent } from './category-save/category-save.component';
import { CategoryUploadComponent } from './category-upload/category-upload.component';
import { TreeSelectModule } from 'primeng/treeselect';
import { TreeTableModule } from 'primeng/treetable';


@NgModule({
  declarations: [CategoryListingComponent, CategorySaveComponent, CategoryUploadComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {
        path: '',
        component: CategoryListingComponent,
      }

    ]),
    CrudModule,
    SharedModule,
    NgbNavModule,
    NgbDropdownModule,
    NgbCollapseModule,
    NgbTooltipModule,
    NgxPaginationModule,
    TreeSelectModule,
    TreeTableModule,
    SweetAlert2Module.forChild(),
  ], schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class CategoryModule { }
