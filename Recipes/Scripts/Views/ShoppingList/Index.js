var myApp = angular.module("myApp", []);

var shoppingListIndexController = myApp.controller("shoppingListIndexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    $scope.isBusy = true;

    $scope.init = function (model) {
        $scope.model = model;
        $scope.model.checkedItems = [];
        $log.debug($scope.model);


        $scope.model.Items.Remove = function (item) {
            for (i = 0; i < this.length; i++) {
                if (this[i].IngredientGroupItemId == item.IngredientGroupItemId) {
                    this.splice(i, 1);
                    break;
                }
            }
        }

        $scope.model.checkedItems.Remove = function (item) {
            for (i = 0; i < this.length; i++) {
                if (this[i].IngredientGroupItemId == item.IngredientGroupItemId) {
                    this.splice(i, 1);
                    break;
                }
            }
        }
    };

    $scope.insert = function () {
        $http.post("/Recipe/Insert", { url: newRecipe }).then(function (response) {
            $scope.status = response.status;
            $scope.data = response.data;
        }, function (response) {
            $scope.data = response.data || 'Request failed';
            $scope.status = response.status;
        });
    }

    $scope.ingredientItemChecked = function (item) {
        if (item.IsChecked) {
            $scope.model.Items.Remove(item);
            $scope.model.checkedItems.push(item);
        }
        else {
            $scope.model.Items.push(item);
            $scope.model.checkedItems.Remove(item);
        }
    };

    $scope.collapseClicked = function (btn) {
        $("#btn_toggle_checked")
            .find('span')
            .toggleClass('glyphicon glyphicon-menu-up')
            .toggleClass('glyphicon glyphicon-menu-down');
    };

    $scope.insert = function () {
        //find an existing checked item.
        var existing = $.grep($scope.model.checkedItems, function (item) { return item.Text == $scope.newItem; })
            .map(function (item) { return item; });
        if (undefined !== existing) {
            existing.IsChecked = false;
            $scope.model.Items.push(existing);
            $scope.model.checkedItems.Remove(existing);
        }
        else {
            var item = { IngredientGroupItemId: 0, Text: $scope.newItem };
            $scope.model.Items.push(item);
        }
        $scope.newItem = "";
    }

    $scope.saveShoppingList = function () {
        $http({
            method: 'POST',
            url: '/ShoppingList/UpdateShoppingList',
            data: { vm: $scope.model.ShoppingList },
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

