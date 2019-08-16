(function () {
    var _purchaseOrderService = abp.services.app.purchaseOrder;
    var _$minedatagrid = $('#dg');
    var _$minedialog = $("#dlg");
    var _$minetbr = $("#toolbar");
  
   
    
    $(function () {

        fLoadQueryData();
        fLoadDataGrid();

        $('#btnadd').click(function () {
            $('#dlg').dialog('open').dialog('setTitle', '创建入库信息');
            var url = 'PurchaseOrders/CreateOrUpdateModal/?id=';

            actionRoles("创建入库单信息", "恭喜您，创建成功！", null, url);
        });

        $('#btnedit').click(function () {
          
            var node = _$minedatagrid.datagrid('getSelected');
            if (node) {
                //var args = { "id": node.Id };
                var url = 'PurchaseOrders/CreateOrUpdateModal/?id=' + node.id;

                actionRoles("编辑入库单信息", "恭喜您，修改成功！", node.id, url);

            } else {
                $.messager.show({
                    title: "提示",
                    msg: '请选择要操作的记录'
                });
            }
        });
        $('#btnremove').click(function () {
            //选中要删除行，if语句判断是否选中
            var dataSelected = $("#dg").datagrid("getSelected");
            if (dataSelected !== null) {//取到选中数据
                $.messager.confirm('确认', '您确认想要删除该供应商吗？', function (r) {
                    if (r) {
                        _purchaseOrderService.deletePurchaseOrder({
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
                    msg: '请选择要删除的记录！'
                });
            }

        });
        $('#bruku').click(function () {
            var dataSelected = $("#dg").datagrid("getSelected");
            if (dataSelected !== null) {//取到选中数据
                if (dataSelected.isInbound == true) {
                    $.messager.show({
                        title: '提示',
                        msg: '采购单已入库！'
                    });
                }
                else {
                    $.messager.confirm('确认', '您确认想要该采购单入库吗？', function (r) {
                        if (r) {
                            _purchaseOrderService.updateInboundStatusUpdateOrder({
                                id: dataSelected.id
                            }).done(function () {
                                freshPage();
                                $.messager.show({
                                    title: "提示",
                                    msg: '恭喜您，入库成功！'
                                });
                            });
                        }
                    });
                }

            } else//未选中需编辑数据行
            {
                $.messager.show({
                    title: '提示',
                    msg: '请选择要入库的采购单！'
                });
            }
        });



    function fLoadDataGrid() {
        abp.ui.setBusy(_$minedatagrid);
        _$minedatagrid.datagrid({
            //首页的分页
            abpMethod: _purchaseOrderService.getPagedOrders
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

        // 初始化搜索相关按钮
        _$minetbr.find(".btn-search").linkbutton({
            width: '80',
            iconCls: 'icon-search'
        }).bind('click', function () {
            //过滤条件         
            if (!_$minetbr.find('form').form('validate')) {
                return;
            }
            var qC = _$minetbr.find('form').serializeFormToObject();
            _$minedatagrid.datagrid("load", qC);
        });

        //清空条件
        _$minetbr.find(".btn-clean").linkbutton({
            width: '80',
            iconCls: 'icon-delete'
        }).bind('click', function () {
            var $form = $(this).closest("form");
            if ($form.length > 0) {
                $form.form('reset');
            }
            var qC = _$minetbr.find('form').serializeFormToObject();
            _$minedatagrid.datagrid("load", qC);

        });

    }    
    
    function actionRoles(title, message, id, data) {
        // 执行一些动作...
        console.info("id", id);
        console.info("url333", data);
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
            var purchaseOrder = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js   
            console.info("库存信息", purchaseOrder);
            var rows = $('#orderTable').datagrid('getRows');
            console.info("当前选中节点", rows)
            if (rows.length == 0) {
                _$minedialog.dialog('close');
            }
            else {
                var str = [];
                for (var i = 0; i < rows.length; i++) {
                    
                    var arr = {};
                    arr.goodsId = rows[i].goodsId;
                    arr.materielCode = rows[i].materielCode;
                    arr.goodsName = rows[i].goodsName;
                    arr.waresFomat = rows[i].waresFomat;
                    arr.goodsUnit = rows[i].goodsUnit;
                    arr.count = rows[i].count;
                    arr.unitPrice = rows[i].unitPrice;
                    arr.totalPrice = rows[i].totalPrice;
                    str[i] = arr;
                }
                console.log(str);
                _purchaseOrderService.createOrUpdateOrder({
                    purchaseOrderEditDto: purchaseOrder,
                    orderDetails: str

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
            }
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
        fitColumns: true,
        rownumbers: true,//行号
        toolbar: "#toolbar",
        frozenColumns: [[
            { field: 'id', title: '主键', width: 80, hidden: true },
            {
                field: 'code', title: '采购单号', width: 200, sortable: true,
                formatter: function (value, rowData, index) {
                    return '<a style="color:blue" href="PurchaseOrders/OrderDetails?orderCode=' + rowData.code + '" > ' + rowData.code + '</a > ';
                }
            },
            { field: 'supplierName', title: '供应商名称', width: 250, sortable: true },
            { field: 'creationTime', title: '采购日期', width: 150, sortable: true },
            { field: 'price', title: '订单总金额', width: 120, sortable: true },
            { field: 'creationTime', title: '创建时间', width: 120 },
            {
                field: 'isInbound', title: '入库状态', width: 120,
                formatter: function (value, rowData, index) {
                    if (value == true) {
                        return "已入库";
                    } else {
                        return "未入库";
                    }
                }
            }
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

/**
 * createEDitModal
 * 
 * */


   
})();


