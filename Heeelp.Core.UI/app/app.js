var app = angular.module("app", ['ngRoute', 'ngMask', 'ui.bootstrap', 'toastr','textAngular'/*, 'blueimp.fileupload'*/])
    .config(function ($routeProvider, $locationProvider, toastrConfig) {
        $routeProvider.when('/', { templateUrl: '/app/viewmodels/Home/Home.html', controller: 'HomeController' });
        $routeProvider.when('/Expertise', { templateUrl: '/app/viewmodels/Home/Home.html', controller: 'HomeController' });
        $routeProvider.when('/Expertise/ExpertiseAdd', { templateUrl: '/app/viewmodels/Expertise/ExpertiseAdd.html', controller: 'ExpertiseController' });
        $routeProvider.when('/Expertise/ExpertiseEdit', { templateUrl: '/app/viewmodels/Expertise/ExpertiseEdit.html', controller: 'ExpertiseEditController' });
        $routeProvider.when('/Expertise/ExpertiseEdit/:id', { templateUrl: '/app/viewmodels/Expertise/ExpertiseEdit.html', controller: 'ExpertiseEditController' });
        $routeProvider.when('/Person/PersonAdd', { templateUrl: '/app/viewmodels/Person/PersonAdd.html', controller: 'PersonController' });
        $routeProvider.when('/Person/PersonAddressAdd', { templateUrl: '/app/viewmodels/Person/PersonAddressAdd.html', controller: 'PersonAddressController' });
        $routeProvider.when('/User/UserAdd', { templateUrl: '/app/viewmodels/User/UserAdd.html', controller: 'UserController' });
        $routeProvider.when('/Notification/CommunicationTypeAdd', { templateUrl: '/app/viewmodels/Notification/CommunicationTypeAdd.html', controller: 'CommunicationTypeController' });
        $routeProvider.when('/Notification/NotificationAdd', { templateUrl: '/app/viewmodels/Notification/NotificationAdd.html', controller: 'NotificationController' });
        $routeProvider.when('/Promotion/PromotionAdd', { templateUrl: '/app/viewmodels/Promotion/PromotionAdd.html', controller: 'PromotionController' });
        $routeProvider.when('/Promotion/PromotionManagement', { templateUrl: '/app/viewmodels/Promotion/PromotionManagement.html', controller: 'PromotionManagementController' });
        $routeProvider.when('/Promotion/ApprovePromotion', { templateUrl: '/app/viewmodels/Promotion/ApprovePromotion.html', controller: 'ApprovePromotionController' });
        $routeProvider.when('/Promotion/PromotionDetail/:id', { templateUrl: '/app/viewmodels/Promotion/PromotionDetail.html', controller: 'PromotionDetailController' });
        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });

        angular.extend(toastrConfig, {
            autoDismiss: true,
            containerId: 'toast-container',
            maxOpened: 2,
            newestOnTop: true,
            positionClass: 'toast-bottom-right',
            preventDuplicates: false,
            preventOpenDuplicates: false,
            timeOut: 5000,
            progressBar: true,
            closeButton: true,
            closeHtml: '<button>&times;</button>',
            target: 'body'
        });

    });
angular.module("BsTableApplication", ["bsTable"]);

var hub = $.connection.hub;
app.service('WebApi', function () {
    this.Communication = function () {
        var path = NotificationUrl;
        return path;
    }
    this.Core = function () {
        var path = CoreUrl;
        return path;
    }
});


app.directive('ngFileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.ngFileModel);
            var isMultiple = attrs.multiple;
            var modelSetter = model.assign;
            element.bind('change', function () {
                var values = [];
                angular.forEach(element[0].files, function (item) {
                    var value = {
                        // File Name 
                        name: item.name,
                        //File Size 
                        size: item.size,
                        //File URL to view 
                        url: URL.createObjectURL(item),
                        // File Input Value 
                        _file: item
                    };
                    values.push(value);
                });
                scope.$apply(function () {
                    if (isMultiple) {
                        modelSetter(scope, element[0].files);
                    } else {
                        modelSetter(scope, element[0].files[0]);
                    }
                });
            });
        }
    };
}]);






