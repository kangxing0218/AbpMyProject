(function () {
    var _orderService = abp.services.app.sell;
    var _$minedatagrid = $('#dg');
    var _$minedialog = $("#dlg");
    var _$minetbr = $("#toolbar");
 
    $(function () {

        fLoadQueryData();
        fLoadDataGrid();

        $('#badd').click(function () {
            var url = 'SellOrders/CreateOrEditModal/?id=';

            actionRoles("创建销售订单信息", "恭喜您，创建成功！", null, url);
        });
        $('#bedit').click(function () {
            var node = _$minedatagrid.datagrid('getSelected');
            if (node) {
                
                    var url = 'SellOrders/CreateOrEditModal/?id=' + node.id;
                    actionRoles("编辑销售订单信息", "恭喜您，修改成功！", node.id, url);
                }
            else {
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
                $.messager.confirm('确认', '您确认想要删除该销售订单吗？', function (r) {
                    if (r) {
                        _orderService.deleteOrder({
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
                    msg: '请选择要删除的销售订单！'
                });
            }

        });
        //修改出库状态
        $('#bruku').click(function () {
            var dataSelected = $("#dg").datagrid("getSelected");
            if (dataSelected !== null) {//取到选中数据
                if (dataSelected.isOutbound == true) {
                    $.messager.show({
                        title: '提示',
                        msg: '销售订单已入库！'
                    });
                }
                else {
                    $.messager.confirm('确认', '您确认想要该销售订单入库吗？', function (r) {
                        if (r) {
                            _orderService.updateOutboundStatus({
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
            }
        });
    });

    function fLoadDataGrid() {
        abp.ui.setBusy(_$minedatagrid);
        _$minedatagrid.datagrid({
            //得到index页下的信息 抬头+金额
            abpMethod: _orderService.getOrders
          //  queryParams: _$minetbr.find('form').serializeFormToObject()
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
        console.info("url", data);
        _$minedialog.dialog('open').dialog('refresh', data).dialog('setTitle', title);
        console.log("asdasdas")
        $('.saveMine').unbind("click").click(function () {
            var _$form = _$minedialog.find('form');
            console.info("表单", _$form);
            if (!_$form.form('validate')) {

                return;
            }

            abp.ui.setBusy(_$minedialog);
            //_$form.validate();
            var order = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js   
            console.info("采购信息", order);
            //var rows = $('#orderTable').datagrid('getSelections');
            var rows = $('#orderTable').datagrid('getRows');
            console.info("当前选中节点", rows)
            if (rows.length == 0) {

                _$minedialog.dialog('close');

            }
            else {

                var str = [];
                for (var i = 0; i < rows.length; i++) {

                    var arr = {};
                    arr.materielId = rows[i].materielId;
                    arr.materielCode = rows[i].materielCode;
                    arr.materielName = rows[i].materielName;
                    arr.goodsFomat = rows[i].goodsFomat;
                    arr.goodsUnit = rows[i].goodsUnit;
                    arr.count = rows[i].count;
                    arr.sellMoney = rows[i].sellPrice;
                    arr.outPrice = rows[i].outPrice;
                    str[i] = arr;
                }
                console.log(str);
                _orderService.createOrUpdateOrder({
                    outSell: order,
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
        pageSize: 15,
        pageList: [15, 20, 30, 40, 50],
        pagination: true,
        fitColumns: true,
        singleSelect: true,//单选模式
        rownumbers: true,//行号
        toolbar: "#toolbar",
        frozenColumns: [[
            { field: 'id', title: '主键', width: 80, hidden: true },
            {
                field: 'code', title: '销售订单号', width: 200, sortable: true,
                formatter: function (value, rowData, index) {
                    return '<a style="color:blue" href="SellOrders/OrderDetails?orderCode=' + rowData.code + '" > ' + rowData.code + '</a > ';
                }
            },
            { field: 'customerName', title: '客户名称', width: 250, sortable: true },
            { field: 'creationTime', title: '销售日期', width: 150, sortable: true },
            { field: 'price', title: '总金额(元)', width: 120, sortable: true },
            {
                field: 'isOutbound', title: '出库状态', width: 120,
                formatter: function (value, rowData, index) {
                    if (value == true) {
                        return "已出库";
                    } else {
                        return "未出库";
                    }
                }
            }
        ]]
    });
})();