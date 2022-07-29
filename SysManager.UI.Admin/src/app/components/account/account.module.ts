import { RecoveryComponent } from './recovery/recovery.component';
import { AccountRoutes } from './account.routing';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { ButtonModule, CardModule, FormModule, GridModule, ToastModule } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';

@NgModule({
    declarations:[
        RegisterComponent,
        LoginComponent,
        RecoveryComponent,
    ],
    imports:[
        CommonModule,
        RouterModule.forChild(AccountRoutes),
        CardModule,
        ButtonModule,
        ToastModule,
        GridModule,
        IconModule,
        FormModule,
    ]
})

export class AccountModule {

}