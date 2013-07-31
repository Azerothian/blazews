define(['backbone', 'jsui/collections/State', 'app/settings', 'model/Item'], function (Backbone, StateCollection, settings, ItemModel) {
    return StateCollection.extend({
        //  ApplicationId: {},
        initialise: function() {
            StateCollection.initialise.apply(this, arguments);
            _.bindAll(this, "GetChildren");
        },
        url: settings.WebservicePath + "/items",
        model: ItemModel,
        OnParse: function (resp) {
            if (resp.Items) {
                return resp.Items;
            }
            return [resp];
        },
        GetChildren: function (obj) {
            this.fetch({
                data: obj.data,
                success: _.bind(function () {
                    this.each(function (m) {
                       // console.log("bind url", m);
                        m.url = m.defaultUrl;
                    });

                    obj.success()
                }, this),
                url: settings.WebservicePath + "/items/children",
                remove: false
            });

        },
        //,getItems: _.bind(function (obj) {
        //    this.fetch({ data: { ApplicationId: this.ApplicationId, ParentItemId: obj.ParentItemId }, success: obj.success });
        //}, this),
        sync: function(method, model)
        {
            console.log("collection sync", { Method: method, Model: model });
            return Backbone.sync.apply(this, arguments);
        }
    });


});