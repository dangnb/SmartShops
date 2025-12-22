import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InlineSVGModule } from 'ng-inline-svg-2';
import { RouterModule, Routes } from '@angular/router';
import {
    NgbDropdownModule,
    NgbProgressbarModule,
    NgbTooltipModule,
} from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core';
import { TranslationModule } from '../../modules/i18n';
import { Routing } from '../../pages/routing';
import { SharedModule } from 'primeng/api';
import { ThemeModeModule } from 'src/app/_metronic/partials/layout/theme-mode-switcher/theme-mode.module';
import { CatalogComponent } from './catalog.component';
import { CatalogRouting } from './catalog.routing';

const routes: Routes = [
    {
        path: '',
        component: CatalogComponent,
        children: CatalogRouting,
    },
];

@NgModule({
    declarations: [
        CatalogComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild(routes),
        TranslationModule,
        InlineSVGModule,
        NgbDropdownModule,
        NgbProgressbarModule,
        NgbTooltipModule,
        TranslateModule,
        ThemeModeModule,
        SharedModule
    ],
    exports: [RouterModule],
})
export class CatalogModule { }
