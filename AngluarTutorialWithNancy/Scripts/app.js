var TodoApp = angular.module("TodoApp", ["ngResource"]).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: ListCtrl, templateUrl: '/Scripts/templates/list.html' }).
            otherwise({ redirectTo: '/' });
    });

var ListCtrl = function ($scope, $location) {
    $scope.test = "testing";
};