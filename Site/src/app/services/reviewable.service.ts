import * as angular from 'angular';

import {ApiService} from './api.service';
import {Reviewable} from '../models/reviewable.model';

const url = "service-api/reviewable";

export class ReviewableService extends ApiService {
    static $inject:string[] = ['$q', '$http', 'localStorageService'];

    constructor (
        $q:ng.IQService,
        $http:ng.IHttpService,
        localStorageService
    ) {
        super($q, $http, localStorageService);
    }

    /* ================================ CRUD ================================ */
    getReviewable(id: number):ng.IPromise<Reviewable> {
        let uri = `${url}/${id}`;
        return super.get(uri);
    }

    /* ================================ Stats ================================ */
    getReviewCount(id: number):ng.IPromise<number> {
        let uri = `${url}/stats/reviewcount/${id}`;
        return super.get(uri);
    }

    getEmojiStats(id: number):ng.IPromise<{[emoji: string]: number}> {
        let uri = `${url}/stats/emojistats/${id}`;
        return super.get(uri);
    }
}

angular.module('revoji').service('reviewableService', ReviewableService);