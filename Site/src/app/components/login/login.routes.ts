import * as angular from 'angular';
import { Route } from '../../app.routes'
import { LoginController } from '.';

const loginView = require('./login.html');

angular.module('revoji').config(loginRouteConfig);

loginRouteConfig.$inject = ['RouteConfigProvider'];
export function loginRouteConfig(RouteConfigProvider) {
    let loginRoute = new Route('login', '/login', 'Login', loginView, 'LoginController');
    RouteConfigProvider.addRoute(loginRoute);
}