(function (app) {
    app.service("apiService", apiService);

    apiService.$inject = ['$http', 'notificationService'];
    function apiService($http, notificationService) {
        return {
            get: get,
            post: post,
            put: put,
            del: del,
        }

        function post(url, params, success, failure) {
            $http.post(url, params).then(
                function (result) {
                    success(result);
                },
                function (error) {
                    if (error.status == '401') {
                        notificationService.displayError("Authen is required");
                    } else {
                        failure(error);
                    }                                  
                }

            );
        }

        function put(url, params, success, failure) {
            $http.put(url, params).then(
                function (result) {
                    success(result);
                },
                function (error) {
                    if (error.status == '401') {
                        notificationService.displayError("Authen is required");
                    } else {
                        failure(error);
                    }
                }

            );
        }

        function del(url, params, success, failure) {
            $http.delete(url, params).then(
                function (result) {
                    success(result);
                },
                function (error) {
                    if (error.status == '401') {
                        notificationService.displayError("Authen is required");
                    } else {
                        failure(error);
                    }
                }

            );
        }

        function get(url, params, success, failure) {
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                failure(error);
            });
        }
    }
})(angular.module("commonModule"));