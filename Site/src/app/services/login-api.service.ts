import * as angular from 'angular';
import { ApiService } from './api.service';

export class LoginService extends ApiService {
    static $inject:string[] = ['$q', '$http'];

    constructor (
        $q:ng.IQService,
        $http:ng.IHttpService
    ) {
        super($q, $http);
    }

    login(username: string, password: string) {
        
    }

    logout() {
        
    }
}

angular.module('revoji').service('loginService', LoginService);