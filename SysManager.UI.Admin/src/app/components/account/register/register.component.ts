import { AccountService } from './../../../services/account-service';
import { AccountView } from './../models/account-view';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html'
})

export class RegisterComponent implements OnInit {

    constructor(private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService
    ) { }

    ngOnInit() {

    }

    createAccount() {

        this.hideMessage();
        var iUserName = (<HTMLInputElement>document.getElementById("username")).value;
        var iEmail = (<HTMLInputElement>document.getElementById("email")).value;
        var iPassword = (<HTMLInputElement>document.getElementById("password")).value;
        var iPasswordConfirm = (<HTMLInputElement>document.getElementById("passwordConfirm")).value;

        if (iUserName == '' || iUserName == undefined) {
            this.showMessage('É necessário informar um usuário, verifique');
            return;
        }

        if (iEmail == '' || iEmail == undefined) {
            this.showMessage('É necessário informar um e-mail, verifique');
            return;
        }

        if (iPassword == '' || iPassword == undefined) {
            this.showMessage('É necessário informar uma senha, verifique');
            return;
        }

        if (iPasswordConfirm == '' || iPasswordConfirm == undefined) {
            this.showMessage('É necessário informar uma senha de confirmação, verifique');
            return;
        }

        if (iPassword != iPasswordConfirm) {
            this.showMessage('Senhas invalidas, verifique');
            return;
        }
        console.log('Tudo certo, vamos preparar para chamar o backEnd');

        const account = new AccountView(iUserName, iEmail, iPassword);

        this.accountService.createAccount(account).subscribe((response: any) => {
            console.log(`OK - ${JSON.stringify(response)}`);
            this.router.navigateByUrl('/login')
        }, error => {
            console.log(`Error - ${error}`);
        }
        )
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