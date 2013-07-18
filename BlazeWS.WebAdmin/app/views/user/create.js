define(['underscore','jsui/Control',
    'model/User',
    'text!templates/user/create.html',
    'jquery',
    'jquery-ui'],
    function (_, Control, UserModel, template, $) {
        return Control.extend({
            DataSource: {},
            events: {
                'click button#btnSave': 'btnSave_OnClick'
            },

            OnInitialise: function () {
                this.DataSource = new UserModel;
                //app.Data.Users.add(this.DataSource);
                var currentApplication = app.Data.System.get('currentApplication');
                this.DataSource.set('Application', currentApplication.get('Id'));
                _.bindAll(this, 'btnSave_OnClick', 'btnCancel_OnClick'); // fixes loss of context for 'this' within methods

                console.log('[/view/application/create/OnInitialise]', this);
                jsui.events.on("create:user", function () {
                    console.log("Event Tracking Create User", this);

                });
                //   this.model.
            },
            OnRender: function () {
                
                this.$el.html(template);

            },
            OnAfterRender: function () {
                

                this.setModel('title', 'Create User');

                console.log('[/view/user/create/OnAfterRender]', this);
                this.modelBinder.bind(this.DataSource, this.el);

                if (this.DataSource.get('Id')) {
                    this.setModel('title', 'Edit User');
                }


                this.$el.dialog({
                    title: this.getModel("title"),
                    buttons: {
                        'Save': this.btnSave_OnClick,
                        'Cancel': this.btnCancel_OnClick
                    },
                    close: this.btnCancel_OnClick,
                    width: 350,
                    height: 250

                });

                
                
            },
            OnDispose: function () {

                this.$el.dialog('close');
                this.modelBinder.unbind();
            },

            btnSave_OnClick: function () {
                this.DataSource.save({},
                    {
                        success: _.bind(function (response) {
                            console.log('[view/user/btnSave_OnClick/save] - Saving is a success', response);
                            console.log("[view/user/btnSave_OnClick/save/trigger/user:create]");
                            jsui.events.trigger('user:create', this.DataModel );
                            this.dispose();
                        }, this)
                    });

               
            },
            btnCancel_OnClick: function () {
                this.dispose();

            }
        });
    });

