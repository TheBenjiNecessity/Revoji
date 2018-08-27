import * as angular from 'angular';

const serviceUrl = "https://localhost:8000/service-api";//TODO needs to be settable by config

export class ApiService {
    constructor (
        private $q:ng.IQService,
        private $http:ng.IHttpService
    ) {}

    get(url:string):ng.IPromise<any> {
        let uri = `${serviceUrl}/${url}`;
        return this.$http.get(uri).then((resp:any) => {
            if (resp.status == 200) {
                return this.$q.resolve(resp.data);
            } else {
                return this.$q.reject(resp);
            }
        });
    }

    post(url:string, body:any):ng.IPromise<any> {
        let uri = `${serviceUrl}/${url}`;
        return this.$http.post(uri, body).then((resp:any) => {
            if (resp.status == 200) {
                return this.$q.resolve(resp.data);
            } else {
                return this.$q.reject(resp);
            }
        });
    }

    del(url:string):ng.IPromise<any> {
        let uri = `${serviceUrl}/${url}`;
        return this.$http.delete(uri).then((resp:any) => {
            if (resp.status == 200) {
                return this.$q.resolve(resp.data);
            } else {
                return this.$q.reject(resp);
            }
        });
    }
}