import { Routes } from '@angular/router';

const CatalogRouting: Routes = [
    {
        path: 'suppliers',
        loadChildren: () => import('./suppliers/supplier.module').then((m) => m.SupplierModule),
    },
    {
        path: 'categories',
        loadChildren: () => import('./categories/category.module').then((m) => m.CategoryModule),
    },
    {
        path: 'products',
        loadChildren: () => import('./products/product.module').then((m) => m.ProductModule),
    },
];

export { CatalogRouting };
