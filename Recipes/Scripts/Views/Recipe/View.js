var myApp = angular.module("myApp", []);

var recipeViewController = myApp.controller("recipeViewController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

	$scope.isBusy = true;

	$scope.init = function (model) {
	    $scope.model = model;
	    $scope.recipe = model.Recipe;
	    $log.debug($scope.model);

        
	    $scope.model.ShoppingList.Items.Remove = function (item) {
	        for (i = 0; i < this.length; i++) {
	            if (this[i].IngredientGroupItemId === item.IngredientGroupItemId) {
	                this.splice(i, 1);
	                break;
	            }
	        }
	    }
	};

	$scope.ingredientItemChecked = function (item) {
	    if (item.IsChecked) {
	        $scope.model.ShoppingList.Items.push(item);
	    }
	    else {
	        $scope.model.ShoppingList.Items.Remove(item);
	    }
	};

	$scope.saveShoppingList = function () {
	    $http({
	        method: 'POST',
	        url: '/ShoppingList/UpdateShoppingList',
	        data: { vm: $scope.model.ShoppingList},
	    }).success(function (data, status, headers, config) {
	        $scope.message = '';
	        if (data.success == false) {
	            var str = '';
	            for (var error in data.errors) {
	                str += data.errors[error] + '\n';
	            }
	            $scope.message = str;
	        }
	        else {
	            $scope.message = 'Saved Successfully';
	            //$window.location.href("/Courses/Index");

	        }
	    }).error(function (data, status, headers, config) {
	        $scope.message = 'Unexpected Error';
	    });
	};


}]);

