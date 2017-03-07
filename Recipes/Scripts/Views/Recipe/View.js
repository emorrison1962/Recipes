var myApp = angular.module("myApp", []);

var recipeViewController = myApp.controller("recipeViewController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

	$scope.isBusy = true;
	$scope.msg = "Hello, World!";
	$scope.recipe;


	$scope.init = function (model) {
		$scope.recipe = model.Recipe;
		$log.debug($scope.recipe);
	}



}]);

