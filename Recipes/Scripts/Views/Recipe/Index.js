var myApp = angular.module("myApp", []);

var indexController = myApp.controller("indexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    $scope.isBusy = true;

    $scope.recipes = [];
    $scope.newRecipe = '';

    $log.error('Testing....');


    $scope.init = function (model) {
        $scope.recipes = model;
        $log.debug($scope.recipes);
    }

    $scope.insert = function () {
        $http.post("/Recipe/Insert", { url: newRecipe }).then(function(response) {
            $scope.status = response.status;
            $scope.data = response.data;
        }, function(response) {
            $scope.data = response.data || 'Request failed';
            $scope.status = response.status;
        });
    }

    $scope.update = function (recipe) {
        $http.get("/Recipe/Update", { recipe: recipe }).then(
        function (response) {
            $scope.status = response.status;
            $scope.data = response.data;
            $window.location.href("/Recipe/Update");
        }, function (response) {
            $scope.data = response.data || 'Request failed';
            $scope.status = response.status;
        });
    }

}]);

