define(['backbone', 'app/settings'], function (Backbone, settings) {
    return Backbone.Model.extend({
        idAttribute: 'Id',
        url: function () {
            var path = this.get('Id') ? '/applications/' + this.get("Id") : '/applications';
            return settings.WebservicePath + path;
        }
    });



});