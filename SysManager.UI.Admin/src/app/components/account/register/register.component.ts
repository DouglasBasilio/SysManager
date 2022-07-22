import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html'
})

export class RegisterComponent implements OnInit {

    constructor(private route: ActivatedRoute,
        private router: Router) { }

    ngOnInit() {

    }

    createAccount() {

        this.hideMessage();
        var iUserName = (<HTMLInputElement>document.getElementById("username")).value;
        var iEmail = (<HTMLInputElement>document.getElementById("email")).value;
        var iPassword = (<HTMLInputElement>document.getElementById("password")).value;
        var iPasswordConfirm = (<HTMLInputElement>document.getElementById("passwordConfirm")).value;

        if (iPassword != iPasswordConfirm) {
            this.showMessage('Senhas invalidas, verifique');
            return;
        }
        console.log('passei por aqui!');

    }

    showMessage(value: string) {
        const colErrors = document.getElementById("colerror")!;
        var idvAlert = (<HTMLDivElement>document.getElementById("dvAlert"));
        idvAlert.innerHTML = value;
        colErrors.style.display = '';
    }

    hideMessage() {
        const colErrors = document.getElementById("colerror")!;
        var idvAlert = (<HTMLDivElement>document.getElementById("dvAlert"));
        idvAlert.innerHTML = '';
        colErrors.style.display = 'none';
    }

}