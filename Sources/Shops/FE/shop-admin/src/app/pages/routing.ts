import { Routes } from '@angular/router';

const Routing: Routes = [
  {
    path: 'dashboard',
    loadChildren: () => import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
  },
  {
    path: 'builder',
    loadChildren: () => import('./builder/builder.module').then((m) => m.BuilderModule),
  },
  {
    path: 'crafted/pages/profile',
    loadChildren: () => import('../modules/profile/profile.module').then((m) => m.ProfileModule),
    // data: { layout: 'light-sidebar' },
  },
  {
    path: 'crafted/account',
    loadChildren: () => import('../modules/account/account.module').then((m) => m.AccountModule),
    // data: { layout: 'dark-header' },
  },
  {
    path: 'crafted/pages/wizards',
    loadChildren: () => import('../modules/wizards/wizards.module').then((m) => m.WizardsModule),
    // data: { layout: 'light-header' },
  },
  {
    path: 'crafted/widgets',
    loadChildren: () => import('../modules/widgets-examples/widgets-examples.module').then((m) => m.WidgetsExamplesModule),
    // data: { layout: 'light-header' },
  },
  {
    path: 'apps/chat',
    loadChildren: () => import('../modules/apps/chat/chat.module').then((m) => m.ChatModule),
    // data: { layout: 'light-sidebar' },
  },
  {
    path: 'identities',
    loadChildren: () => import('../modules/Identities/identity.module').then((m) => m.IdentityModule),
  },
  {
    path: 'apps/roles',
    loadChildren: () => import('../modules/Identities/role/role.module').then((m) => m.RoleModule),
  },
  {
    path: 'apps/cities',
    loadChildren: () => import('./city/city.module').then((m) => m.CityModule),
  },
  {
    path: 'apps/districts',
    loadChildren: () => import('./districts/district.module').then((m) => m.DistrictModule),
  },
  {
    path: 'apps/customers',
    loadChildren: () => import('./customers/customer.module').then((m) => m.CustomerModule),
  },
  {
    path: 'apps/wards',
    loadChildren: () => import('./wards/ward.module').then((m) => m.WardModule),
  },
  {
    path: 'apps/villages',
    loadChildren: () => import('./villages/village.module').then((m) => m.VillageModule),
  },
  {
    path: 'apps/permissions',
    loadChildren: () => import('../modules/Identities/permission/permission.module').then((m) => m.PermissionModule),
  },
  {
    path: 'apps/payment',
    loadChildren: () => import('./payments/payment.module').then((m) => m.PaymentModule),
  },
  {
    path: 'catalogs',
    loadChildren: () => import('../modules/catalogs/catalog.module').then((m) => m.CatalogModule),
  },
  {
    path: '',
    redirectTo: '/dashboard',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: 'error/404',
  },
];

export { Routing };
