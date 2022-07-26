import { AccountToken } from './../models/account-token-view';
import { AccountLoginView } from './../models/account-login-view';
import { AccountService } from './../../../services/account-service';
import { AccountView } from './../models/account-view';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html'
})

export class LoginComponent implements OnInit {

    returnUrl: string = ''

    constructor(private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService
    ) { }

    ngOnInit() {

    }

    login(){
        this.hideMessage();
        var iUserName = (<HTMLInputElement>document.getElementById("username")).value;
        var iPassword = (<HTMLInputElement>document.getElementById("password")).value;

        // contrato
        const user = new AccountLoginView(iUserName, iPassword);

        if (iUserName == '' || iUserName == undefined) {
            this.showMessage('É necessário informar um usuário, verifique');
            return;
        }

        if (iPassword == '' || iPassword == undefined) {
            this.showMessage('É necessário informar uma senha, verifique');
            return;
        }

        this.accountService.login(user).subscribe((response: any) => {

            const userToken = new AccountToken()
            userToken.email = user.email
            userToken.password = user.password
            userToken.token = response.token
            localStorage.setItem('currentUser', JSON.stringify(userToken))

            console.log(`Login efetuado com sucesso - ${JSON.stringify(response)}`);

        }, error => {
            console.log(`Erro ao efetuar login - ${error}`);
            this.showMessage('Login ou senha inválidos')
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