var myApp = angular.module("myApp", []);

var recipeViewController = myApp.controller("recipeViewController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

	$scope.isBusy = true;

	$scope.init = function (model) {
	    $scope.model = model;
	    $scope.recipe = model.Recipe;
	    $log.debug($scope.model);

	    $scope.model.ShoppingList.Items.Remove = function (itemID) {
	        for (i = 0; i < this.length; i++) {
	            if (this[i] == itemID) {
	                this.splice(i, 1);
	                break;
	            }
	        }
	    }
	};

	$scope.ingredientItemChecked = function (item) {
	    if (item.IsChecked) {
	        $scope.model.ShoppingList.Items.push(item.IngredientGroupItemId);
	    }
	    else {
	        $scope.model.ShoppingList.Items.Remove(item.IngredientGroupItemId);
	    }
	};

}]);

