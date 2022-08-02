import { ProductTypeViewComponent } from './models/product-type-view-component';
import { ProductTypeMaintenanceComponent } from './product-type-maintenance/product-type-maintenance.component';
import { ProductTypeComponent } from './product-type.component';
import { ProductTypeService } from './../../services/product-type-service';
import { CustomPaginationModule } from './../pagination/custom-pagination.module';
import { PagerService } from './../../services/page-service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProductTypeRoutes } from './product-type.routing';
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
        RouterModule.forChild(ProductTypeRoutes),
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
        ProductTypeService,
        PagerService,
    ],
    declarations: [
        ProductTypeComponent,
        ProductTypeMaintenanceComponent,
        ProductTypeViewComponent,
    ],
})
export class ProductTypeModule {
}