import { Routes } from '@angular/router';

const IdentityRouting: Routes = [
    {
        path: 'permissions',
        loadChildren: () => import('./permission/permission.module').then((m) => m.PermissionModule),
    },
    {
        path: 'roles',
        loadChildren: () => import('./role/role.module').then((m) => m.RoleModule),
    },
    {
        path: 'users',
        loadChildren: () => import('./user/user.module').then((m) => m.UserModule),
    },
];

export { IdentityRouting };
