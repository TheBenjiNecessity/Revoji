import * as angular from 'angular';

import {ApiService} from './api.service';
import {Reviewable} from '../models/reviewable.model';

const url = "reviewable";

export class ReviewableService extends ApiService {
    static $inject:string[] = ['$q', '$http'];

    constructor (
        $q:ng.IQService,
        $http:ng.IHttpService
    ) {
        super($q, $http);
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