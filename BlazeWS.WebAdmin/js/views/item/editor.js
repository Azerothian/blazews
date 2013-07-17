define(['underscore','jsui/Control',
    'model/Item',
    'text!templates/item/editor.html',
    'jquery',
    'jsoneditor/jsoneditor',
    'jquery-ui'],
    function (_, Control, ItemModel, template, $, jsoneditor) {
        return Control.extend({
            isBound: false,
            //DataSource: {},
            events: {
                'click button.btnSaveDetails': 'btnSave_OnClick'
            },

            OnInitialise: function () {
                this.isBound = false;
                _.bindAll(this, 'SetDataSource', 'btnSave_OnClick'); // fixes loss of context for 'this' within methods
            },
            OnRender: function () {
                
                this.$el.html(template);

            },
            SetDataSource: function (source) {
                if (this.isBound) {
                    this.modelBinder.unbind();
                }
                this.DataSource = source;
                this.modelBinder.bind(this.DataSource, this.el);
                this.isBound = true;
            },
            OnAfterRender: function () {
                $(this.el).find(".btnSaveDetails").button();
            },
            OnDispose: function () {
                if (this.isBound) {
                    this.modelBinder.unbind();
                }
            },

            btnSave_OnClick: function () {
                this.DataSource.save({},
                    {
                        success: _.bind(function (response) {
                            console.log('[/view/item/create] - Saving is a success', response);
                            
                        }, this)
                    });

               
            }
        });
    });

