(function () {
    var _materielService = abp.services.app.materiel;
    let $mgtbr = $("#toolbar");
    let $mgrid = $("#dg");
    let $minedialog = $('#dlg');
    $(function () {
           //创表
            $('#dg').datagrid({
                abpMethod: _materielService.getPagedMateriel,
                queryParams: $mgtbr.find('form').serializeFormToObject(),
                method: 'post',
                //url: '/Customers/GetPagedCustomers',
                idField: 'id',
                fit: false,
                sortName: 'CreationTime',
                sortOrder: 'desc',
                pageSize: 10,
                pageList: [10, 20, 30, 40, 50],
                pagination: true,
                singleSelect: true,//单选模式
                rownumbers: true,
                fitColumns:true,
                toolbar: $mgtbr,
                
                frozenColumns: [[
                    { field: 'id', title: '主键', width: 80, hidden: true },
                    { field: 'code', title: '编码', width: 80 },
                    { field: 'waresName', title: '商品名称', width: 120, sortable: true }
                ]],
                columns: [[
                    { field: 'waresType', title: '型号', width: 80, sortable: true },
                    { field: 'waresFomat', title: '规格', width: 80, sortable: true },
                    { field: 'waresUnit', title: '单位', width: 80, sortable: true },
                    { field: 'manufacturer', title: '生产厂家', width: 80, sortable: true },
                    { field: 'brands', title: '品牌', width: 80, sortable: true },
                    { field: 'buyMoney', title: '采购价', width: 80, sortable: true },
                    { field: 'sellMoney', title: '销售价', width: 80, sortable: true },
                ]]
            });
        
       //增加用户 
        $('#btnadd').click(function () {

            $('#dlg').dialog('open').dialog('setTitle', '创建物料信息');
            var url = 'Materiels/CreateOrUpdateModal/?id=';          

            actionRoles("创建物料信息", "恭喜您，创建成功！", null, url);
        });
        
        
        //修改用户
        $('#btnedit').click(function () {
            var node = $mgrid.datagrid('getSelected');
            if (node) {
              
                var url = 'Materiels/CreateOrUpdateModal/?id=' + node.id;
                actionRoles("编辑物料信息", "恭喜您，修改成功！", node.id, url);

            } else {
                
                $.messager.show({
                    title: "提示",
                    msg: '请选择要操作的记录'
                });
            }
        });
        //删除用户
        $('#btnremove').click(function () {
            var row = $mgrid.datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '您确定要删除这个物料信息吗?', function (r) {
                    if (r) {
                        _materielService.deleteMateriel({
                            id: row.id
                        }).done(function (result) {
                            freshPage();
                                $.messager.show({    // show error message
                                    title: '提示',
                                    msg: '您好，删除成功'
                                });
                            
                        });
                    }
                });
            } else {
                $.messager.show({    // show error message
                    title: '删除',
                    msg: '请选择用户删除!'
                });
            }
        });
        function freshPage() {
            $mgrid.datagrid("reload"); //reload page to see new user!
        }


       

        function actionRoles(title, message, id, data) {
            // 执行一些动作...
            console.info("id", id);
            console.info("url", data);
            $minedialog.dialog('open').dialog('refresh', data).dialog('setTitle', title);
            //console.info("表单", _$form);
            //  abp.ui.setBusy(_$minedialog);
            $('.saveMine').unbind("click").click(function () {
                var _$form = $minedialog.find('form');
                //  console.info("表单", _$form);
                if (!_$form.form('validate')) {

                    return;
                }
                abp.ui.setBusy($minedialog);
                // _$form.validate();
                var customer = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js   
                console.info("物料信息", customer);
                _materielService.createOrUpdateMateriel({
                    //customerEditDto:跟CreateOrUpdateCustomerInput中的
                    //public SupplierEditDto supplierEditDto { get; set; }有关
                    materielEditDto: customer

                }).done(function () {
                    $minedialog.dialog('close');
                    freshPage(); //reload page to see new user!

                    // abp.notify.success(message);
                    $.messager.show({
                        title: "提示",
                        msg: message
                    });
                }).always(function () {
                    abp.ui.clearBusy($minedialog);
                });
            });
        }

        //前端数据 验证  sell
        
        //自定义金额校验规则
       
        var postReg = /^([1-9]\d*(\.\d*[1-9])?)|(0\.\d*[1-9])$/;
        $.extend($.fn.validatebox.defaults.rules, {
            sellMoney: {
                validator: function (value, param) {
                    return postReg.test(value);
                },
                message: '金额输入有误！'
            }
           
        }); 

    });
})();