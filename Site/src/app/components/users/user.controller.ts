import * as angular from 'angular';
import { AppUser } from '../../models/appuser.model';

export class UserController {
    static $inject: string[] = ['user'];

    constructor(private user:AppUser) {}
}

angular.module('revoji')
   .controller('UserController', UserController);