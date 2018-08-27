import * as angular from 'angular';

import 'angular-route';

const app = angular.module('revoji', ['ngRoute']);

export default app;

import './components/main';
import './components/login';

import './services';
