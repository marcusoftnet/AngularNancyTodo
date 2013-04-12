var TodoApp = angular.module("TodoApp", ["ngResource"]).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: ListCtrl, templateUrl: '/Scripts/TodoApp/templates/list.html' }).
            otherwise({ redirectTo: '/' });
    });
    

TodoApp.factory('Todo', function ($resource) {
    return $resource('/api/todo/:id', { id: '@id' }, { update: { method: 'PUT'} });
});

var ListCtrl = function ($scope, $location, Todo) {
    $scope.items = Todo.query();
};