define(['jsui/Control', 'jsui/controls/DataTable',
    'text!templates/user/editor.html',
    'jquery', 'collections/Users'],
    function (Control, DataTable, template, $, Users) {
        return Control.extend({
            ctlDataTable: {},
            events: {

            },

            OnInitialise: function () {
                _.bindAll(this, "OnCreateUser");
                this.setModel('title', 'User Browser');
                this.ctlDataTable = new DataTable;
                this.ctlDataTable.Configure({
                    editable: {
                        Buttons: {
                            Delete: true,
                            Commit: true,
                            Save: true
                        }
                    },
                    collection: new Users,
                    columns: [
                        { Header: { Title: "Name" }, Data: "Name", Index: 1, Editable: { type: 'text' } },
                        { Header: { Title: "Data" }, Data: "ObjectData", Index: 2, Editable: true }

                    ]
                });
                jsui.events.on('user:create',
                    _.bind(function (model) {
                        this.ctlDataTable.collection.fetch({
                            data: { Application: app.Data.System.GetCurrentApplicationId() },
                            success: _.bind(function () {

                                // On User Create!!
                            }, this)
                        });
                    }, this),
                    this);

                this.ctlDataTable.collection.fetch({
                    data: { Application: app.Data.System.GetCurrentApplicationId() },
                    success: _.bind(function () {
                        this.addChild("ctlDataTable", this.ctlDataTable);
                    }, this)
                });

            },
            OnRender: function () {
                this.btnCreate = $("<input type='button'/>");
                $(this.el).append(this.btnCreate);
                $(this.btnCreate).button();
                $(this.btnCreate).click(_.bind(function () {
                    app.Logic.Router.loadView("user", "create");

                }, this));
                $(this.btnCreate).attr('value', 'Create New User');


            }, OnCreateUser: function (data) {

            },
            OnAfterRender: function () {
                this.$el.dialog({
                    title: this.getModel('title'),
                    width: 550,
                    height: 350,
                    close: this.dispose
                });


            },
            OnDispose: function () {
                jsui.events.off(null, null, this);
                this.$el.dialog("close");
            }

        });
    });

