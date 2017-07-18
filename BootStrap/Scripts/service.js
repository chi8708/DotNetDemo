/// <reference path="angular.js" />
//ngResource 网络请求 依赖$http
var memberServiceModule = angular.module('memberServiceModule', []);
memberServiceModule.service('memberService', function () {
    var self = this;

    self.members = [{ "id": 1, "name": 'jack', "age": 12 }, { "id": 2, "name": 'tom', "age": 15 }];

    self.search = function (query) {
        var data = [];
        if (!query) {
            data = self.members;
        }
        else {
            angular.forEach(self.members, function (member) {
                if (member.name.toLowerCase().indexOf(query) !== -1) {
                    data.push(member);
                }
            });
        }
        return data;
    }

    self.getMebmberById = function (id) {
        if (id<=0) {
            return null;
        }
        else {
            var memberNew = {};
            angular.forEach(self.members, function (member) {
                if (member.id == id) {
                    memberNew = member;
                    return;
                }
            });

            return memberNew;
        }
    }

    self.addMember = function (member) {
        if (member) {
            self.members.push(member);
        }
    }
})