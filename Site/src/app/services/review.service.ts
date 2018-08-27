import * as angular from 'angular';

import { ApiService } from './api.service';
import { Review } from '../models/review.model';
import { ReviewLike } from '../models/review-like.model';

const url = "reviews";

export class ReviewService extends ApiService {
    static $inject:string[] = ['$q', '$http'];

    constructor (
        $q:ng.IQService,
        $http:ng.IHttpService
    ) {
        super($q, $http);
    }

    /* ================================ CRUD ================================ */
    getReview(appUserId: number, reviewableId: number):ng.IPromise<Review> {
        let uri = `${url}/${appUserId}?reviewableId=${reviewableId}`;
        return super.get(uri);
    }

    create(review: Review):ng.IPromise<Review> {
        let uri = `${url}`;
        return super.post(uri, review);
    }

    update(id: number, review: Review):ng.IPromise<Review> {
        let uri = `${url}/${id}`;
        return super.post(uri, review);
    }

    delete(id: number):ng.IPromise<any> {
        let uri = `${url}/${id}`;
        return super.del(uri);
    }

    /* ================================ List Reviews ================================ */
    listReviewsByUser(id: number,
                      order: string = "DESC",
                      pageStart: number = 0,
                      pageLimit: number = 20):ng.IPromise<Review[]> {
        let uri = `${url}/list/user/${id}?order=${order}&pageStart=${pageStart}&pageLimit=${pageLimit}`;
        return super.get(uri);
    }

    listReviewsByReviewable(id: number,
                            order: string = "DESC",
                            pageStart: number = 0,
                            pageLimit: number = 20):ng.IPromise<Review[]> {
        let uri = `${url}/list/reviewable/${id}?order=${order}&pageStart=${pageStart}&pageLimit=${pageLimit}`;
        return super.get(uri);
    }

    listReviewsByFollowings(id: number,
                            order: string = "DESC",
                            pageStart: number = 0,
                            pageLimit: number = 20):ng.IPromise<Review[]> {
        let uri = `${url}/list/followings/${id}?order=${order}&pageStart=${pageStart}&pageLimit=${pageLimit}`;
        return super.get(uri);
    }

    /* ================================ Like Reviews ================================ */
    like(like: ReviewLike):ng.IPromise<ReviewLike> {
        let uri = `${url}/like`;
        return super.post(uri, like);
    }

    removeLike(reviewId: number, appUserId: number):ng.IPromise<ReviewLike> {
        let uri = `${url}/like/${reviewId}?appUserId=${appUserId}`;
        return super.del(uri);
    }
}

angular.module('revoji').service('reviewService', ReviewService);