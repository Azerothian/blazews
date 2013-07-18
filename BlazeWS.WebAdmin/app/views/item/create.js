define(['underscore','jsui/Control',
    'model/Item',
    'text!templates/item/create.html',
    'jquery',
    'jquery-ui'],
    function (_, Control, ItemModel, template, $) {
        return Control.extend({
            DataModel: {},
            events: {
                'click button#btnSave': 'btnSave_OnClick'
            },

            OnInitialise: function () {
                this.DataModel = new ItemModel;
                var currentApplication = app.Data.System.GetCurrentApplicationId();
                this.DataModel.set('Application', currentApplication);
                _.bindAll(this, 'btnSave_OnClick', 'btnCancel_OnClick'); // fixes loss of context for 'this' within methods

                console.log('[/view/item/create/OnInitialise]', this);
            },
            OnRender: function () {
                
                this.$el.html(template);

            },
            OnAfterRender: function () {
                

                this.setModel('title', 'Create Item');

                console.log('[/view/item/create/OnAfterRender]', this);
                this.modelBinder.bind(this.DataModel, this.el);

                if (this.DataModel.get('Id')) {
                    this.setModel('title', 'Edit Item');
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
            OnDispose: function() {
                this.modelBinder.unbind();
                this.$el.dialog('close');
            },

            btnSave_OnClick: function () {
                this.DataModel.save({},
                    {
                        success: _.bind(function (response) {
                            console.log('[/view/item/create] - Saving is a success', response);
                            
                            console.log("trigger on event proxy");
                            this.trigger('CreateItem', { model: this.DataModel });
                            this.dispose();
                        }, this)
                    });

               
            },
            btnCancel_OnClick: function () {
                this.dispose();

            }
        });
    });

