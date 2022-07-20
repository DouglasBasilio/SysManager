export class AccountPutRequest {
    username: string = '';
    email: string = '';
    newpassword: string = '';

    constructor(_username: string, _email: string, _newpassword: string) {
        this.username = _username;
        this.email = _email;
        this.newpassword = _newpassword;
    };
}