angular.module('debtManagerApp.dashboard', [])
	.controller('dashboardController', [
		'$scope', '$location', 'dashboardFactory', function ($scope, $location, dashboardFactory) {

		    dashboardFactory.getAll().success(function (data) {
		        $scope.aggregatedDebts = data;
		    });

		    dashboardFactory.getAllMinimized().success(function (data) {
		        $scope.aggregatedMinimizedDebts = data;
		    });
		}]);