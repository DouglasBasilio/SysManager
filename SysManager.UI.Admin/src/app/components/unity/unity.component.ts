import { PagerService } from './../../services/page-service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { UnityFilter } from './models/unity-filter'
import { NgxSpinnerService } from 'ngx-spinner';
import { UnityService } from '../../services/unity-service';
import { Utils } from './../../Utils/utils';

@Component({
    selector: 'app-unity',
    templateUrl: './unity.component.html'
})

export class UnityComponent implements OnInit {
    @Input() modalTitle = '';
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
        private unityService: UnityService,
        private spinner: NgxSpinnerService,
        private pagerService: PagerService,
        private utils: Utils
    ) {

    }

    formFilter = new FormGroup({
        name: this.formbuilder.control(''),
        active: this.formbuilder.control('todos'),
        itemsByPage: this.formbuilder.control('10'),
    });


    ngOnInit() { }

    filterView(filter: UnityFilter, page: number) {
        let _filter = new UnityFilter(filter.name, filter.active, page, filter.itemsByPage)
        this.spinner.show();
        this.unityService.getByFilter(_filter).subscribe(view => {
            this.pagedItems = view.items;
            this.pager = this.pagerService.getPager(view._total, page, view._pageSize);
            this.spinner.hide();
        }, error => {
            this.spinner.hide();
        });
    }

    prepareDelete(id: string, name: string) {
        this.deleteId = id;
        this.modalTitle = 'Excluir Unidade de Medida'
        this.modalBodyDetail = `Deseja mesmo excluir a unidade de medida (${name}) ?`;
        this.setModalVisible = true;
    }

    confirmdelete() {
        if (this.deleteId !== undefined && this.deleteId != '') {
            this.spinner.show();
            this.unityService.delete(this.deleteId).subscribe((response: any) => {
                this.spinner.hide();
                this.utils.showSuccessMessage(response.message, 'Sucesso');
                this.filterView(this.formFilter.value, 1);
            }, error => {
                this.spinner.hide();
                this.utils.showErrorMessage(error, 'Exclusão de Unidade de Medida')
            });
            this.deleteId == '';
            this.setModalVisible = false;
        }
    }

    canceldelete() {
        this.setModalVisible = false;
        this.deleteId = '';
    }

    handleChangeModal(event: any) { }

    redirectUpdate(url: string, id: string) {
        this.router.navigate([url, id]);
    }

    redirectTo(url: string) {
        this.router.navigate([url]);
    }
}