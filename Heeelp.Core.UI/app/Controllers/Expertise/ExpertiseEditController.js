'use strict';

app.controller("ExpertiseEditController", ['$scope', 'fileUpload', 'WebApi', '$routeParams', function ($scope, fileUpload, WebApi, $routeParams) {
    // $scope.courses = bootstrappedData.courses; 

    var userId = getCookie("User");
    var _expertiseId = $routeParams.id
    $scope.Expertise = {};
    $scope.Expertise.Name = "";
    $scope.Expertise.Description = "";
    $scope.Expertise.ExpertiseFather = "";
    $scope.Expertise.ApprovalStatusId = "1";
    $scope.Expertise.CreatedBy = userId
    $scope.Expertise.ApprovedBy = userId;
    $scope.files = [];
    $scope.expertiseList = [];
    $scope.PersonList = [];
    $scope.expertiseSaveList = [];
    $scope.expertisePhotoList = [];


    $scope.ListExpertisesCallback = function (data) {
        $scope.$apply(function () {

            $scope.expertiseList = data;
            $scope.expertiseSaveList = data;
        });
    };

    $scope.GetExpertiseWithImagesCallback = function (data) {
        $scope.$apply(function () {

            $scope.Expertise.Name = data.Name;
            $scope.Expertise.Description = data.Description;
            $scope.Expertise.ExpertiseFather = data.ExpertiseFather;
            $scope.ExpertiseFather[2];
            //$scope.expertiseSaveList = data;
            $scope.expertisePhotoList = data.fileUrl;
        });
    };



    $scope.Edit = function (expertise) {
        alert(JSON.stringify(expertise));
    };



    $scope.WebApiCore = WebApi.Core();

    $.post($scope.WebApiCore + '/api/Expertise/ListExpertises', null, $scope.ListExpertisesCallback);
    $.get($scope.WebApiCore + '/api/Expertise/GetExpertise/?id=' + _expertiseId, null, $scope.GetExpertiseWithImagesCallback);

    $scope.uploadFile = function (Expertise) {
        var file = $scope.files;
        var uploadUrl = $scope.WebApiCore + "/api/Expertise/PostFile?jsonData=" + JSON.stringify(Expertise);


        fileUpload.uploadFileToUrl(file, uploadUrl, uploadSuccess, uploadError);
    };

    var uploadSuccess = function () {
        alert('sucesso');
    };
    var uploadError = function () {
    };



}]);