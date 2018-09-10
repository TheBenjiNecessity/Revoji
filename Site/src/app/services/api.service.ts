import * as angular from 'angular';

const serviceUrl = "https://localhost:8000";//TODO needs to be settable by config

export class ApiService {
    config: any;

    constructor (
        protected $q:ng.IQService,
        protected $http:ng.IHttpService,
        protected localStorageService
    ) {
        let token = this.localStorageService.get('auth.token');
        this.config = { headers: { Authorization: null } };
        if (token) {
            this.config.headers.Authorization = 'Bearer ' + token;
        }
    }

    get(url:string):ng.IPromise<any> {
        let uri = `${serviceUrl}/${url}`;
        return this.$http.get(uri, this.config).then((resp:any) => {
            if (resp.status == 200) {
                return this.$q.resolve(resp.data);
            } else {
                return this.$q.reject(resp);
            }
        });
    }

    post(url:string, body:any):ng.IPromise<any> {
        let uri = `${serviceUrl}/${url}`;
        return this.$http.post(uri, body, this.config).then((resp:any) => {
            if (resp.status == 200) {
                return this.$q.resolve(resp.data);
            } else {
                return this.$q.reject(resp);
            }
        });
    }

    del(url:string):ng.IPromise<any> {
        let uri = `${serviceUrl}/${url}`;
        return this.$http.delete(uri, this.config).then((resp:any) => {
            if (resp.status == 200) {
                return this.$q.resolve(resp.data);
            } else {
                return this.$q.reject(resp);
            }
        });
    }
}