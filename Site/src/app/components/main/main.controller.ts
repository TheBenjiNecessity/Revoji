import * as angular from 'angular';

export class MainController {
    static $inject = [];
    test:string = 'tests';

    constructor() {
        this.test = 'It worked!'

        let username = 'test';
        let password = 'test';

        let data = {
            client_id: "com.revoji",
            client_secret: "secret",
            username: username,
            password: password,
            grant_type: "password"
        };

        // this.$http({
        //     method: 'POST',
        //     url: '//localhost:8000/api/connect/token',
        //     headers: {
        //         'Content-Type': 'application/x-www-form-urlencoded'
        //     },
        //     data: $.param(data)
        // }).then(resp => {
        //     console.log(resp.data);
        // }, err => {
        //     console.log(err.data);
        // });
    }
}

angular.module('revoji')
   .controller('MainController', MainController);