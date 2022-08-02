import { UnityPut } from './../models/unity-put';
import { UnityPost } from './../models/unity-post';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { UnityView } from '../models/unity-view';
import { UnityService } from '../../../services/unity-service';

@Component({
    selector: 'app-unity-maintenance',
    templateUrl: './unity-maintenance.component.html'
})

export class UnityMaintenanceComponent implements OnInit {
    returnUrl: string = '/unity/unity';
    @Input() bodyDetail = ''
    action = 'Inserir';
    @Input() id: any = '';
    idDefault = Guid.EMPTY;

    public modalVisible = false;
    unity = new UnityView();
    constructor(private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private activedRouter: ActivatedRoute,
        private spinner: NgxSpinnerService,
        private unityService: UnityService,
        private toastr: ToastrService
    ) { }

    formUnity = new FormGroup({
        id: this.formBuilder.control(this.unity.id),
        name: this.formBuilder.control(this.unity.name),
        active: this.formBuilder.control(this.unity.active)
    });

    ngOnInit() {
        this.id = this.activedRouter.snapshot.params['id'];
        if (this.id != undefined && this.id != this.idDefault && this.id != null) {
            this.action = 'Alterar';
            this.getById(this.id);
        } else {
            this.action = 'Inserir';
            this.unity = new UnityView();
            this.formUnity.patchValue(this.unity);
        }
    }

    getById(id: string) {
        this.spinner.show();
        this.unityService.getByID(id)
            .subscribe(view => {
                this.unity = view;
                this.updateForm(this.unity);
                this.spinner.hide();
            }, error => {
                this.sendAnyMessageErro(this.toastr, error, this.action)
                this.spinner.hide();
            });
    }

    updateForm(unity: UnityView) {
        this.formUnity = new FormGroup({
            id: this.formBuilder.control(unity.id),
            name: this.formBuilder.control(unity.name),
            active: this.formBuilder.control(unity.active),
        });
    }

    confirmdelete() {
        if (this.unity.id !== undefined && this.unity.id != '') {
            this.spinner.show();
            this.unityService.delete(this.unity.id).subscribe((response: any) => {
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
        this.bodyDetail = `Deseja mesmo excluir a unidade de medida (${this.unity.name}) ?`;
    }

    saveChanges(unity: any) {
        if(this.unity.id === undefined || this.unity.id === ''){
            this.insertUnity(unity)
        }
        else {
            this.updateUnity(unity)
        }
     }

     insertUnity(unity: UnityView){
        const unityPost = new UnityPost(unity);
        this.spinner.show();
        this.unityService.insert(unityPost).subscribe((response: any) => {
            this.spinner.hide();
            this.toastr.success(response.message, 'Sucesso');
        }, error => {
            this.spinner.hide();
            this.sendAnyMessageErro(this.toastr, error, this.action)
        });
     }

     updateUnity(unity: UnityView){
        const unityPut = new UnityPut(unity);
        this.spinner.show();
        this.unityService.update(unityPut).subscribe((response: any) => {
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