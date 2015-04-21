var debtManager = angular.module('debtManager', ['ngRoute', 'debtManagerApp.debt', 'debtManagerApp.payment', 'debtManagerApp.user', 'debtManagerApp.dashboard']);

debtManager.config(function ($routeProvider) {
    $routeProvider.when('/', { templateUrl: 'Areas/Dashboard/Views/overview.html', controller: 'dashboardController' });
    $routeProvider.when('/Debts', { templateUrl: 'Areas/Debt/Views/overview.html', controller: 'debtsController' });
    $routeProvider.when('/Debts/Create', { templateUrl: 'Areas/Debt/Views/create.html', controller: 'debtController' });
    $routeProvider.when('/Payments', { templateUrl: 'Areas/Payment/Views/overview.html', controller: 'paymentsController' });
    $routeProvider.when('/Payments/Create', { templateUrl: 'Areas/Payment/Views/create.html', controller: 'paymentController' });
});