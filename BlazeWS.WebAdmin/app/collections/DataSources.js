define(['backbone', 'app/settings', 'model/DataSource'], function (Backbone, settings, model) {
    return Backbone.Collection.extend({
        url: settings.WebservicePath + "/datasources",
        model: model,
        parse: function (resp, xhr) {
            this.header = resp.header;
            this.stats = resp.stats;
            return resp.Data;
        },
        fetch: _.bind(function (options) {
            if (this.Application) {
                if (!(options.data)) {
                    options.data = {};
                }
                options.data.Application = this.Application.get('Id');
            }
            Backbone.Collection.prototype.fetch.apply(this, options);
        }, this)
    });


});