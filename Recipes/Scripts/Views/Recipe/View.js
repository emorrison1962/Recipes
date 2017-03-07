var myApp = angular.module("myApp", []);

var indexController = myApp.controller("recipeViewController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

	$scope.isBusy = true;


	$scope.init = function (model) {
		$scope.recipe = model;
		$log.debug($scope.recipe);
	}



}]);

