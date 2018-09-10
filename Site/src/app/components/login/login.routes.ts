import * as angular from 'angular';
import { Route } from '../../app.routes'

const frontView = require('./front.html');
const loginView = require('./login.html');
const forgotView = require('./forgot-password.html');

angular.module('revoji').config(loginRouteConfig);

loginRouteConfig.$inject = ['RouteConfigProvider'];
export function loginRouteConfig(RouteConfigProvider) {
    let frontRoute = new Route('front', '/', 'Revvr', frontView, 'FrontController', {});
    RouteConfigProvider.addRoute(frontRoute);

    let loginRoute = new Route('login', '/login', 'Login', loginView, 'LoginController', {});
    RouteConfigProvider.addRoute(loginRoute);

    let forgotRoute = new Route('forgot', '/forgot', 'Forgot Password', forgotView, 'ForgotController', {});
    RouteConfigProvider.addRoute(forgotRoute);
}