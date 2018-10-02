import * as angular from 'angular';

import { Route } from '../../app.routes';

const userView = require('./user.html');
const usersView = require('./users.html');

angular.module('revoji').config(userRouteConfig);

userRouteConfig.$inject = ['RouteConfigProvider'];
export function userRouteConfig(RouteConfigProvider) {
    let userRoute = new Route('user', '/user', '', userView, 'UserController', { user: getUser });
    RouteConfigProvider.addRoute(userRoute);

    let usersRoute = new Route('users', '/users', 'Users', usersView, 'UsersController', { users: getUsers });
    RouteConfigProvider.addRoute(usersRoute);
}

getUser.$inject = ['$q', '$route', 'appUserService'];
function getUser($q, $route, appUserService) {
    return appUserService.getAuthUser();
}

getUsers.$inject = ['$q', '$route', 'appUserService'];
function getUsers($q, $route, appUserService) {
    //TODO
}