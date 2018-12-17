(function (app) {
    app.filter("filterStatus", function () {
        return function (input) {
            if (input == true) {
                return "Kích hoạt";
            } else {
                return "Khóa";
            }
        }
    });
})(angular.module("commonModule"));


