import * as angular from 'angular';

import 'angular-route';
import 'angular-local-storage';

const app = angular.module('revoji', ['ngRoute', 'LocalStorageModule']);

export default app;

import './components/main';
import './components/login';

import './services';

import './shared/filters';

app.config(configure);

function configure($routeProvider, $locationProvider) {
    // use the HTML5 History API
    $locationProvider.html5Mode(true);
}