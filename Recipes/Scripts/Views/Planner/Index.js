var myApp = angular.module("myApp", ['as.sortable']);

var plannerIndexController = myApp.controller("plannerIndexController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

    var vm = this;
    vm.isBusy = false;

    $scope.init = function (model) {

        $log.debug(JSON.stringify(model));

        model.Planner.Groups.forEach(function (group) {
            group.Items.forEach(function (item) {
                var recipe = model.RecipeCatalog.firstOrDefault(function (recipe) {
                    return recipe.RecipeId === item.Recipe.RecipeId;
                });
                recipe.IsChecked = true;
                item.IsChecked = true;
                item.Recipe = recipe;
                recipe.PlannerItemId = item.PlannerItemId;
            });
        });
        vm.model = model;
        $log.debug(vm.model);
    };


    $scope.getAllItems = function () {
        var result = [];
        vm.model.Planner.Groups.forEach(function (group) {
            Array.prototype.push.apply(result, group.Items);
        });
        return result;
    };

    $scope.addItem = function () {
        var items = $scope.getAllItems();
        var item = items.firstOrDefault({ Text: vm.newItem });
        if (null !== item) {//prevent dupes
            item.IsChecked = true;
        }
        else {
            var groupId = vm.model.Planner.Groups[0].PlannerGroupId;
            var item = { PlannerItemId: 0, GroupId: groupId, Recipe: null, Text: vm.newItem, IsChecked: true };
            vm.model.Planner.Groups[0].Items.push(item)
        }
        vm.newItem = "";
    }

    $scope.newItem_Changed = function titleCase() {
        var splitStr = vm.newItem.toLowerCase().split(' ');
        for (var i = 0; i < splitStr.length; i++) {
            // You do not need to check if i is larger than splitStr length, as your for does that for you
            // Assign it back to the array
            splitStr[i] = splitStr[i].charAt(0).toUpperCase() + splitStr[i].substring(1);
        }
        // Directly return the joined string
        vm.newItem = splitStr.join(' ');
    }

    $scope.dragControlListeners = {
        itemMoved: function (event) {
            $scope.setGroup(event.source.itemScope.item, event.dest.sortableScope.group);
            $log.debug(vm.model);

        },
        orderChanged: function (event) {
            var x = 0;
    }};

    $scope.mouseover = function () {
        var x = 0;
        //var ctx = canvas.getContext("2d");
        //ctx.fillStyle = "#FF0000";
        //ctx.fillRect(0, 0, 80, 80);
    };


    $scope.trackingFunction = function (group, plannerItem) {
        return group.PlannerGroupId << 2 | plannerItem.PlannerItemId << 1 | plannerItem.Recipe.RecipeId;
    };


    $scope.plannerItemChecked = function (item) {
        if (!item.IsChecked) {
            item.Recipe.IsChecked = false;
            vm.model.Planner.Groups.forEach(function (group) {
                group.Items.RemovePlannerItem(item);
            }
        )}
    };


    $scope.recipeChecked = function (recipe) {
        if (recipe.IsChecked) {
            var groupId = vm.model.Planner.Groups[0].PlannerGroupId;
            var plannerItem = { PlannerItemId: 0, GroupId: groupId, Recipe: recipe, Text: recipe.Name, IsChecked: true };
            vm.model.Planner.Groups[0].Items.push(plannerItem)
        }
    };

    $scope.setGroup = function (plannerItem, dst) {
        //var src = $scope.getGroup(plannerItem.GroupId);
        //if (src)
        //    src.Items.RemovePlannerItem(plannerItem);

        plannerItem.GroupId = dst.PlannerGroupId;
        //dst.Items.push(plannerItem);

    };

    $scope.getGroup = function (id) {
        var group = vm.model.Planner.Groups.firstOrDefault(function (group) { return group.PlannerGroupId === id; });
        return group;
    };

    Array.prototype.RemovePlannerItem = function (item) {
        for (i = 0; i < this.length; i++) {
            if (this[i].Recipe.RecipeId === item.Recipe.RecipeId) {
                this.splice(i, 1);
                break;
            }
        }
    };

    $scope.savePlanner = function () {
        vm.isBusy = true;
        $http({
            method: 'POST',
            url: 'Planner/Update',
            data: JSON.stringify(vm.model.Planner) ,
        }).success(function (data, status, headers, config) {
            vm.isBusy = false;
            vm.message = '';
            if (data.success == false) {
                var str = '';
                for (var error in data.errors) {
                    str += data.errors[error] + '\n';
                }
                vm.message = str;
            }
            else {
                vm.message = 'Saved Successfully';
                //$window.location.href("/Courses/Index");

            }
        }).error(function (data, status, headers, config) {
            vm.isBusy = false;
            vm.message = 'Unexpected Error';
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

