'use strict';

angular.module('debtManagerApp.payment')
    .factory('paymentFactory', ['$http', function ($http) {

        var urlBase = '/api/Payments';
        var paymentFactory = {};

        paymentFactory.get = function () {
            return $http.get(urlBase);
        };

        paymentFactory.create = function (entity) {
            return $http.post(urlBase, entity);
        };

        return paymentFactory;
    }]);