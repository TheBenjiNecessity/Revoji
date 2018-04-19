import * as ng from 'angular';

export class MainController {
    static $inject = ['$http'];
    test:string;

    constructor(private $http) {
        this.test = 'It worked!'

        this.$http.get('sample/2143').then((resp) => {
            console.log(resp);
        });
    }
}

ng.module('revoji')
   .controller('MainController', MainController);