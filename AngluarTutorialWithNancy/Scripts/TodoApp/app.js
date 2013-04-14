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

    $scope.limit = 1; // Number of search results

    $scope.search = function () {
        Todo.query({ q: $scope.query, limit: $scope.limit, offset: $scope.offset },
            function (items) {
                var cnt = items.length;
                $scope.no_more = cnt < $scope.limit;
                $scope.items = $scope.items.concat(items);
            });
    };

    $scope.reset = function () {
        $scope.offset = 0;
        $scope.items = [];
        $scope.search();
    };

    $scope.show_more = function () { return !$scope.no_more; };

    

    $scope.reset();
};