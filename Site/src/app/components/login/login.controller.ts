import * as angular from 'angular';

import { LoginService } from '../../services/login-api.service';

class loginModel {
    handle: string;
    password: string;
}

export class LoginController {
    static $inject: string[] = ['$window', 'loginService'];

    model: loginModel;

    constructor(private $window:ng.IWindowService, private loginService:LoginService) {}

    login() {
        this.loginService.login(this.model.handle, this.model.password).then(resp => {
            let url = `/user?handle=${this.model.handle}`;
            this.$window.location.href = url;
        }).catch(error => {
            console.log(error);
        });
    }
}

angular.module('revoji')
   .controller('LoginController', LoginController);