import { Routes } from '@angular/router';
import { GoodsReceiptListingComponent } from './GoodsReceipts/goodsreceipt-listing/goodsreceipt-listing.component';

const InventoryRouting: Routes = [
    {
        path: 'goods-receipts',
        loadComponent: () => import('./GoodsReceipts/goodsreceipt-listing/goodsreceipt-listing.component').then(m => m.GoodsReceiptListingComponent),
    },

];

export { InventoryRouting };
