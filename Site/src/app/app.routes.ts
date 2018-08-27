import * as angular from 'angular';

export class RouteConfig implements ng.IServiceProvider {
    routes: Route[] = [];

    static $inject = ['$routeProvider'];
    constructor(private $routeProvider) {}

    public $get() {
        return { routes: this.routes };
    }

    public addRoute(route: Route) {
        this.routes[route.id] = route;
        if (route.routePath) {
            this.$routeProvider.when(route.routePath, route);
        }
    }
}

export class Route {//TODO: this needs to allow for a tree structure of routes
    id: string;
    routePath:string;
    title:string;
    template:string;
    controller:string;
    controllerAs:string;

    //controller
    //controllerAs
    //template
    //templateUrl
    //resolve
    //resolveAs
    //redirectTo
    //resolveRedirectTo

    constructor (
        id: string,
        routePath: string,
        title: string,
        template:string,
        controller:string
    ) {
        this.id = id;
        this.routePath = routePath;
        this.title = title;
        this.template = template;
        this.controller = controller;
        this.controllerAs = '$ctrl';
    }
}

angular.module('revoji').provider('RouteConfig', RouteConfig);

angular.module('revoji').config(routeFallBack);
routeFallBack.$inject = ['$routeProvider'];
function routeFallBack($routeProvider) {
    $routeProvider.otherwise({redirectTo: '/login'});
}