define(['underscore', 'jsui/Control', 'jsui/Tree',
    'model/Item',
    'text!templates/item/browser.html',
    'collections/Items',
    'jquery',
    'jquery-ui'],
    function (_, Control,Tree, ItemModel, template,Items, $) {
        return Control.extend({
            config: {
                PrimaryKey: 'Id',
                ParentId: 'Parent',
            },
            collection: {},
            //DataSource: {},
            events: {
                //'click button#btnSave': 'btnSave_OnClick'
            },

            OnInitialise: function () {
                  _.bindAll(this, 'btnSave_OnClick', 'btnCancel_OnClick'); // fixes loss of context for 'this' within methods

                //Need to get application id and root item
                //create expand and contract functionality in the tree class

                this.collection = new Items;
                currentApplication = app.Data.System.get("currentApplication");

                this.ctlTree = new Tree;
                this.ctlTree.Configure({
                    TextField: 'Name',
                    collection: this.collection,
                    PrimaryKey: 'Id',
                    ParentKey: 'Parent',
                    theme: {
                        open: 'ui-icon-triangle-1-se',
                        closed: 'ui-icon-triangle-1-e',


                    },
                    Editable: true
                });
                
                this.addChild("ctlTree",this.ctlTree);
                this.ctlTree.collection.fetch({
                    data: {
                        Id: currentApplication.get("BaseItem"),
                        Application: currentApplication.get("Id"),
                        GetChildren: false
                    }, success: function () {


                    }
                });
                console.log("[/view/item/browser/OnInitialise]", this);
                //   this.model.
            },
            OnRender: function () {

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


                this.$el.dialog({
                    title: this.model.title,
                    buttons: {
                       // "Save": this.btnSave_OnClick,
                        "Close": this.btnCancel_OnClick
                    },
                    close: this.btnCancel_OnClick,

                });



            },
            OnDispose: function () {
             //   this.modelBinder.unbind();
            },

            btnSave_OnClick: function () {
                //this.DataSource.save(
                //    {
                //        success: function (response) {
                //            console.log("[view/Item/browser] - Saving is a success", response);
                //        }
                //    });

                this.$el.dialog("close");
            },
            btnCancel_OnClick: function () {
               // app.Data.Applications.remove(this.DataSource);
                this.$el.dialog("close");
                this.dispose();

            }
        });
    });

