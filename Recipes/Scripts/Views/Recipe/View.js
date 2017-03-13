var myApp = angular.module("myApp", []);

var recipeViewController = myApp.controller("recipeViewController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

	$scope.isBusy = true;

	$scope.init = function (model) {
	    $scope.model = model;
	    $scope.recipe = model.Recipe;
	    $log.debug($scope.model);


	    var count = $scope.model.ShoppingList.Items.count;
	    for (i = 0; i < count; i++) {
	        var item = $scope.model.ShoppingList.Items[i];
	        item.IsChecked = true;
	    }


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

	$scope.saveShoppingList = function () {
	    $http({
	        method: 'POST',
	        url: '/Recipe/UpdateShoppingList',
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

