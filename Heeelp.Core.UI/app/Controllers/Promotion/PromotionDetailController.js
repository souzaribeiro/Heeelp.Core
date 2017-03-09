'use strict';

app.controller("PromotionDetailController", ['$scope', 'WebApi', '$routeParams', 'toastr', function ($scope, WebApi, $routeParams, toastr) {
    // $scope.courses = bootstrappedData.courses; 
    var profile = $.parseJSON(getCookie("profile"));
    $(document).scrollTop(0);
    $scope.Promotion = {};
    $scope.SelectedPromotionId = $routeParams.id;
    $scope.CardList = GetSessionStorage("HeeelpCardList");
    $scope.SelectedPromotion = null;
    $scope.couponCode = "Gerando Coupon..."
    $scope.loadGenerateCoupon = true;

    
    $scope.WebApiCore = WebApi.Core();


    $scope.CloseModal = function () {
        $("#emitir-cupom").fadeOut(300);
    }


    //hub.start().done(function () {

    $scope.DisabledGenerateCouponButton = false;

    $scope.GenerateCoupon = function (SelectedPromotion) {
        $scope.DisabledGenerateCouponButton = true;
        $scope.loadGenerateCoupon = false;

        $scope.couponCode = "Gerando Coupon...";
        var userSessionId = $.cookie(UserSessionName);
        var url = $scope.WebApiCore + "/api/coupon/IssueCoupon";

        var data = {};
        data.PromotionId = SelectedPromotion.PromotionId;
        data.UserSessionIssue = userSessionId;
        data.Token = profile.CoreToken;



        $.ajax({
            type: 'POST',
            url: url,
            crossDomain: true,
            headers: { Authorization: 'Bearer ' + profile.CoreToken, 'Access-Control-Allow-Origin': '*' },
            contentType: 'application/x-www-form-urlencoded',
            data: data,
            success: function (ret, b) {
                $scope.DisabledGenerateCouponButton = false;
                if (!ret.IsError) {
                    $scope.$apply(function () {
                        $scope.couponCode = ret.CouponCode;
                        $("#emitir-cupom").fadeIn(300);

                        if (ret.NumberOfAvailableCoupons) {
                            $scope.SelectedPromotion.NumberOfAvailableCoupons = ret.NumberOfAvailableCoupons;
                        }
                        $scope.loadGenerateCoupon = true;
                    });
                } else {
                    //verificar como tratar erro

                    //toastr.error("Houve um problema ao emitir o cupom", "Erro...");
                    toastr.error(ret.ResultMessage, "Erro...");
                    $scope.loadGenerateCoupon = true;
                }

            },
            error: function (xhr, status, error) {
                $scope.DisabledGenerateCouponButton = false;
                //alert('negado');
            }
        });




    }



    
    if ($scope.CardList) {
        var promotionTemp = $.grep($scope.CardList, function (item, index) {
            return item.PromotionId == $scope.SelectedPromotionId;
        });
        if (promotionTemp && promotionTemp.length > 0) {
            $scope.SelectedPromotion = promotionTemp[0];

            var url = $scope.WebApiCore + 'api/classified/getPromotion/' + $scope.SelectedPromotionId;
            $.get(url, null, function (data) {

                $scope.$apply(function () {
                    $scope.SelectedPromotion.FullDescription = data.FullDescription;
                    $scope.SelectedPromotion.ShortDescription = data.ShortDescription;
                    $scope.SelectedPromotion.PhoneNumber = data.PhoneNumber;
                    $scope.SelectedPromotion.PersonDocumentNumber = data.PersonDocumentNumber;
                    $scope.SelectedPromotion.HeeelpCashBackValue = data.HeeelpCashBackValue;
                    $scope.SelectedPromotion.DateOfExpire = data.DateOfExpire;
                    $scope.FullDescription = data.FullDescription;
                    maps(data.PersonName, data.Address, data.Coordinates);

                });
            });
        }
        else {
            alert('Erro ao exibir detalhes da promoção');
        }
    }
    else {
        var url = $scope.WebApiCore + 'api/classified/GetFullPromotion/' + $scope.SelectedPromotionId;
        $.get(url, null, function (promotion) {

            if (promotion)
                $scope.$apply(function () {
                    $scope.SelectedPromotion = promotion;
                    maps(promotion.PersonName, promotion.Address, promotion.Coordinates);
                });
        });

    }







    var maps = function (title, Address, Coordinates) {
        var coordinates = Coordinates.replace("POINT ", "").replace("(", "").replace(")", "").split(" ");
        var myOptions = {
            zoom: 15,
            center: new google.maps.LatLng(coordinates[0], coordinates[1]),
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("gmap_canvas"), myOptions);
        var marker = new google.maps.Marker({ map: map, position: new google.maps.LatLng(coordinates[0], coordinates[1]) });
        var infowindow = new google.maps.InfoWindow({ content: "<b>" + title + "</b><br/>" + Address });

        infowindow.open(map, marker);
    }


    //  Eventos de User Experience


    $('#fileToUpload').on('change', function () {

        $("#BtnSelecionar").empty();
        $("#BtnSelecionar").append("Imagem selecionada");
        $("#BtnSelecionar").addClass('btn-success');
        $(".progress-bar").css('width', '80%');
    });


}]);