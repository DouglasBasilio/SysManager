import { Utils } from './../../Utils/utils';
import { ToastrService } from 'ngx-toastr';
import { PagerService } from './../../services/page-service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryFilter } from './models/category-filter'
import { CategoryService } from 'src/app/services/category-service'
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
    selector: 'app-category',
    templateUrl: './category.component.html'
})

export class CategoryComponent implements OnInit {
    @Input() modalTitle = ''
    @Input() modalBodyDetail = '';
    public deleteId = '';
    public setModalVisible = false;
    public pager: any = {};
    pagedItems: any[] = [];
    firstPage = 1;
    itemsByPage = 10;
    returnUrl: string = '';

    constructor(
        private router: Router,
        private formbuilder: FormBuilder,
        private categoryService: CategoryService,
        private spinner: NgxSpinnerService,
        private pagerService: PagerService,
        private toastr: ToastrService,
        private utils: Utils
    ) { }

    formFilter = new FormGroup({
        name: this.formbuilder.control(''),
        active: this.formbuilder.control('todos'),
        itemsByPage: this.formbuilder.control('10'),
    });


    ngOnInit() { }

    filterView(filter: CategoryFilter, page: number) {
        let _filter = new CategoryFilter(filter.name, filter.active, page, filter.itemsByPage)
        this.spinner.show();
        this.categoryService.getByFilter(_filter).subscribe(view => {
            this.pagedItems = view.items;
            this.pager = this.pagerService.getPager(view._total, page, view._pageSize);
            this.spinner.hide();
        }, error => {
            this.spinner.hide();
        });
    }

    prepareDelete(id: string, name: string) {
        this.deleteId = id;
        this.modalTitle = 'Excluir categoria';
        this.modalBodyDetail = `Deseja mesmo excluir o registro (${name}) ?`;
        this.setModalVisible = true;
    }

    confirmdelete() {
        if (this.deleteId !== undefined && this.deleteId != '') {
            this.spinner.show();
            this.categoryService.delete(this.deleteId).subscribe((response: any) => {
                this.spinner.hide();
                this.utils.showSuccessMessage(response.message, 'Sucesso');
                this.filterView(this.formFilter.value, 1);
            }, error => {
                this.spinner.hide();
                this.utils.showErrorMessage(error, 'Exclusão de Categoria');
            });
            this.setModalVisible = false;
            this.deleteId == '';
        }
    }

    canceldelete() {
        this.setModalVisible = false;
        this.deleteId = '';
    }

    handleChangeModal(event: any) {

    }

    redirectUpdate(url: string, id: string) {
        this.router.navigate([url, id]);
    }

    redirectTo(url: string) {
        this.router.navigate([url]);
    }

}