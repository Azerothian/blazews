define(['underscore', 'backbone', '../Control', 'jquery', 'jquery.jstree', 'css!jquery.jstree/../themes/apple/style'],
    function (_, Backbone, Control, $) {
        return Control.extend({
            collection: {},
            root: {},
            nodes: [],
            events: {

            },

            OnInitialise: function () {
                this.nodes = [];
                //  this.SetDataCollection(new Backbone.Collection);
               // this.$el.addClass('ui-menu ui-widget ui-widget-content ui-corner-all ui-menu-icons');
                this.root = $('<ul></ul>');
                // _.bindAll(this, ''); // fixes loss of context for 'this' within methods

            },
            OnConfigure: function (config) {
                if (config.collection) {
                    
                    this.SetDataCollection(config.collection);
                    
                }
            },
            SetDataCollection: function (col) {
                this.collection = col;
                
                this.listenTo(this.collection, 'add', this.OnDataAdd);
                this.listenTo(this.collection, 'remove', this.OnDataRemove);
                this.listenTo(this.collection, 'change', this.OnDataChange);
            },
            OnRender: function () {

                $(this.el).append(this.root);
                this.jstree = $(this.root).bind("before.jstree", _.bind( function (e, data) {
                    console.log("Action", { func: data.func, Id: $(data.args[0]).attr("Id"), data: data });


                    switch (data.func) {
                        case "open_node":
                         //   {
                                //                    data: {
                                //                        ParentItem: this.node.NodeID,
                                //                        Application: 
                                //                    }, success: _.bind(function () {

                            this.collection.GetChildren({
                                data: {
                                    ParentItem: $(data.args[0]).attr("Id"),
                                    Application: app.Data.System.GetCurrentApplicationId()
                                }, success: _.bind(function () {


                                }, this)

                            });
                            //console.log("Open node", { e: e, data: data });
                            break;

                    }
                }, this)).jstree({
                    "ui": {
                        //         "initially_select": ["rhtml_2"]
                    },
                    //   "core": { "initially_open": ["rhtml_1"] },
                    "plugins": ["themes", "ui", "crrm"]
                });
                //    .bind("loaded.jstree", function (event, data) {
                //    console.log("LOADED JSTREE");
                //    // you get two params - event & data - check the core docs for a detailed description
                //}).bind("select_node.jstree", function (event, data) {
                //    // `data.rslt.obj` is the jquery extended node that was clicked
                //    console.log("selected node", data.rslt.obj.attr("id"));
                //}).bind("create.jstree", function (e, data) {
                //    console.log("create node");
                //}).bind("remove.jstree", function (e, data) {
                //    console.log("remove node");
                //}).bind("rename.jstree", function (e, data) {
                //    console.log("rename node");
                //}).bind("move_node.jstree", function (e, data) { 
                //    console.log("move node");
                //});
                
            },
            OnAfterRender: function () {


            },
            OnDataChange: function (model) {
                //var id = model.get(this.config.PrimaryKey);
                //if (this.nodes[id]) {
                //    if (this.config.TextField) {
                //        $(this.nodes[id].Text).text(model.get(this.config.TextField));
                //        //$(newnode.Text).text(model.get(this.config.TextField));
                //    }
                //}
            },
            OnDataAdd: function (model) {
                $(this.root).jstree("create", "#" + model.get(this.config.ParentKey), false, { attr: { Id: model.get(this.config.PrimaryKey) }, state: "closed", data: { title: model.get(this.config.TextField) } }, function () { }, true);
            },
            OnDataRemove: function (model) {
                $(this.root).jstree("remove", "#" + model.get(this.config.PrimaryKey));

            },
            OnDispose: function () {

            }

        });
    });

