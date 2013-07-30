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
                this.jstree = $(this.root).bind("before.jstree", _.bind(function (e, data) {

                    //var selected = data.inst.get_selected();
                    // console.log("Action", { func: data.func, Id: $(data.args[0]).attr("Id"), data: data, selected: selected });


                    switch (data.func) {
                        case "open_node":
                            //   {
                            //                    data: {
                            //                        ParentItem: this.node.NodeID,
                            //                        Application: 
                            //                    }, success: _.bind(function () {
                            if (this.config.Events) {
                                if (this.config.Events.Expand) {
                                    this.config.Events.Expand($(data.args[0]).attr("Id"), this.collection);
                                }

                            }

                            //console.log("Open node", { e: e, data: data });
                            break;
                            //   case "save_selected":


                            //       break;

                    }
                }, this))
                    .jstree({
                        "plugins": ["themes", "ui", "crrm", "contextmenu"]
                    })
                    .bind("loaded.jstree", function (event, data) {
                        console.log("LOADED JSTREE");
                        // you get two params - event & data - check the core docs for a detailed description
                    }).bind("select_node.jstree", _.bind(function (event, data) {
                        // `data.rslt.obj` is the jquery extended node that was clicked
                        console.log("selected node", data.rslt.obj.attr("id"));

                        if (this.config.Events) {
                            if (this.config.Events.Select) {
                                this.config.Events.Select(this.collection.get(data.rslt.obj.attr("id")));

                            }
                        }

                    }, this)).bind("create.jstree", _.bind(function (e, data) {
                        if ($(data.rslt.obj).attr("Id") === undefined) {

                            var model = new this.collection.model;
                            model.set(this.config.TextField, data.rslt.name);
                            $(this.root).find(data.rslt.obj).remove();
                            model.save({}, {
                                success: _.bind(function (response) {
                                    console.log('[/jsui/tree.jstree/create] - Saving is a success', response);
                                    console.log("trigger on event proxy");
                                    this.trigger('CreateItem', { model: this.DataModel });
                                    this.collection.sync();
                                    if (this.config.Events) {
                                        if (this.config.Events.Create) {
                                            this.config.Events.Create({ name: data.rslt.name, element: data.rslt.obj, parent: data.rslt.parent });
                                        }
                                    }
                                }, this)
                            });

                            //this.collection.add(model);
                            

                        }
                    }, this)).bind("remove.jstree", function (e, data) {
                        console.log("remove node", { e: e, data: data });
                    }).bind("rename.jstree", function (e, data) {
                        console.log("rename node", { e: e, data: data });
                    }).bind("move_node.jstree", function (e, data) {
                        console.log("move node", { e: e, data: data });
                    });

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
                $(this.root).find(".jstree-loading").parent().remove();
            },
            OnDataRemove: function (model) {
                $(this.root).jstree("remove", "#" + model.get(this.config.PrimaryKey));

            },
            OnDispose: function () {

            }

        });
    });

