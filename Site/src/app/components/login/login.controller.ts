import * as angular from 'angular';

import { SessionService } from '../../services/session.service';

class loginModel {
    handle: string;
    password: string;
}

export class LoginController {
    static $inject: string[] = ['$location', 'sessionService'];

    model: loginModel;

    constructor(private $location:ng.ILocationService, private sessionService:SessionService) {}

    login() {
        this.sessionService.login(this.model.handle, this.model.password).then(resp => {
            let url = `user`;
            this.$location.path(url);
        }).catch(error => {
            console.log(error);
        });
    }
}

angular.module('revoji')
   .controller('LoginController', LoginController);