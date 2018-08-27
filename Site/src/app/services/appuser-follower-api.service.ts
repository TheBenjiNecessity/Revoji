import * as angular from 'angular';
import { ApiService } from './api.service';
import { AppUser } from '../models/appuser.model';
import { AppUserFollowing } from '../models/appuser-follower.model';

const url = "appuser";

export class AppUserFollowerService extends ApiService {
    static $inject:string[] = ['$q', '$http'];

    constructor (
        $q:ng.IQService,
        $http:ng.IHttpService
    ) {
        super($q, $http);
    }

    listFollowers(id: number,
                  order: string = "DESC",
                  pageStart: number = 0,
                  pageLimit: number = 20):ng.IPromise<AppUser[]> {
        let uri = `${url}/followers/${id}?order=${order}&pageStart=${pageStart}&pageLimit=${pageLimit}`;
        return super.get(uri);
    }

    listFollowings(id: number,
                   order: string = "DESC",
                   pageStart: number = 0,
                   pageLimit: number = 20):ng.IPromise<AppUser[]> {
        let uri = `${url}/followings/${id}?order=${order}&pageStart=${pageStart}&pageLimit=${pageLimit}`;
        return super.get(uri);
    }

    addFollowing(following: AppUserFollowing):ng.IPromise<AppUser> {
        let uri = `${url}/follower`;
        return super.post(uri, following);
    }

    removeFollowing(id: number, followingId: number):ng.IPromise<AppUser> {
        let uri = `${url}/follower/${id}?followingId=${followingId}`;
        return super.del(uri);
    }
}

angular.module('revoji').service('appUserFollowerService', AppUserFollowerService);