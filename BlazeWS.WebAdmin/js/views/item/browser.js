define(['underscore', 'jsui/controls/Templated',
    'model/Item',
    'text!templates/item/browser.html',
    'collections/Items',
    'jquery',
    'jquery-ui'],
    function (_, TemplateControl, ItemModel, template, Items, $) {
        return TemplateControl.extend({
            collection: {},
            events: {
                'click button.btnCreateNewItem': 'btnCreateNewItem_OnClick'
            },
            OnInitialise: function () {
                _.bindAll(this, 'btnCreateNewItem_OnClick', 'btnCancel_OnClick'); // fixes loss of context for 'this' within methods

                //Need to get application id and root item

                this.collection = new Items;
                currentApplication = app.Data.System.get("currentApplication");

                this.template = template;
                
                console.log("[/view/item/browser/OnInitialise]", this);
            },
            OnCreateControl: function (name, control) {
                if (name == "ctlItemTree") {
                   

                    control.Configure({
                        TextField: 'Name',
                        collection: this.collection,
                        PrimaryKey: 'Id',
                        ParentKey: 'Parent',
                        theme: {
                            open: 'ui-icon-triangle-1-se',
                            closed: 'ui-icon-triangle-1-e',


                        },
                        Editable: false,
                        Selectable: {
                            OnSelect: _.bind(function (model) {
                                
                                var itemEditor = this.getChildByName('ctlItemEditor');
                                console.log("Browser OnSelect", {model: model, itemeditor: itemEditor });
                                itemEditor.SetDataSource(model);

                            }, this)

                        }
                    });
                    control.collection.fetch({
                        data: {
                            Id: currentApplication.get("BaseItem"),
                            Application: currentApplication.get("Id"),
                            GetChildren: false
                        }, success: function () {


                        }

                    });
                }

            },
            OnAfterRender: function () {

                $(this.el).find('.btnCreateNewItem').button();

                $(this.el).dialog({
                    width: 650,
                    title: "Item Browser"
                });



            },
            OnDispose: function () {
            },

            btnCreateNewItem_OnClick: function () {
                //Get Selected Item Id
                //Fire Create Function
                app.Logic.Router.loadView("item", "create");

            },
            btnCancel_OnClick: function () {
                this.$el.dialog("close");
                this.dispose();

            }
        });
    });

