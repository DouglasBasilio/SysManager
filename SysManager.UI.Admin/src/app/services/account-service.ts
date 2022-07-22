import { environment } from './../../environments/environment';
import { Injectable } from "@angular/core";
import { ServiceBase } from '../service-base/service-base'
import { AccountLoginView } from './../components/account/models/account-login-view';

@Injectable()
export class AccountService extends ServiceBase<AccountLoginView>{
    constructor(){
        super({
            endpoint: `${environment.url_login}`
        })
    }
}