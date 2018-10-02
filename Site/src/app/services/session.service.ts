import * as angular from 'angular';
import * as $ from 'jquery';

import { ApiService } from './api.service';
import { AppUserService } from './appuser-api.service';
import { AppUser } from '../models/appuser.model';
 
export class SessionService extends ApiService {
    static $inject:string[] = ['$q', '$http', 'localStorageService', 'appUserService'];

    constructor (
        $q:ng.IQService,
        $http:ng.IHttpService,
        localStorageService,
        private appUserService:AppUserService
    ) {
        super($q, $http, localStorageService);
        //this.logout();
    }

    get token() {
        return this.localStorageService.get('auth.token');//TODO auth.token?
    }

    set token(token: string) {
        if (token == null) {
            this.localStorageService.remove('auth.token');
        } else {
            this.localStorageService.set('auth.token', token);
        }
    }

    get user() {
        return this.localStorageService.get('auth.user');
    }

    set user(user: AppUser) {
        if (user == null) {
            this.localStorageService.remove('auth.token');
        } else {
            this.localStorageService.set('auth.user', user);
        }
    }

    get isLoggedIn(): boolean {
        return this.user != null && this.token != null;
    }

    login(username: string, password: string) {
        let data = {
            client_id: 'com.revoji',
            client_secret: 'secret',//TODO: change secret
            grant_type: 'password',
            username: username,
            password: password
        };

        let uri = 'http://localhost:5001/connect/token';//TODO: use config

        return this.$http({
            method: 'POST',
            url: uri,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            data: $.param(data)
        }).then(resp => {
            this.token = (resp.data as any).access_token;

            return this.appUserService.getAuthUser().then(resp => {
                this.user = resp;
            });
        }).catch(err => {
            console.log(err);
            return err;
        });
    }

    logout() {
        //TODO: reset other session stuff
        this.user = null;
        this.token = null;
    }
}

angular.module('revoji').service('sessionService', SessionService);