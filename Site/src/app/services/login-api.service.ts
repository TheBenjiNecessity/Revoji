import * as angular from 'angular';
import * as $ from 'jquery';

import { ApiService } from './api.service';

export class LoginService extends ApiService {
    static $inject:string[] = ['$q', '$http', 'localStorageService'];

    constructor (
        $q:ng.IQService,
        $http:ng.IHttpService,
        localStorageService
    ) {
        super($q, $http, localStorageService);
    }

    login(username: string, password: string) {
        let data = {
            client_id: 'com.revoji',
            client_secret: 'secret',//TODO: change secret
            grant_type: 'password',
            username: username,
            password: password
        };

        let uri = 'http://localhost:5001/connect/token';

        return this.$http({
            method: 'POST',
            url: uri,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            data: $.param(data)
        }).then(resp => {
            this.localStorageService.set('auth.token', (resp.data as any).access_token);//TODO auth.token?
            return resp;
        }, err => {
            console.log(err);
            return err;
        });
    }

    logout() {
        //TODO: reset other session stuff
        this.localStorageService.set('auth.token', null);
    }
}

angular.module('revoji').service('loginService', LoginService);