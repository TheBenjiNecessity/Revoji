import * as angular from 'angular';

export class LoginController {
    testing:string;

    constructor() {
        this.testing = 'this is a test';
    }
}

angular.module('revoji')
   .controller('LoginController', LoginController);