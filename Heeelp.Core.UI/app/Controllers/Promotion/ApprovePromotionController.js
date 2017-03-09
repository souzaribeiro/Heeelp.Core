'use strict';

app.controller("ApprovePromotionController", ['$scope', 'WebApi', '$routeParams','toastr', function ($scope, WebApi, $routeParams,toastr) {

    $(document).scrollTop(0);
    $scope.Promotion = {};
    $scope.CardList = [];
    $scope.page = 0;
    $scope.HasNextPage = false;
    
    var userSessionId = getCookie(user_session);
    $scope.WebApiCore = WebApi.Core();
    $scope.ChangeOrderPromotionList = function (orderBy, selectedOrderByText) {
        $scope.selectedOrderByText = selectedOrderByText;
        $scope.selectedOrderByID = orderBy;
        $scope.CardList = [];
        $scope.page = 0;
        $scope.LoadingCards(orderBy);
    };


    $scope.SetConstants = function () {
        $scope.LowerPrice = "Menor Preço";
        $scope.HigherPrice = "Maior Preço";
        $scope.HigherDiscount = "Maior Desconto";
    };
    $scope.SetConstants();
    $scope.selectedOrderByText = $scope.LowerPrice;
    $scope.selectedOrderByID = 1;



    $scope.LoadingCards = function (orderBy) {

        var url = $scope.WebApiCore + 'api/classified/ListPromotionClassifiedPerPageWaitingApproval/' + $scope.page + '/' + orderBy;
        var type = 'GET';
        var card = "";

        $.get(url, null, function (data) {

            var countDistance = 0;
            $scope.$apply(function () {
                $scope.CardList = [];
                $.merge($scope.CardList, data.Result);
                $scope.HasNextPage = data.Result[0].HasNextPage;
                if ($scope.HasNextPage) {
                    $(document).scroll($scope.onScroll);
                }
            });
            SetSessionStorage("HeeelpCardList", $scope.CardList);
        });

    }
    $scope.LoadingCards(1);

    $scope.onScroll = function (e) {
        if (($(e.target).scrollTop() + $(window).height()) >= (($('body').prop('scrollHeight') - 80))) {
            $(document).off("scroll", $scope.onScroll);
            $scope.page++;
            $scope.LoadingCards($scope.selectedOrderByID);
        }

    }

    $scope.approve = function (PromotionIntegrationCode) {
        var promotionData = {};
        promotionData.IntegrationCode = PromotionIntegrationCode;
        promotionData.PromotionApprovalStatus = enumPromotionApprovalStatus.APROVADO;
        promotionData.UserSessionId = userSessionId;
        var url = $scope.WebApiCore + "/api/Promotion/ApprovePromotion";
        send(url, promotionData, 'POST');
    }
    $scope.refuse = function (PromotionIntegrationCode) {
        $scope.Promotion.IntegrationCode = PromotionIntegrationCode;
        openModalRefuse();
    }
    $scope.sendRefusePromotion = function (promotion) {
        var promotionData = {};
        promotionData.IntegrationCode = $scope.Promotion.IntegrationCode;
        promotionData.PromotionApprovalStatus = enumPromotionApprovalStatus.REJEITADO;
        promotionData.RefusedReason = promotion.RefusedReason;
        promotionData.UserSessionId = userSessionId;
        var url = $scope.WebApiCore + "/api/Promotion/RefusePromotion";
        send(url, promotionData, 'POST');
        closeModalRefuse();
        
    }
    $scope.closeModalRecused = function () {
        closeModalRefuse();
    }
    
    var closeModalRefuse = function () {
        $("#RefusePromotionModal").fadeOut(50);
    }
    var openModalRefuse = function () {
        $("#RefusePromotionModal").fadeIn(50);
    }
    var send = function (url, data, type) {
        var profile = $.parseJSON(getCookie("profile"));
        $.ajax({
            type: type,
            url: url,
            crossDomain: true,
            dataType: "json",
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*' },
            contentType: 'application/x-www-form-urlencoded',
            data: data,
            success: function (response) {
                toastr.success('Solicitação enviada com sucesso!', 'Sucesso!');
                $scope.Promotion.IntegrationCode = '';
            },
            error: function (xhr, status, error) {
                toastr.error('Erro ao enviar Solicitação!', 'Erro!');
                $scope.Promotion.IntegrationCode = '';
            }
        });
    }
    var enumPromotionApprovalStatus = {
        AGUARDANDOAPROVACAO: 1,
        APROVADO: 2,
        REJEITADO: 3
    };

}]);