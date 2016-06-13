$(function () {
    console.log("开始获取网页：https://panamadb.org/ 的数据 OK.");

    //获取最后新增国家记录
    //获取参数
    $.ajax({
        type: 'GET',
        timeout: 50000,
        url: config.getApi_getLast
    }).done(function (data) {

        var getNowCountries = "";
        var getNowPage = 0;

        console.log(data);
        if (data.Countries) {
            //todo something
            getNowCountries = data.Countries;
            getNowPage = (data.getPage && data.getPage > 0) ? data.getPage : 0;
        }
        // 获取所有国家
        $.ajax({
            type: 'GET',
            timeout: 50000,
            url: configGetUrl.panamadbHome
        }).done(function (data) {
            //todo something 
            //获取得到的网页
            console.log(this.url);
            var $body = $('<div></div>').html(data);
            console.log(data);


        }).fail(function (err) {
            console.log(err);
        });



        //$.ajax({
        //    type: 'GET',
        //    timeout: 50000,
        //    url: config.getApi_getLast
        //}).done(function (data) {


        //}).fail(function (err) {
        //    console.log(err); 
        //});

    }).fail(function (err) {
        console.log(err);
    });
});