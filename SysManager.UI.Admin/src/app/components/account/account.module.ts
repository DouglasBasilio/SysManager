import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register/register.component';
import { ButtonModule, CardModule, FormModule, GridModule, ToastModule } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { AccountRoutingModule } from './account-routing-module';


@NgModule({
    declarations:[
        RegisterComponent,
    ],
    imports:[
        CommonModule, 
        AccountRoutingModule,
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