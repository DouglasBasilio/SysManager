import { ProductTypePut } from '../models/product-type-put';
import { ProductTypePost } from '../models/product-type-post';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ProductTypeView } from '../models/product-type-view';
import { ProductTypeService } from '../../../services/product-type-service';

@Component({
    selector: 'app-product-type-maintenance',
    templateUrl: './product-type-maintenance.component.html'
})

export class ProductTypeMaintenanceComponent implements OnInit {
    returnUrl: string = '/product-type/product-type';
    @Input() bodyDetail = ''
    action = 'Inserir';
    @Input() id: any = '';
    idDefault = Guid.EMPTY;

    public modalVisible = false;
    productType = new ProductTypeView();
    constructor(private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private activedRouter: ActivatedRoute,
        private spinner: NgxSpinnerService,
        private productTypeService: ProductTypeService,
        private toastr: ToastrService
    ) { }

    formProductType = new FormGroup({
        id: this.formBuilder.control(this.productType.id),
        name: this.formBuilder.control(this.productType.name),
        active: this.formBuilder.control(this.productType.active)
    });

    ngOnInit() {
        this.id = this.activedRouter.snapshot.params['id'];
        if (this.id != undefined && this.id != this.idDefault && this.id != null) {
            this.action = 'Alterar';
            this.getById(this.id);
        } else {
            this.action = 'Inserir';
            this.productType = new ProductTypeView();
            this.formProductType.patchValue(this.productType);
        }
    }

    getById(id: string) {
        this.spinner.show();
        this.productTypeService.getByID(id)
            .subscribe(view => {
                this.productType = view;
                this.updateForm(this.productType);
                this.spinner.hide();
            }, error => {
                this.sendAnyMessageErro(this.toastr, error, this.action)
                this.spinner.hide();
            });
    }

    updateForm(productType: ProductTypeView) {
        this.formProductType = new FormGroup({
            id: this.formBuilder.control(productType.id),
            name: this.formBuilder.control(productType.name),
            active: this.formBuilder.control(productType.active),
        });
    }

    confirmdelete() {
        if (this.productType.id !== undefined && this.productType.id != '') {
            this.spinner.show();
            this.productTypeService.delete(this.productType.id).subscribe((response: any) => {
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
        this.bodyDetail = `Deseja mesmo excluir o tipo de produto (${this.productType.name}) ?`;
    }

    saveChanges(productType: any) {
        if(this.productType.id === undefined || this.productType.id === ''){
            this.insertProductType(productType)
        }
        else {
            this.updateProductType(productType)
        }
     }

     insertProductType(productType: ProductTypeView){
        const productTypePost = new ProductTypePost(productType);
        this.spinner.show();
        this.productTypeService.insert(productTypePost).subscribe((response: any) => {
            this.spinner.hide();
            this.toastr.success(response.message, 'Sucesso');
        }, error => {
            this.spinner.hide();
            this.sendAnyMessageErro(this.toastr, error, this.action)
        });
     }

     updateProductType(productType: ProductTypeView){
        const productTypePut = new ProductTypePut(productType);
        this.spinner.show();
        this.productTypeService.update(productTypePut).subscribe((response: any) => {
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