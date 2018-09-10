import * as angular from 'angular';
import { AppUser } from '../../models/appuser.model';
import { AppUserService } from '../../services/appuser-api.service';

class signUpModel {
    firstName: string;
    lastName: string;
    handle: string;
    email: string;
    password: string;
}

export class FrontController {
    static $inject: string[] = ['appUserService'];

    model: signUpModel;

    constructor(private appUserService:AppUserService) {
        
    }

    signup() {
        let user: AppUser = {
            firstName: this.model.firstName,
            lastName: this.model.lastName,
            handle: this.model.handle,
            email: this.model.email,
            password: this.model.password
        };

        this.appUserService.create(user)
            .then(this.onCreateSuccess)
            .catch(this.onCreateFail);
    }

    private onCreateSuccess(resp:any) {
        console.log('create success');
        console.log(resp);
    }

    private onCreateFail(error:any) {
        //TODO: display some sort of error (popup for server error/outline textboxes for model error)
        console.log('create fail');
        console.log(error);
    }
}

angular.module('revoji')
   .controller('FrontController', FrontController);