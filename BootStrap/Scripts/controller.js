/// <reference path="angular.js" />
var memeberModule = angular.module('memeberModule', ['memberServiceModule']);

//通过factory注入，共享数据controller 之间通讯。考虑将所有用到服务的地方都封装到factory中
memeberModule.factory('Data', ['memberService', function (memberService) {
    var vm = {};
    vm.members = memberService.members;
    vm.search = function (query) {return memberService.search(query); }
    return vm;
}]);

//controller 不能依赖 [memberServiceModule];
memeberModule.controller('memberListController', ['$scope', 'memberService','Data', function ($scope, memberService,Data) {
    var memberList = Data.members;
    $scope.memberList =memberList;
    $scope.search = function (query) {
        $scope.memberList = Data.search(query);
    };
}]);

memeberModule.controller('memberDetailController', function ($scope, $routeParams, Data,memberService) {
    //方式1 通过factory依赖注入共享
    /*var members = Data;
    for (var index = 0; index < members.length; index++) {
        if (members[index].id == $routeParams.id) {
            $scope.member = members[index];
            break;
        }
    }*/

    //方式2 直接调用服务层
    $scope.member = memberService.getMebmberById($routeParams.id);

});

memeberModule.directive('addMember', function (memberService) {
    return {
        restrict: 'AE',
        link: function (scope, element, attrs) {
            element.bind("click", function () {
                memberService.addMember({ id: 3, name: "newMember", age: 23 });
                scope.$apply(function () {
                    scope.memberList = memberService.members;
                });
            });
        }
    };
})

memeberModule.filter('searchFilter', function (memberService) {
    return function (members, query) {
        return memberService.search(query);
    }
})