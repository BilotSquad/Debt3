angular.module('debtManagerApp.debt', [])
	.controller('debtsController', [
		'$scope', '$location', 'debtFactory', function ($scope, $location, debtFactory) {

		    debtFactory.get().success(function (data) {
		        $scope.debts = data;
		    });

		    $scope.addDebtClicked = function () {
		        $location.path('Debts/Create');
		    }
		}]);