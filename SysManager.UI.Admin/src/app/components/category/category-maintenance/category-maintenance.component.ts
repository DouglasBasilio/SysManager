import { CategoryPut } from './../models/category-put';
import { CategoryPost } from './../models/category-post';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from 'src/app/services/category-service';
import { CategoryView } from '../models/category-view';

@Component({
    selector: 'app-category-maintenance',
    templateUrl: './category-maintenance.component.html'
})

export class CategoryMaintenanceComponent implements OnInit {
    returnUrl: string = '/category/category';
    @Input() bodyDetail = ''
    action = 'Inserir';
    @Input() id: any = '';
    idDefault = Guid.EMPTY;

    public modalVisible = false;
    category = new CategoryView();
    constructor(private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private activedRouter: ActivatedRoute,
        private spinner: NgxSpinnerService,
        private categoryService: CategoryService,
        private toastr: ToastrService
    ) { }

    formCategory = new FormGroup({
        id: this.formBuilder.control(this.category.id),
        name: this.formBuilder.control(this.category.name),
        active: this.formBuilder.control(this.category.active)
    });

    ngOnInit() {
        this.id = this.activedRouter.snapshot.params['id'];
        if (this.id != undefined && this.id != this.idDefault && this.id != null) {
            this.action = 'Alterar';
            this.getById(this.id);
        } else {
            this.action = 'Inserir';
            this.category = new CategoryView();
            this.formCategory.patchValue(this.category);
        }
    }

    getById(id: string) {
        this.spinner.show();
        this.categoryService.getByID(id)
            .subscribe(view => {
                this.category = view;
                this.updateForm(this.category);
                this.spinner.hide();
            }, error => {
                this.sendAnyMessageErro(this.toastr, error, this.action)
                this.spinner.hide();
            });
    }

    updateForm(category: CategoryView) {
        this.formCategory = new FormGroup({
            id: this.formBuilder.control(category.id),
            name: this.formBuilder.control(category.name),
            active: this.formBuilder.control(category.active),
        });
    }

    confirmdelete() {
        if (this.category.id !== undefined && this.category.id != '') {
            this.spinner.show();
            this.categoryService.delete(this.category.id).subscribe((response: any) => {
                this.spinner.hide();
                this.toastr.success(response.message, 'Sucesso');
            }, error => {
                this.spinner.hide();
                this.sendAnyMessageErro(this.toastr, error, this.action)
            });
            this.modalVisible = false;
            this.router.navigateByUrl(this.returnUrl)
        }
    }

    canceldelete() {
        this.modalVisible = false;
    }
    prepareDelete() {
        this.bodyDetail = `Deseja mesmo excluir a categoria (${this.category.name}) ?`;
    }

    saveChanges(category: any) {
        if(this.category.id === undefined || this.category.id === ''){
            this.insertCategory(category)
        }
        else {
            this.updateCategory(category)
        }
     }

     insertCategory(category: CategoryView){
        const categoryPost = new CategoryPost(category);
        this.spinner.show();
        this.categoryService.insert(categoryPost).subscribe((response: any) => {
            this.spinner.hide();
            this.toastr.success(response.message, 'Sucesso');
        }, error => {
            this.spinner.hide();
            this.sendAnyMessageErro(this.toastr, error, this.action)
        });
     }

     updateCategory(category: CategoryView){
        const categoryPut = new CategoryPut(category);
        this.spinner.show();
        this.categoryService.update(categoryPut).subscribe((response: any) => {
            this.spinner.hide();
            this.toastr.success(response.message, 'Sucesso');
        }, error => {
            this.spinner.hide();
            this.sendAnyMessageErro(this.toastr, error, this.action)
        });
     }

    sendAnyMessageErro(toastr: ToastrService, messages: any, action: string) {
        var listItems = messages.split('</br>');
        for (let index = 0; index < listItems.length; index++) {
            const element = listItems[index];
            if (element != '' && element != undefined)
                toastr.error(element, action);
        }
    }
}