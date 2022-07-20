import { RegisterComponent } from './register/register.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';


const routes: Routes = [
    {
        path: 'register',
        component: RegisterComponent,
        data: {
            title: 'register page'
        }
    }
];

@NgModule({
    imports : [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class AccountRoutingModule {

}