import * as angular from 'angular';
import { AppUser } from '../../models/appuser.model';
import { AppUserService } from '../../services/appuser-api.service';

export class UserController {
    static $inject: string[] = ['$location', 'appUserService', 'user'];

    constructor(
        private $location,
        private appUserService:AppUserService,
        private user:AppUser
    ) {}
}

angular.module('revoji')
   .controller('UserController', UserController);