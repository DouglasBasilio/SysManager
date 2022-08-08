import { RecoveryComponent } from './components/account/recovery/recovery.component';
import { LoginComponent } from './components/account/login/login.component';
import { RegisterComponent } from './components/account/register/register.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DefaultLayoutComponent } from './containers';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    data: {
      title: 'Home'
    },
    children: [
      {
        path: 'account',
        loadChildren: () =>
          import('./components/account/account.module').then((m) => m.AccountModule)
      },
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./components/dashboard/dashboard.module').then((m) => m.DashboardModule)
      },
      {
        path: 'category',
        loadChildren: () =>
          import('./components/category/category.module').then((m) => m.CategoryModule)
      },
      {
        path: 'unity',
        loadChildren: () =>
          import('./components/unity/unity.module').then((m) => m.UnityModule)
      },
      {
        path: 'product-type',
        loadChildren: () =>
          import('./components/product-type/product-type.module').then((m) => m.ProductTypeModule)
      },
      {
        path: 'product',
        loadChildren: () =>
          import('./components/product/product.module').then((m) => m.ProductModule)
      },
    ]
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  {
    path: 'recovery',
    component: RecoveryComponent,
    data: {
      title: 'Recovery Page'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  }

  //{path: '**', redirectTo: 'dashboard'}
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      scrollPositionRestoration: 'top',
      anchorScrolling: 'enabled',
      initialNavigation: 'enabledBlocking'
    })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}