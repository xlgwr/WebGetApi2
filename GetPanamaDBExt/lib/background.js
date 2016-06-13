// chrome.runtime.sendMessage({greeting: "hello"}, function(response) {
//     console.log(response.farewell)
// })
// User-Agent: Fiddler
// Content-Type: application/json; charset=utf-8  
// Content-Length: 1696
// Host: 192.168.1.136:8081

///示例看 ./model/backMsg.js 内方法
chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
        var tmpid = sender.tab.id
            // console.log(request)
            // console.log(tmpid)
            // console.log(sender.tab ?
            // "from a content script:" + sender.tab.url :
            // "from the extension")

        if (request.greeting == 'hello') {
            bootStrap(tmpid, request.msg)
            sendResponse({
                farewell: 'goodbye'
            })
        }
        if (request.greeting == 'removeUrl') {
            removeTabUrl(tmpid, request.msg)
            sendResponse({
                farewell: 'goodbye Url'
            })
        }
    })

chrome.browserAction.onClicked.addListener(function(tab) {
        chrome.tabs.create({
            url: chrome.extension.getURL('popup.html')
        })
    })
    // //////////////////////////////////////////////////////////////////////////

// must be jquery
$(function() {
    console.log('backGround run.')
})