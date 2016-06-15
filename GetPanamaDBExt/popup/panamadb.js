$(function() {

    $('#btn1hklaw').click(function() {
        $('#panelhklaw').removeClass('panel-default')
        $('#panelhklaw').addClass('panel-success')
        $(this).attr('disabled', 'disabled');
        var $msg = $('#msg1')
        $msg.removeClass('hide')
        getDatapanamadb($(this), $msg);
        //$(this).attr('disabled', null);
    });

    //获取下级明细,有国家
    getItems(config.getApi_topCountryItems, "有国家");
    //获取无国家资料
    getItems(config.getApi_topNoCountryItems, "无国家");

    /////////////获取国家明细
    function getDatapanamadb(btn, msgid) {
        var $currGetCountIndex = 0
        var allCountries = {}
        var $getNowCountries = ''
        var $getNowPage = 1
        var msg = "开始获取网页：https://panamadb.org/ 的数据.";
        console.log(msg)
        msgid.text(msg);

        // 获取最后新增国家记录
        // 获取参数
        $.ajax({
            type: 'GET',
            timeout: 80000,
            url: config.getApi_getLast
        }).done(function(data) {
            console.log(data)
            if (data) {
                // todo something
                $getNowCountries = data.Countries
                $currGetCountIndex = (data.CountriesIndex && data.CountriesIndex > 0) ? data.CountriesIndex : 0
                $getNowPage = (data.getPage && data.getPage > 1) ? data.getPage : 1
            }
            // 获取所有国家
            $.ajax({
                type: 'GET',
                timeout: 80000,
                url: configGetUrl.panamadbHome
            }).done(function(data) {
                // todo something 
                // 获取得到的网页
                console.log(this.url)
                var $body = $('<div></div>').html(data)
                var $getAlla = $body.find('a[class="list-group-item"][href^="/country/"]')
                    // console.log(data.length)
                msg = '国家个数:' + $getAlla.length;
                console.log(msg);
                msgid.text(msg);
                for (var index = 0; index < $getAlla.length; index++) {
                    var a = $getAlla.eq(index)
                    var span = a.find('span[class="badge"]').eq(0).text()
                    var asize = span ? parseInt(span, 10) : 0
                    var ahref = unescape(decodeURI(a.attr('href')))
                    var aname = ahref.split('/')[2]
                    allCountries[index] = {
                        index: index,
                        name: aname,
                        nameDesc: a.text().replace(asize, '').trim(),
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
                    console.clear();

                    if ($currGetCountIndex > $getAlla.length) {
                        console.log('国家数据已采集完.个数:' + $getAlla.length)
                        msgid.text('国家数据已采集完.个数:' + $getAlla.length);
                        btn.attr('disabled', null);
                        return
                    }

                    if (!allCountries[$currGetCountIndex]) {
                        console.log($currGetCountIndex + '：对应国家不存在。')
                        msgid.text($currGetCountIndex + '：对应国家不存在。')
                        $currGetCountIndex += 1
                        getCountires()
                        return
                    }

                    var tmpCountName = allCountries[$currGetCountIndex]
                    msg = '开始获取国家：' + tmpCountName.nameDesc + ',有记录数：' + tmpCountName.size + ',页数：' + tmpCountName.pages + ',Index:' + $currGetCountIndex;
                    console.log(msg);
                    msgid.text(msg);
                    var postPageOKFlag = false;
                    var postPageOK = $getNowPage > 0 ? $getNowPage - 1 : 0
                    var currRunPage = $getNowPage > 1 ? $getNowPage : 1
                    $getNowPage = 0
                        ////////////////////////////////
                    getPageData()

                    // 获取明细记录
                    function getPageData() {

                        if (currRunPage <= 0) {
                            currRunPage = 1
                        }

                        if (currRunPage > tmpCountName.pages) {
                            // console.clear()
                            console.log("CurrRunPage:" + currRunPage + ",All:" + tmpCountName.pages)
                            return
                        }

                        $.ajax({
                            type: 'GET',
                            data: {
                                page: currRunPage
                            },
                            tmppage: currRunPage,
                            tmpdata: tmpCountName,
                            timeout: 80000,
                            url: configGetUrl.panamadbHome + '/country/' + tmpCountName.name
                        }).done(function(data) {
                            console.log(this.url)
                            var $body = $('<div></div>').html(data)
                            var $getAlla = $body.find('a[class="list-group-item"][href^="https://panamadb.org"]')
                                // console.log(data)
                            msg = '国家:' + this.tmpdata.name + ', 有记录数：' + this.tmpdata.size + ', 页数：' + this.tmpdata.pages + ', 当前页:' + this.tmppage + '，当前页有记录数:' + $getAlla.length;
                            console.log(msg);
                            msgid.text(msg);
                            var postMain = []
                            for (var index = 0; index < $getAlla.length; index++) {
                                var a = $getAlla.eq(index)
                                var ahref = unescape(decodeURI(a.attr('href')))
                                var ah4 = a.find('h4').eq(0).text().trim()
                                var atype = ahref.split('/')[3]
                                var aname = ahref.split('/')[4].split('_')[1]
                                var anameDesc = ah4; // ahref.split('/')[4].split('_')[0]

                                var postItems = {
                                    Countries: this.tmpdata.name,
                                    CountriesDesc: this.tmpdata.nameDesc,
                                    CountriesIndex: this.tmpdata.index,
                                    ttype: atype,
                                    name: aname,
                                    nameDesc: anameDesc,
                                    nameURL: ahref,
                                    Tid: 0,
                                    Remark: this.url,
                                    getPage: this.tmppage,
                                    tStatus: 0,
                                    ClientIP: undefined,
                                    addDate: undefined,
                                    UpdateDate: undefined
                                }
                                postMain.push(postItems)
                            }
                            // console.log(postMain)

                            currRunPage = currRunPage + 1
                                //
                                // 提交数据库
                            $.ajax({
                                    type: 'POST',
                                    url: config.postApi_CountryItems,
                                    tmpdata: postMain,
                                    tmpdataCountry: tmpCountName,
                                    timeout: 80000,
                                    contentType: 'application/json; charset=utf-8',
                                    data: JSON.stringify(postMain)
                                }).done(function(data) {
                                    console.log(this.tmpdata[0].Countries + ',CurrPage:' + this.tmpdata[0].getPage + '--> Post Done!')
                                        // console.log(data);  
                                    postPageOK = postPageOK + 1
                                    console.log(postPageOK + ',All:' + this.tmpdataCountry.pages)
                                    if (postPageOK >= this.tmpdataCountry.pages) {
                                        postPageOK = 0;
                                        postPageOKFlag = true;
                                        // console.clear()
                                        msg = '************国家：' + this.tmpdataCountry.nameDesc + ',有记录数：' + this.tmpdataCountry.size + ',页数：' + this.tmpdataCountry.pages + '，已采集URL完。';
                                        console.log(msg);
                                        msgid.text(msg);
                                        //clearInterval(s1_Country_items)
                                        $currGetCountIndex += 1

                                        getCountires()

                                        return
                                    } else {

                                        getPageData()
                                    }
                                }).fail(function(err) {
                                    // showError
                                    currRunPage -= 1
                                    console.log(this.tmpdata)
                                    console.log(err)

                                    getPageData()
                                })
                                // //////////////////////////////////
                        }).fail(function(err) {
                            if (err.status === 404) {
                                console.log('404: no data:' + this.url)
                            } else {
                                console.log('Error:' + this.url)
                                console.log(err)
                                currRunPage -= 1
                                getPageData()
                            }
                        })
                    }
                    // ///////////////////////
                }
                // /////////////////////////////////////////////////
            }).fail(function(err) {
                console.log('Error:' + this.url)
                console.log(err)
            })

            // $.ajax({
            //    type: 'GET',
            //    timeout: 80000,
            //    url: config.getApi_getLast
            // }).done(function (data) {
            //     console.log(this.url)
            // }).fail(function (err) {
            //   console.log("Error:" + this.url)
            //    console.log(err); 
            // })

        }).fail(function(err) {
            console.log('Error:' + this.url)
            console.log(err)
        })
    }

    function getItems(geturl, title) {
        ///获取未更新前N条记录。
        console.log(title + "," + geturl);

        var $currRunPage = 1;
        var $currItemIndex = 0;
        var $dataAllItems = [];

        $.ajax({
            type: 'GET',
            timeout: 80000,
            contentType: 'application/json; charset=utf-8',
            url: geturl
        }).done(function(data) {
            console.log(this.url)
            console.log(data);
            $dataAllItems = data;
            //start get data
            getItemsNext()

        }).fail(function(err) {
            console.log(title + ",获取明细Error:" + this.url)
            console.log(err);
        })

        function getItemsNext() {


            if ($currItemIndex < 0) {
                $currItemIndex = 0;
            }
            if ($currRunPage < 1) {
                $currRunPage = 1;
            }

            if ($currItemIndex >= $dataAllItems.length) {
                console.log(title + ",处理完成：" + $dataAllItems.length + ",开始处理下：" + $dataAllItems.length + " 条。");
                //start main
                getItems(geturl, title);
                return;
            }
            console.log(title + ",开始处理：" + $currItemIndex + ",Page:" + $currRunPage);
            var tmpItem = $dataAllItems[$currItemIndex];
            //console.log(tmpItem);
            if (tmpItem) {
                if (!tmpItem.ttype) {
                    tmpItem.ttype = tmpItem.nameURL.split('/')[3]
                }
            }

            //获取记录明细
            $.ajax({
                type: 'GET',
                data: {
                    page: $currRunPage
                },
                tmppage: $currRunPage,
                tmpdata: tmpItem,
                timeout: 80000,
                url: tmpItem.nameURL
            }).done(function(data) {
                console.log(title + "," + this.tmpdata.nameDesc + "," + this.url)
                var $body = $('<div></div>').html(data);
                console.log(title + ",html lenght:" + data.length);

                var $getDiv = $body.find('.container').eq(2).find('.col-sm-8').eq(0);
                var $allp = $getDiv.find('p');

                var $table = $getDiv.find('table').eq(0);
                var $tableTR = $table.find('tr');

                if ($tableTR.length <= 1 && this.tmppage > 1) {

                    console.clear();

                    console.log("CountPage:" + this.tmppage)
                    $currRunPage = 1;
                    $currItemIndex = $currItemIndex + 1;
                    getItemsNext()
                } else {
                    var _remark = $getDiv.find('p[class="text-muted"]').eq(0).text().trim()

                    //console.log('Remark:' + _remark);

                    var postMain = {
                        Tid: this.tmpdata.Tid,
                        entitysAll: {
                            $id: "2",
                            name: this.tmpdata.name,
                            nameDesc: this.tmpdata.nameDesc,
                            nameURL: this.tmpdata.nameURL,
                            ttype: this.tmpdata.ttype,
                            Countries: this.tmpdata.CountriesDesc,

                            Source: undefined,
                            Status: undefined,
                            Address: undefined,
                            CompanyType: undefined,
                            DormDate: undefined,
                            Jurisdiction: undefined,

                            Tid: 0,
                            Remark: _remark,
                            getPage: this.tmppage,
                            tStatus: 0,
                            ClientIP: undefined,
                            addDate: undefined,
                            UpdateDate: undefined
                        },
                        connections: []
                    }

                    //get for main
                    for (var index = 0; index < $allp.length; index++) {
                        //$allp.eq(index).find('br').replaceWith("@");
                        var ptext = $allp.eq(index).text().replace(/[\↵\t\n\v\r]/g, '').trim();

                        if (ptext.indexOf("Countries:") > -1) {
                            postMain.entitysAll.Countries = ptext.replace('Countries:', '').replace(/\ {2,}/g, '@').replace('@', '').trim();
                            continue;
                        }
                        //console.log(index + ":" + ptext);
                        if (ptext.indexOf("Source:") > -1) {
                            postMain.entitysAll.Source = ptext.replace('Source:', '').trim();
                            continue;
                        }
                        if (ptext.indexOf("Status:") > -1) {
                            postMain.entitysAll.Status = ptext.replace('Status:', '').trim();
                            continue;
                        }
                        if (ptext.indexOf("Address:") > -1) {
                            postMain.entitysAll.Address = ptext.replace('Address:', '').trim();
                            continue;
                        }

                        if (ptext.indexOf("Company Type:") > -1) {
                            postMain.entitysAll.CompanyType = ptext.replace('Company Type:', '').trim();
                            continue;
                        }
                        if (ptext.indexOf("Dorm Date:") > -1) {
                            postMain.entitysAll.DormDate = ptext.replace('Dorm Date:', '').trim();
                            continue;
                        }
                        if (ptext.indexOf("Jurisdiction:") > -1) {
                            postMain.entitysAll.Jurisdiction = ptext.replace('Jurisdiction:', '').replace(/\ {2,}/g, '');
                            continue;
                        }
                    }
                    switch (this.tmpdata.ttype) {
                        case 'officer':
                        case 'intermediary':
                            for (var index = 1; index < $tableTR.length; index++) {
                                var td = $tableTR.eq(index).find('td');
                                var td2aAll = td.eq(2).find('a');
                                if (td2aAll.length <= 0) {
                                    continue;
                                }
                                var td2a = td2aAll.eq(0);

                                var ahref = unescape(decodeURI(td2a.attr('href')));


                                // console.log(td.text());
                                var postItemsconnections = {
                                    $id: (index + 2),
                                    nameFrom: this.tmpdata.name,
                                    nameFromURL: this.tmpdata.nameURL,
                                    nameFromDesc: this.tmpdata.nameDesc,
                                    nameType: td.eq(1).text().trim(),
                                    nameTo: ahref.split('_')[1],
                                    nameToURL: ahref,
                                    nameToDesc: td2a.text(),
                                    Tid: 0,
                                    Remark: undefined,
                                    getPage: this.tmppage,
                                    tStatus: 0,
                                    ClientIP: undefined,
                                    addDate: undefined,
                                    UpdateDate: undefined
                                }
                                postMain.connections.push(postItemsconnections);

                                //end for  
                            }
                            break;
                        case "entity":
                        case "address":
                            for (var index = 1; index < $tableTR.length; index++) {
                                var td = $tableTR.eq(index).find('td');
                                var td2aAll = td.eq(0).find('a');
                                var tmptype = td.eq(1).text().trim();
                                // if (!tmptype == 'Intermediary of') {
                                //     continue;
                                // }
                                if (td2aAll.length <= 0) {
                                    continue;
                                }

                                var td2a = td2aAll.eq(0);

                                var ahref = unescape(decodeURI(td2a.attr('href')));


                                // console.log(td.text());
                                var postItemsconnections = {
                                    $id: (index + 2),
                                    nameFrom: ahref.split('_')[1],
                                    nameFromURL: ahref,
                                    nameFromDesc: td2a.text(),
                                    nameType: tmptype,
                                    nameTo: this.tmpdata.name,
                                    nameToURL: this.tmpdata.nameURL,
                                    nameToDesc: this.tmpdata.nameDesc,
                                    Tid: 0,
                                    Remark: undefined,
                                    getPage: this.tmppage,
                                    tStatus: 0,
                                    ClientIP: undefined,
                                    addDate: undefined,
                                    UpdateDate: undefined
                                }

                                postMain.connections.push(postItemsconnections);

                                //end for  
                            }
                            break;
                        default:
                            break;
                    }

                    //console.log(postMain);

                    // $currItemIndex = $currItemIndex + 1;               
                    // getItemsNext()

                    // 提交数据库
                    $.ajax({
                        type: 'POST',
                        url: config.postApi_Panamadb,
                        tmppage: this.tmppage,
                        tmpdata: postMain,
                        timeout: 80000,
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify(postMain)
                    }).done(function(data) {
                        console.log(title + "," + this.tmpdata.entitysAll.Countries + ',Type:' + this.tmpdata.entitysAll.ttype + ',nameDesc:' + this.tmpdata.entitysAll.nameDesc + ",CurrPage:" + this.tmppage + '--> Post Done!')
                            //
                        $currRunPage = $currRunPage + 1;
                        //$currItemIndex = $currItemIndex + 1;
                        //start get data
                        getItemsNext()

                    }).fail(function(err) {
                        $currRunPage = $currRunPage - 1;
                        console.log(title + ",Error:" + this.url)
                        console.log(err);
                        //$currItemIndex = $currItemIndex - 1;
                        //start get data
                        getItemsNext()

                    })
                }


            }).fail(function(err) {
                $currRunPage = 1;
                if (err.status === 404) {
                    console.log('404: no data:' + this.url)
                    $currItemIndex = $currItemIndex + 1;
                    getItemsNext()
                } else {
                    console.log(title + ",Error:" + this.url)
                    console.log(err);
                    $currItemIndex = $currItemIndex - 1;
                    //start get data
                    getItemsNext()
                }

            })
        }
        // $.ajax({
        //    type: 'GET',
        //    timeout: 80000,
        //    url: config.getApi_getLast
        // }).done(function (data) {
        //     console.log(this.url)
        // }).fail(function (err) {
        //   console.log("Error:" + this.url)
        //    console.log(err); 
        // })
    }
})