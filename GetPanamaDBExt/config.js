var $gbHttpIp = "http://192.168.1.136:9000";
var $gbHttp_S_Ip = "https://192.168.1.136:9443";

// var $gbHttpIp = "http://192.168.1.6:9000";
// var $gbHttp_S_Ip = "https://192.168.1.6:9443";

var config = {
    //API 接口
    postApi_Panamadb: $gbHttpIp + "/api/Panamadb",
    postApi_CountryItems: $gbHttpIp + "/api/CountryItems",

    getApi_getLast: $gbHttpIp + "/api/getLast",
    getApi_topCountryItems: $gbHttpIp + "/api/topCountryItems/5"
}

var configGetUrl = {
    panamadbHome: 'https://panamadb.org'

}