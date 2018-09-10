import * as angular from 'angular';
import { AppUser } from '../../models/appuser.model';
import { AppUserService } from '../../services/appuser-api.service';

export class UsersController {
    static $inject: string[] = ['appUserService'];

    constructor(private appUserService:AppUserService) {}
}

angular.module('revoji')
    .controller('UsersController', UsersController);