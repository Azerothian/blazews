define(['underscore','jsui/Control',
    'model/Application',
    'text!templates/application/create.html',
    'jquery',
    'jquery-ui'],
    function (_, Control, ApplicationModel, template, $) {
        return Control.extend({
            DataSource: {},
            events: {
                'click button#btnSave': 'btnSave_OnClick'
            },

            OnInitialise: function () {
                this.DataSource = new ApplicationModel;
                app.Data.Applications.add(this.DataSource);
                _.bindAll(this, 'btnSave_OnClick', 'btnCancel_OnClick'); // fixes loss of context for 'this' within methods

                console.log("[/view/application/create/OnInitialise]", this);

                //   this.model.
            },
            OnRender: function () {
                
                this.$el.html(template);

            },
            OnAfterRender: function () {
                

                this.model.title = "Create Application";

                console.log("[/view/application/create/OnAfterRender]", this);
                this.modelBinder.bind(this.DataSource, this.el);

                if (this.DataSource.get('Id')) {
                    this.model.title = "Edit Application";
                    this.DataSource.fetch();
                }


                this.$el.dialog({
                    title: this.model.title,
                    buttons: {
                        "Save": this.btnSave_OnClick,
                        "Cancel": this.btnCancel_OnClick
                    }

                });

                
                
            },
            OnDispose: function() {
                this.modelBinder.unbind();
            },

            btnSave_OnClick: function () {
                this.DataSource.save(
                    {
                        success: function (response) {
                            console.log("[view/Application/create] - Saving is a success", response);
                        }
                    });

                this.$el.dialog("close");
            },
            btnCancel_OnClick: function () {
                app.Data.Applications.remove(this.DataSource);
                this.$el.dialog("close");
                this.dispose();

            }
        });
    });

