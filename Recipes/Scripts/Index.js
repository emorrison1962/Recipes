var myApp = angular.module("myApp", []);

var indexController = myApp.controller("indexController", ['$scope', '$window', '$log', '$http', function ($scope, $window, $log, $http) {

    $scope.isBusy = true;

    $scope.recipes = [];
    $scope.added = '';

    $log.error('Testing....');


    $scope.init = function (model) {
        $scope.recipes = model;
        $log.debug($scope.recipes);
    }

    $scope.add = function () {
        $log.error($scope.added);

    }

}]);

