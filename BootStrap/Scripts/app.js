/// <reference path="/angular.js" />
var mainApp = angular.module('mainApp', ['memeberModule','ngRoute']);

mainApp.config(['$routeProvider',function ($routeProvider) {
        $routeProvider.
            when('/memberList', {
                templateUrl: 'Views/list.html',
                controller: 'memberListController'
            }).
             when('/detail/:id', {
                templateUrl: 'Views/detail.html',
                controller: 'memberDetailController'
            }).
            otherwise({
                redirectTo: '/memberList'
            });
    }
])
