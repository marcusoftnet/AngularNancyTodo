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

    $scope.limit = 2; // Number of search results

    // Sort stuff
    $scope.sort_order = 'Priority';
    $scope.sort_desc = false;

    $scope.search = function () {
        Todo.query({ q: $scope.query,
                     limit: $scope.limit, 
                     offset: $scope.offset,
                     sort: $scope.sort_order,
                     desc: $scope.sort_desc
                 },

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

    $scope.sort_by = function (ord) {
        if ($scope.sort_order == ord) {
            $scope.sort_desc = !$scope.sort_desc; 
        }
        else {
            $scope.sort_desc = false; 
        }
        $scope.sort_order = ord;
        $scope.reset();
    };

    $scope.do_show = function (asc, col) {
        return (asc != $scope.sort_desc) && ($scope.sort_order == col);
    };

    $scope.reset();
};

TodoApp.directive('sorted', function () {
    // Ok - we're creating a new object here...
    // with some properties that I expect that Angular needs

    return {
        scope: true,
        transclude: true,

        template: '<a ng-click="do_sort()" ng-transclude></a>' +
                  '<span ng-show="do_show(true)"><i class="icon-circle-arrow-down"></i></span>' +
                  '<span ng-show="do_show(false)"><i class="icon-circle-arrow-up"></i></span>',

        controller: function ($scope, $element, $attrs) {
            $scope.sort = $attrs.sorted;

            $scope.do_sort = function () { $scope.sort_by($scope.sort); };

            $scope.do_show = function (asc) {
                return (asc != $scope.sort_desc) && ($scope.sort_order == $scope.sort);
            };

        } // ???
    };
});