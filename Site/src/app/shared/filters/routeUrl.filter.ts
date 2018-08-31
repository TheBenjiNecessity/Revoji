import * as angular from 'angular';

RouteUrlFilter.$inject = ['RouteConfig'];
export function RouteUrlFilter(routeConfig:any) {
    return (name, args) => {
        return routeConfig.routes[name].toUrl(args);
    }
}

angular.module('revoji').filter('toRouteUrl', RouteUrlFilter);