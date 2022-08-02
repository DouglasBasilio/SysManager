import { CustomPaginationModule } from './../pagination/custom-pagination.module';
import { PagerService } from './../../services/page-service';
import { UnityService } from './../../services/unity-service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UnityMaintenanceComponent } from './unity-maintenance/unity-maintenance.component';
import { UnityComponent } from './unity.component';
import { UnityRoutes } from './unity.routing';
import { UnityViewComponent } from './models/unity-view-component';
import {
    ButtonGroupModule,
    ButtonModule,
    CardModule,
    DropdownModule,
    FormModule,
    GridModule,
    ListGroupModule,
    PaginationModule,
    SharedModule,
    TableModule,
    ModalModule,
    ToastModule,
  } from '@coreui/angular';
  import { IconModule } from '@coreui/icons-angular';
  //import { BarNavigatorModule } from '../bar-navigator/bar-navigator.module';
   import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
    imports: [
        RouterModule.forChild(UnityRoutes),
        CommonModule,
        FormModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        ButtonGroupModule,
        ButtonModule,
        SharedModule,
        CardModule,
        GridModule,
        ListGroupModule,
        DropdownModule,
        IconModule,
        TableModule,
        NgxSpinnerModule,
        PaginationModule,
        //BarNavigatorModule,
        ModalModule,
        ToastModule,
        CustomPaginationModule
    ],
    providers: [
        UnityService,
        PagerService,
    ],
    declarations: [
        UnityComponent,
        UnityMaintenanceComponent,
        UnityViewComponent,
    ],
})
export class UnityModule {
}