define(['underscore', 'jsui/controls/Templated', 'jsui/controls/Tree.jstree',
    'model/Item',
    'text!templates/item/browser.jstree.html',
    'collections/Items',
    'jquery',
    'jquery-ui'],
    function (_, TemplateControl, Tree, ItemModel, template, Items, $) {
        return TemplateControl.extend({
            collection: {},
            events: {
                //'click button#btnSave': 'btnSave_OnClick'
            },

            OnInitialise: function () {
                _.bindAll(this, 'btnClose_OnClick'); // fixes loss of context for 'this' within methods
                this.template = template;
                this.collection = new Items;
                //   this.model.
            },
            OnCreateControl: function (name, control) {

                if (name == "ctlItemTree") {

                    control.Configure({
                        TextField: 'Name',
                        collection: this.collection,
                        PrimaryKey: 'Id',
                        ParentKey: 'Parent',
                        Editable: false,
                        Events: {
                            Select: _.bind(function (model) {

                                var itemEditor = this.getChildByName('ctlItemEditor');
                                console.log("Browser Select", { model: model, itemeditor: itemEditor });
                                itemEditor.SetDataSource(model);

                            }, this),
                            Create: _.bind(function (create) {




                            }, this),
                            Expand: _.bind(function(id, collection){ 

                                collection.GetChildren({
                                    data: {
                                        ParentItem: id,
                                        Application: app.Data.System.GetCurrentApplicationId()
                                    }, success: _.bind(function () {


                                    }, this)

                                });

                            }, this)


                        }
                    });
                    currentApplication = app.Data.System.get("currentApplication");
                    control.collection.fetch({
                        data: {
                            Id: currentApplication.get("BaseItem"),
                            Application: currentApplication.get("Id"),
                            GetChildren: false
                        }, success: function () {


                        }

                    });
                }

                //                this.$el.append("");
                //this.$el.html();

            },
            OnAfterRender: function () {


                //this.model.title = "Create Application";

                //console.log("[/view/application/create/OnAfterRender]", this);
                //this.modelBinder.bind(this.DataSource, this.el);

                //if (this.DataSource.get('Id')) {
                //    this.model.title = "Edit Application";
                //    this.DataSource.fetch();
                //}
                $(this.el).find('.btnCreateNewItem').button();


                this.$el.dialog({
                    width: 650,
                    title: "Item Browser",
                    close: this.btnClose_OnClick,

                });



            },
            OnDispose: function () {
                //   this.modelBinder.unbind();
            },
            btnClose_OnClick: function () {
                // app.Data.Applications.remove(this.DataSource);
                this.$el.dialog("close");
                this.dispose();

            }
        });
    });

