var myApp = angular.module("myApp", []);

var recipeViewController = myApp.controller("recipeViewController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

	$scope.isBusy = true;

	$scope.init = function (model) {
	    $scope.model = model;
	    $log.debug($scope.model);

	    //$scope.model.ShoppingList.Items.Remove = function (item) {
	    
	    Array.prototype.Remove = function (item) {
	        for (i = 0; i < this.length; i++) {
	            if (this[i].IngredientItemId === item.IngredientItemId) {
	                this.splice(i, 1);
	                break;
	            }
	        }
	    };
	};

	$scope.ingredientItemChecked = function (ingredientItem) {
	    var shoppingListItem = createShoppingListItem(ingredientItem);
	    if (ingredientItem.IsChecked) {
	        $scope.model.ShoppingList.Groups[0].Items.push(shoppingListItem);
	    }
	    else {
	        $scope.model.ShoppingList.Groups[0].Items.Remove(shoppingListItem);
	    }
	};

	createShoppingListItem = function (ingredientItem) {
	    return { ShoppingListItemId: 0, Text: ingredientItem.Text }
	};


	$scope.saveShoppingList = function () {
	    //window.alert("hi!");
	    $http({
	        method: 'POST',
	        url: 'ShoppingList/UpdateShoppingList',
	        data: { shoppingList: $scope.model.ShoppingList },
	    }).success(function (data, status, headers, config) {
	        $scope.message = '';
	        if (data.success == false) {
	            var str = '';
	            for (var error in data.errors) {
	                str += data.errors[error] + '\n';
	            }
	            $scope.message = str;
	            window.alert($scope.message);
            }
	        else {
	            $scope.message = 'Saved Successfully';
	            //window.alert($scope.message);
	            //$window.location.href("/Courses/Index");

	        }
	    }).error(function (data, status, headers, config) {
	        $scope.message = 'Unexpected Error';
	        window.alert($scope.message);
	    });
	};

    /////////////////////////////////////////////////////////////////////////////
    // Utilities
    /////////////////////////////////////////////////////////////////////////////
	Array.prototype.where = function (filter) {

	    var collection = this;

	    switch (typeof filter) {

	        case 'function':
	            return $.grep(collection, filter);

	        case 'object':
	            for (var property in filter) {
	                if (!filter.hasOwnProperty(property))
	                    continue; // ignore inherited properties

	                collection = $.grep(collection, function (item) {
	                    return item[property] === filter[property];
	                });
	            }
	            return collection.slice(0); // copy the array 
	            // (in case of empty object filter)

	        default:
	            throw new TypeError('func must be either a' +
                    'function or an object of properties and values to filter by');
	    }
	};


	Array.prototype.firstOrDefault = function (func) {
	    return this.where(func)[0] || null;
	};

    /////////////////////////////////////////////////////////////////////////////




}]);

