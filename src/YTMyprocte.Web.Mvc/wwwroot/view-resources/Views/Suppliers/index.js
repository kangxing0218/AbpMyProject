(function () {
    var _supplierService = abp.services.app.supplier;
    var _$minedatagrid = $('#dg');
    var _$minedialog = $("#dlg");
    var _$minetbr = $("#toolbar");
    $(function () {

        fLoadQueryData();
        fLoadDataGrid();

        $('#badd').click(function () {
            $('#dlg').dialog('open').dialog('setTitle', '创建供应商');
            var url = 'Suppliers/CreateOrUpdateModal/?id=';

            actionRoles("创建用户信息", "恭喜您，创建成功！", null, url);
        });
        $('#bedit').click(function () {
            var node = _$minedatagrid.datagrid('getSelected');
            if (node) {
                //var args = { "id": node.Id };
                var url = 'Suppliers/CreateOrUpdateModal/?id=' + node.id;

                actionRoles("编辑供应商信息", "恭喜您，修改成功！", node.id, url);

            } else {
                // abp.message.warn('请选择要操作的记录', '提示');
                $.messager.show({
                    title: "提示",
                    msg: '请选择要操作的记录'
                });
            }
        });
        $('#bdel').click(function () {
            //选中要删除行，if语句判断是否选中
            var dataSelected = $("#dg").datagrid("getSelected");
            if (dataSelected !== null) {//取到选中数据
                $.messager.confirm('确认', '您确认想要删除该供应商吗？', function (r) {
                    if (r) {
                        _supplierService.deleteSupplier({
                            id: dataSelected.id
                        }).done(function () {
                            freshPage();
                            $.messager.show({
                                title: "提示",
                                msg: '恭喜您，删除成功！'
                            });
                        });
                    }
                });
            } else//未选中需编辑数据行
            {
                $.messager.show({
                    title: '删除',
                    msg: '请选择要删除的供应商！'
                });
            }

        });
 


    function fLoadDataGrid() {
        abp.ui.setBusy(_$minedatagrid);
        _$minedatagrid.datagrid({

            abpMethod: _supplierService.getPagedSuppliers
            //queryParams: _$minetbr.find('form').serializeFormToObject()
        });
        abp.ui.clearBusy(_$minedatagrid);
    }
    function fLoadQueryData() {
        function onComboboxHidePanel() {
            var el = $(this);
            el.combobox('textbox').focus();
            // 检查录入内容是否在数据里
            var opts = el.combobox("options");
            var data = el.combobox("getData");
            var value = el.combobox("getValue");
            // 有高亮选中的项目, 则不进一步处理
            var panel = el.combobox("panel");
            var items = panel.find(".combobox-item-selected");
            if (items.length > 0) {
                var values = el.combobox("getValues");
                el.combobox("setValues", values);
                return;
            }
            var allowInput = opts.allowInput;
            if (allowInput) {
                var idx = data.length;

                data[idx] = [];
                data[idx][opts.textField] = value;
                data[idx][opts.valueField] = value;
                el.combobox("loadData", data);
            } else {
                // 不允许录入任意项, 则清空
                el.combobox("clear");
            }
        }
    }    
        
       
    function actionRoles(title, message, id, data) {
        // 执行一些动作...
        console.info("id", id);
        console.info("url", data);
        _$minedialog.dialog('open').dialog('refresh', data).dialog('setTitle', title);
        //console.info("表单", _$form);
        //  abp.ui.setBusy(_$minedialog);
        $('.saveMine').unbind("click").click(function () {
            var _$form = _$minedialog.find('form');
            //  console.info("表单", _$form);
            if (!_$form.form('validate')) {

                return;
            }
            abp.ui.setBusy(_$minedialog);
            // _$form.validate();
            var supplier = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js   
            console.info("供应商信息", supplier);
            _supplierService.createOrUpdateSupplier({
                supplierEditDto: supplier

            }).done(function () {
                _$minedialog.dialog('close');
                freshPage(); //reload page to see new user!

                // abp.notify.success(message);
                $.messager.show({
                    title: "提示",
                    msg: message
                });
            }).always(function () {
                abp.ui.clearBusy(_$minedialog);
            });
        });

    }
    function freshPage() {
        _$minedatagrid.datagrid("reload"); //reload page to see new user!
    }

    _$minedatagrid.datagrid({
        method: 'post',
        fit: false,
        sortName: 'CreationTime',
        sortOrder: 'desc',
        idField: 'id',
        pageSize: 10,
        pageList: [10, 20, 30, 40, 50],
        pagination: true,
        singleSelect: true,//单选模式
        fitColumns: true,
        rownumbers: true,//行号
        toolbar: "#toolbar",
        frozenColumns: [[
            { field: 'id', title: '主键', width: 80, hidden: true },
            { field: 'code', title: '编码', width: 80 },
            { field: 'supplierName', title: '供应商名称', width: 120, sortable: true },
        ]],
        columns: [[
            { field: 'personTel', title: '联系方式', width: 120, sortable: true },
            {
                field: 'supplierAddr', title: '联系地址', width: 120, 
                formatter: function (value, row, index) {
                    return '<span title=' + value + '>' + value +'</sapn > '
                    }

                   
            },
            
        ]]
        });


        //前端数据 验证  sell

        //自定义金额校验规则
        var postReg = /^[1][3,4,5,7,8][0-9]{9}$/;
        $.extend($.fn.validatebox.defaults.rules, {
            telePhone: {
                validator: function (value, param) {
                    return postReg.test(value);
                },
                message: '联系方式输入有误！'
            }

        }); 
    });
})();