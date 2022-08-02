import { ToastrService } from 'ngx-toastr';
import { PagerService } from './../../services/page-service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UnityFilter } from './models/unity-filter'
import { NgxSpinnerService } from 'ngx-spinner';
import { UnityService } from '../../services/unity-service';

@Component({
    selector: 'app-unity',
    templateUrl: './unity.component.html'
})

export class UnityComponent implements OnInit {
    returnUrl: string = '';
    @Input() bodyDetailTodelete = '';
    public deleteId = '';

    public modalVisible = false;
    public pager: any = {};
    pagedItems: any[] = [];
    firstPage = 1;
    itemsByPage = 10;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private formbuilder: FormBuilder,
        private unityService: UnityService,
        private spinner: NgxSpinnerService,
        private pagerService: PagerService,
        private toastr: ToastrService
    ) {

    }

    formFilter = new FormGroup({
        name: this.formbuilder.control(''),
        active: this.formbuilder.control('todos'),
        itemsByPage: this.formbuilder.control('10'),
    });


    ngOnInit() {
    }

    confirmdelete() {
        if (this.deleteId !== undefined && this.deleteId != '') {
            this.spinner.show();
            this.unityService.delete(this.deleteId).subscribe((response: any) => {
                this.spinner.hide();
                this.toastr.success(response.message, 'Sucesso');
            }, error => {
                this.spinner.hide();
                this.sendAnyMessageErro(this.toastr, error, 'Exclusão de Unidade de Medida')
            });
            this.deleteId == '';
            this.modalVisible = false;
            var filter = new UnityFilter('', 'todos', this.firstPage, this.itemsByPage);
            this.filterView(filter, 1);
            //this.filterView(this.formFilter.value, 1);
        }
    }

    canceldelete() {
        this.modalVisible = false;
    }

    handleChangeModal(event: any) {

    }

    prepareDelete(id: string, name: string) {
        this.deleteId = id;
        this.bodyDetailTodelete = `Deseja mesmo excluir a unidade de medida (${name}) ?`;
        this.modalVisible = true;
    }

    redirectUpdate(url: string, id: string) {
        this.router.navigate([url, id]);
    }
    redirectTo(url: string) {
        this.router.navigate([url]);
    }

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

    sendAnyMessageErro(toastr: ToastrService, messages: any, action: string) {
        var listItems = messages.split('</br>');
        for (let index = 0; index < listItems.length; index++) {
            const element = listItems[index];
            if (element != '' && element != undefined)
                toastr.error(element, action);
        }
    }
}