var myApp = angular.module("myApp", []);

var recipeViewController = myApp.controller("recipeViewController", ['$scope', '$window', '$log', '$http', '$location', function ($scope, $window, $log, $http, $location) {

	$scope.isBusy = true;
	$scope.msg = "Hello, World!";
	$scope.recipe;


	$scope.init = function (model) {
	    $scope.recipe = model.Recipe;
	    $log.debug($scope.recipe);
	};

	$scope.ingredientItemChecked = function (item) {
	    if (employeeVM.IsEnrolled) {
	        $scope.course.Enrollments.push({ CourseID: $scope.course.CourseID, EmployeeID: employeeVM.Employee.EmployeeID });
	    }
	    else {
	        $scope.course.Enrollments.Remove(employeeVM.Employee.EmployeeID);
	    }
	};

}]);

