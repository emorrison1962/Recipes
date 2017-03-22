var myApp = angular.module("myApp", []);

var shoppingListIndexController = myApp.controller("shoppingListIndexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    $scope.isBusy = true;

    $scope.init = function (model) {
        $scope.model = model;
        $scope.checkedItems = [];
        $log.debug($scope.model);

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

        $scope.model.Items.Remove = function (item) {
            for (i = 0; i < this.length; i++) {
                if (this[i].IngredientItemId == item.IngredientItemId) {
                    this.splice(i, 1);
                    break;
                }
            }
        }

        $scope.checkedItems.Remove = function (item) {
            for (i = 0; i < this.length; i++) {
                if (this[i].IngredientItemId == item.IngredientItemId) {
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
        $scope.setChecked(item);
        //if (item.IsChecked) {
        //    $scope.model.Items.Remove(item);
        //    $scope.checkedItems.push(item);
        //}
        //else {
        //    $scope.model.Items.push(item);
        //    $scope.checkedItems.Remove(item);
        //}
    };

    $scope.collapseClicked = function (btn) {
        $("#btn_toggle_checked")
            .find('span')
            .toggleClass('glyphicon glyphicon-menu-up')
            .toggleClass('glyphicon glyphicon-menu-down');
    };

    $scope.setChecked = function (item) {
        if (item.IsChecked) {
            $scope.model.Items.Remove(item);
            $scope.checkedItems.push(item);
        }
        else {
            $scope.model.Items.push(item);
            $scope.checkedItems.Remove(item);
        }
    };


    $scope.addItem = function () {
        var item = $scope.model.Items.firstOrDefault({ Text: $scope.newItem });
        if (null === item) {//prevent dupes
            //find an existing checked item.
            var item = $scope.checkedItems.firstOrDefault({ Text: $scope.newItem });
            if (null !== item) {
                item.IsChecked = false;
                $scope.setChecked(item);
            }
            else {
                var item = { IngredientItemId: 0, Text: $scope.newItem, IsChecked: false };
                $scope.setChecked(item);
            }
        }
        $scope.newItem = "";
    }

    $scope.saveShoppingList = function () {
        $http({
            method: 'POST',
            url: '/ShoppingList/UpdateShoppingList',
            data: { vm: $scope.model },
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

