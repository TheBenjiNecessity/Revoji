import * as angular from 'angular';
import { SessionService } from './services/session.service';

angular.module('revoji').run(start);

start.$inject = ['$rootScope', 'sessionService'];
export function start($rootScope:ng.IRootScopeService, sessionService: SessionService) {
    $rootScope['sessionService'] = sessionService;
}