app.service('fileUpload', ['$http', function ($http) {
    this.uploadFileToUrl = function (file, uploadUrl, SuccessCallBack, ErrorCallback) {
        var fd = new FormData();

        $.each(file, function (index, item) {

            fd.append(index, item);
        })

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
        .success(SuccessCallBack)
        .error(ErrorCallback);
    };

}]);

app.service('sendPerson', ['$http', function ($http) {
    this.send = function (uri, objectData, SuccessCallBack, ErrorCallback) {
        var fd = new FormData();
        //var jsonData = [];
        //$.each(objectData, function (index, item) {
        //    jsonData.push(JSON.stringify( item.attrName,item.Value));
        //});
        var json = angular.toJson(objectData);

        $http.post(uri, json, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': 'application/json' }
        })
        .success(SuccessCallBack)
        .error(ErrorCallback);
    };

}]);

app.service('send', ['$http', function ($http) {
    this.post = function (uri, objectData, SuccessCallBack, ErrorCallback) {
        var json = angular.toJson(objectData);
        var profile = $.parseJSON(getCookie("profile"));
        $.ajax({
            type: 'POST',
            url: uri,
            crossDomain: true,
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*' },
            contentType: 'application/json',
            data: json,
            success: SuccessCallBack,
            error: ErrorCallback
        });
    };
    this.postFIle = function (uri, file, SuccessCallBack, ErrorCallback) {
        var fd = new FormData();
        $.each(file, function (index, item) {
            fd.append(index, item);
        })
        var profile = $.parseJSON(getCookie("profile"));
        $http.post(uri, fd, {
            transformRequest: angular.identity,
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*', 'Content-Type': undefined },
        })
       .success(SuccessCallBack)
       .error(ErrorCallback);

    };
    this.get = function (uri, objectData, SuccessCallBack, ErrorCallback) {
        var json = angular.toJson(objectData);
        var profile = $.parseJSON(getCookie("profile"));
        $.ajax({
            type: 'GET',
            url: uri,
            crossDomain: true,
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*' },
            contentType: 'application/json',
            data: json,
            success: SuccessCallBack,
            error: ErrorCallback
        });
    };

}]);

app.service('sendPersonProspect', ['$http', function ($http) {
    this.send = function (file, uploadUrl, SuccessCallBack, ErrorCallback, objectData) {
        var fd = new FormData();

        $.each(file, function (index, item) {

            fd.append(index, item);
        })

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
            params: objectData
        })
        .success(SuccessCallBack)
        .error(ErrorCallback);
    };
}]);

app.service('sendPromotion', ['$http', function ($http) {
    this.send = function (file, uploadUrl, SuccessCallBack, ErrorCallback, objectData) {
        var fd = new FormData();

        $.each(file, function (index, item) {

            fd.append(index, item);
        })
        //$.each(objectData, function (index, item) {

        //    fd.append(index, item);
        //})
        var profile = $.parseJSON(getCookie("profile"));
        var headers = {
            "Authorization": 'Bearer ' + profile.CoreToken,
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'POST, GET, OPTIONS, PUT',
            'Content-Type': undefined,
            'Accept': undefined
        };

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: headers,
            params: objectData
        })
        .success(SuccessCallBack)
        .error(ErrorCallback);

        
    };
}]);

app.service('sendPersonDocument', ['$http', function ($http) {
    this.send = function (file, uploadUrl, SuccessCallBack, ErrorCallback, objectData) {
        var fd = new FormData();

        $.each(file, function (index, item) {

            fd.append(index, item);
        })

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
            params: objectData
        })
        .success(SuccessCallBack)
        .error(ErrorCallback);
    };
}]);


app.service('sendUser', ['$http', function ($http) {
    this.send = function (file, uploadUrl, SuccessCallBack, ErrorCallback, objectData) {
        var fd = new FormData();

        $.each(file, function (index, item) {

            fd.append(index, item);
        })

        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined },
            params: objectData
        })
        .success(SuccessCallBack)
        .error(ErrorCallback);
    };
}]);


