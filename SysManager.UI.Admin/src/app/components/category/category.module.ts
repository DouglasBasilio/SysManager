import { CustomPaginationModule } from './../pagination/custom-pagination.module';
import { PagerService } from './../../services/page-service';
import { CategoryService } from './../../services/category-service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CategoryMaintenanceComponent } from './category-maintenance/category-maintenance.component';
import { CategoryComponent } from './category.component';
import { CategoryRoutes } from './category.routing';
import { CategoryViewComponent } from './models/category-view-component';
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
        RouterModule.forChild(CategoryRoutes),
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
        CategoryService,
        PagerService,
    ],
    declarations: [
        CategoryComponent,
        CategoryMaintenanceComponent,
        CategoryViewComponent,
    ],
})
export class CategoryModule {
}