(function () {
    var _customerService = abp.services.app.customer;
    let $mgtbr = $("#toolbar");
    let $mgrid = $("#dg");
    let $minedialog = $('#dlg');
    $(function () {

        
        //创表
            $('#dg').datagrid({
                abpMethod: _customerService.getPagedCustomers,
                queryParams: $mgtbr.find('form').serializeFormToObject(),
                method: 'post',
                //url: 'Customers/GetPagedCustomers',
                idField: 'id',
                fit: false,
                sortName: 'CreationTime',
                sortOrder: 'desc',
                singleSelect: true,//单选模式
                pageSize: 10,
                pageList: [10, 20, 30, 40, 50],
                pagination: true,
                rownumbers: true,
                fitColumns:true,
                toolbar: $mgtbr,
                columns: [
                            [{ title: '客户信息表', width: 80,colspan:5 }],
                            [
                                { field: 'id', title: '主键',hidden:true, width: 30, rowspan: 2 },
                                { field: 'code', title: '编码', align: 'center', width: 30,rowspan:2 },                        
                                { field: 'customerName', title: '客户名称', width: 30, align: 'center', sortable: true,rowspan:2 },
                                { title: '用户信息', width: 30, colspan: 2, align: 'center' }
                            ],
                            [//customerName     customerTel     
                                { field: 'customerTel', title: '联系电话', width: 30, sortable: true,align: 'center' },
                                { field: 'customerAddr', title: '联系地址', width: 30, sortable: true,align: 'center' }
                            ],
                          ]
            });
        
       //增加用户 
        $('#btnadd').click(function () {

            $('#dlg').dialog('open').dialog('setTitle', '创建客户');
            var url = 'Customers/CreateOrUpdateModal/?id=';          

           actionRoles("创建客户信息", "恭喜您，创建成功！", null, url);
        });
        
        
        //修改用户
        $('#btnedit').click(function () {
            var node = $mgrid.datagrid('getSelected');
            if (node) {
              
                var url = 'Customers/CreateOrUpdateModal/?id=' + node.id;

                actionRoles("编辑客户信息", "恭喜您，修改成功！", node.id, url);

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
                $.messager.confirm('Confirm', '您确定要删除这个用户吗?', function (r) {
                    if (r) {
                        _customerService.deleteCustomer({
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
                console.info("客户信息", customer);
                _customerService.createOrUpdateCustomer({
                    //customerEditDto:跟CreateOrUpdateCustomerInput中的
                    //public SupplierEditDto supplierEditDto { get; set; }有关
                    customerEditDto: customer

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