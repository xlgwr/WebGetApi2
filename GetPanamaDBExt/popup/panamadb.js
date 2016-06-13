$(function() {
    var $currGetCountIndex = 0
    var allCountries = {}
    var getNowCountries = ''
    var getNowPage = 0
    console.log('开始获取网页：https://panamadb.org/ 的数据.')

    // 获取最后新增国家记录
    // 获取参数
    $.ajax({
        type: 'GET',
        timeout: 50000,
        url: config.getApi_getLast
    }).done(function(data) {
        console.log(data)
        if (data) {
            // todo something
            getNowCountries = data.Countries
            getNowPage = (data.getPage && data.getPage > 0) ? data.getPage : 0
        }
        // 获取所有国家
        $.ajax({
            type: 'GET',
            timeout: 50000,
            url: configGetUrl.panamadbHome
        }).done(function(data) {
            // todo something 
            // 获取得到的网页
            console.log(this.url)
            var $body = $('<div></div>').html(data)
            var $getAlla = $body.find('a[class="list-group-item"][href^="/country/"]')
                // console.log(data.length)
            console.log('国家个数:' + $getAlla.length)
            for (var index = 0; index < $getAlla.length; index++) {
                var a = $getAlla.eq(index)
                var span = a.find('span[class="badge"]').eq(0).text()
                var asize = span ? parseInt(span, 10) : 0
                var ahref = unescape(decodeURI(a.attr('href')))
                var aname = ahref.split('/')[2]
                allCountries[index] = {
                    name: aname,
                    size: asize,
                    pages: Math.ceil(asize / 50)
                }
            }
            console.log(allCountries)
            console.log('3:' + allCountries[3])

            // 从国家开始获取数据
            // /////////////////////////////////////////
            getCountires()

            function getCountires() {
                var tmpCountName = allCountries[$currGetCountIndex]
                console.log('开始获取国家：' + tmpCountName.name + ',有记录数：' + tmpCountName.size + ',页数：' + tmpCountName.pages)

                var currRunPage =

                    // 每10取一页
                    getPageData()

                // 获取明细记录
                function getPageData() {
                    $.ajax({
                        type: 'GET',
                        data: {
                            page: currRunPage
                        },
                        dataitem: tmpCountName,
                        timeout: 50000,
                        url: configGetUrl.panamadbHome + '/country/' + tmpCountName.name
                    }).done(function(data) {
                        console.log(this.url)
                        var $body = $('<div></div>').html(data)
                        var $getAlla = $body.find('a[class="list-group-item"][href^="https://panamadb.org"]')
                            // console.log(data)
                        console.log('国家:' + this.dataitem.name + '，当前页有记录数:' + $getAlla.length)

                        for (var index = 0; index < $getAlla.length; index++) {
                            var a = $getAlla.eq(index)
                            var ahref = unescape(decodeURI(a.attr('href')))
                            var atype = ahref.split('/')[3];
                            var aname = ahref.split('/')[4].split('_')[1];
                            var anameDesc = ahref.split('/')[4].split('_')[0];
                        }
                    }).fail(function(err) {
                        console.log('Error:' + this.url)
                        console.log(err)
                    })
                }
                // ///////////////////////
            }
            // /////////////////////////////////////////////////
        }).fail(function(err) {
            console.log(this.url)
            console.log(err)
        })

        // $.ajax({
        //    type: 'GET',
        //    timeout: 50000,
        //    url: config.getApi_getLast
        // }).done(function (data) {
        //     console.log(this.url)
        // }).fail(function (err) {
        //   console.log("Error:" + this.url)
        //    console.log(err); 
        // })

    }).fail(function(err) {
        console.log(err)
    })
})