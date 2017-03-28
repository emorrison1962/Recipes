var myApp = angular.module("myApp", []);

var shoppingListIndexController = myApp.controller("shoppingListIndexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    $scope.isBusy = true;

    $scope.init = function (model) {
        $scope.model = model;
        $log.debug($scope.model);
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

    $scope.collapseClicked = function (btn) {
        $("#btn_toggle_checked")
            .find('span')
            .toggleClass('glyphicon glyphicon-menu-up')
            .toggleClass('glyphicon glyphicon-menu-down');
    };

    $scope.getAllItems = function () {
        var result = [];
        $scope.model.Groups.forEach(function (group) {
            Array.prototype.push.apply(result, group.Items);
        });
        return result;
    };

    $scope.addItem = function () {
        var items = $scope.getAllItems();
        var item = items.firstOrDefault({ Text: $scope.newItem });
        if (null !== item) {//prevent dupes
            item.IsChecked = false;
        }
        else {
            var item = { ShoppingListItemId: 0, Text: $scope.newItem, IsChecked: false };
            $scope.model.Groups[0].Items.push(item);
        }
        $scope.newItem = "";
    }

    $scope.saveShoppingList = function () {
        $http({
            method: 'POST',
            url: '/ShoppingList/UpdateShoppingList',
            data: { shoppingList: $scope.model },
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

}]);

