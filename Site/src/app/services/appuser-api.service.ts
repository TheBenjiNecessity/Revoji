import * as angular from 'angular';
import { ApiService } from './api.service';
import { AppUser } from '../models/appuser.model';
import { AppUserStats } from '../models/appuser-stats.model';

const url = "service-api/appuser";

export class AppUserService extends ApiService {
    static $inject:string[] = ['$q', '$http', 'localStorageService'];

    constructor (
        $q:ng.IQService,
        $http:ng.IHttpService,
        localStorageService
    ) {
        super($q, $http, localStorageService);
    }

    getAppUserById(id: number):ng.IPromise<AppUser> {
        let uri = `${url}/${id}`;
        return super.get(uri);
    }

    getAppUserByHandle(handle: string):ng.IPromise<AppUser> {
        let uri = `${url}/handle/${handle}`;
        return super.get(uri);
    }

    getAuthUser():ng.IPromise<AppUser> {
        return super.get(url);
    }

    getStats(id: number):ng.IPromise<AppUserStats> {
        let uri = `${url}/counts/${id}`;
        return super.get(uri);
    }

    create(user: AppUser):ng.IPromise<AppUser> {
        let uri = `${url}`;
        return super.post(uri, user);
    }

    update(id: number, user: AppUser):ng.IPromise<AppUser> {
        let uri = `${url}/${id}`;
        return super.post(uri, user);
    }

    delete(id: number):ng.IPromise<any> {
        let uri = `${url}/${id}`;
        return super.del(uri);
    }
}

angular.module('revoji').service('appUserService', AppUserService);