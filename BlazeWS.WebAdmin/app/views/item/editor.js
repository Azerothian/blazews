define(['underscore','jsui/Control',
    'model/Item',
    'jsui/controls/Templated',
    'text!templates/item/editor.html',
    'jquery',
    'jquery-ui'],
    function (_, Control, ItemModel,TemplateControl,  template, $, jsoneditor) {
        return TemplateControl.extend({
            isBound: false,
            //DataSource: {},
            events: {
                'click button.btnSaveDetails': 'btnSave_OnClick',
            },

            OnInitialise: function () {
                this.isBound = false;
                _.bindAll(this, 'SetDataSource', 'btnSave_OnClick'); // fixes loss of context for 'this' within methods
                this.template = template;
                
            },
            
            OnCreateControl: function (name, control) {
                if (name == "jedObjectData") {
                    this.jedObjectData = control;
                    if (this.DataSource) {
                        this.jedObjectData.Configure({
                            DataSource: this.DataSource,
                            Key: "JsonData"

                        });
                    }
                    
                }
            },
            SetDataSource: function (source) {
                if (this.isBound) {
                    this.modelBinder.unbind();
                    this.stopListening(this.DataSource);
                }
                
                this.DataSource = source;
                this.listenTo(this.DataSource, 'change', this.OnDataChange);
                this.modelBinder.bind(this.DataSource, this.el, {

                    Id: '[name=Id]',
                    Name: '[name=Name]',
                    Parent: '[name=Parent]',
                    Path: '[name=Path]',
                    JsonDataType: '[name=JsonDataType]',
                    Type: { selector: '[name=Type]' }
                   // ,ObjectData: '[name=ObjectData]'
                });
                this.isBound = true;
                this.jedObjectData.Configure({
                    DataSource: this.DataSource,
                    Key: "JsonData",
                    Stringify: true
                });
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
                this.jedObjectData.Update();
                this.DataSource.save({},
                    {
                        success: _.bind(function (response) {
                            console.log('[/view/item/create] - Saving is a success', response);
                            
                        }, this)
                    });

               
            },
            OnDataChange: function () {
                $(this.el).find(".btnSaveDetails").removeAttr('disabled');
            }
        });
    });

