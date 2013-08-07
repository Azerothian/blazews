define(['backbone', 'app/settings'], function (Backbone, settings) {
    return Backbone.Model.extend({
        idAttribute: 'Id',
        url: function () {
            var path = this.get('Id') ? '/datasources/' + this.get("Id") : '/datasources';
            return settings.WebservicePath + path;
        },
        destroy: function () {
            var args = _.extend(
                {
                    attrs: { 'Application': this.get('Application') }
                }, arguments);
            Backbone.Model.prototype.destroy.apply(this, args);

        }
    });



});