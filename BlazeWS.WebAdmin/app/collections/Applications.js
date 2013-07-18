define(['backbone', 'app/settings', 'model/Application'], function (Backbone,settings, ApplicationModel) {
    return Backbone.Collection.extend({
        url: settings.WebservicePath + "/applications",
        model: ApplicationModel,
        parse: function (resp, xhr) {
            this.header = resp.header;
            this.stats = resp.stats;
            console.log("[collections/Applications/parse]", resp, resp.Applications);
            return resp.Applications;
        }
    });


});