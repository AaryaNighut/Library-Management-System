$(document).ready(() => {

    $('.mobile').bind("cut copy paste", function (e) {
        e.preventDefault();
    });



});


function onlyAlphabets(e, t) {
    try {
        if (window.event) {
            var charCode = window.event.keyCode;
        }
        else if (e) {
            var charCode = e.which;
        }
        else { return true; }
        if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
            return true;
        else
            return false;
    }
    catch (err) {
        // alert(err.Description);
    }
}

function showNotification(icon, title, msg, isButtonVisible, isRedirect, redirectUrl) {
    //debugger;

    var btnColor = '';
    if (icon == 'success') {
        btnColor = '#6c0003';
    } else if (icon == 'error') {
        btnColor = 'red';
    } else if (icon == 'info') {
        btnColor = 'blue';
    }
    if (isRedirect) {
        Swal.fire({
            icon: icon,
            title: title,
            html: "<span class='sw-msg'>" + msg + "<span>",
            showCancelButton: false,
            showCloseButton: true,
            confirmButtonColor: btnColor,
            allowOutsideClick: false
        }).then(function () {
            window.location = redirectUrl;
        });
    } else {
        Swal.fire({
            icon: icon,
            title: title,
            html: msg,
            showCancelButton: false,
            showCloseButton: true,
            confirmButtonColor: btnColor,
            allowOutsideClick: false

        }).then(() => {

        })
    }
}
function showLoader() {

    $('.preloader').removeAttr('style');
    $('.wrapper').addClass('d-none');
}

//For Hide Loader
function hideLoader() {
    setTimeout(() => {
        $('.preloader').attr('style', 'display: none;');
        $('.wrapper').removeClass('d-none');
    }, 600);
}
function emptyTable(tableId) {
    $('#' + tableId + ' tr ').remove();

}
function validateNumber(e) {
    const pattern = /^[0-9]$/;

    return pattern.test(e.key)
}

function spaceremove(e) {
    $('#UserName').val($('#UserName').val().replace(/\s/g, ''));
}

function checkSpace(e) {
    if (e.key === " ") {
        return false;
    }
}

function validateCharecter(e) {

    const pattern = /^[A-Za-z ]$/;
    return pattern.test(e.key)
}


function validateEmail(e) {

    const pattern = /^[A-Za-z_.0-9@]$/;
    return pattern.test(e.key)
}
function isJsonString(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function constructTable(selector, list) {

    // Getting the all column names
    var cols = Headers(list, selector);

    // Traversing the JSON data
    for (var i = 0; i < list.length; i++) {
        var row = $('<tr/>');
        for (var colIndex = 0; colIndex < cols.length; colIndex++) {

            var val = list[i][cols[colIndex]];
            var temp = "";
            // If there is any key, which is matching
            // with the column name
            if (val == null) {
                temp = "";
            } else {
                temp = val;
            }
            var aaa = encodeWhiteSpaces(temp);
            row.append($('<td/>').html(aaa));
        }

        // Adding each row to the table
        $(selector).append(row);
    }


    $(selector + ' tr:has(th)').wrapAll('<thead></thead>');
    $(selector + ' tr:has(td)').wrapAll('<tbody></tbody>');
    $(selector + ' thead').prependTo(selector);
    $(selector + ' tbody').prependTo(selector);
    //replaceSrc();
}



function replaceSrc() {

    var images = document.getElementsByTagName('img');

    for (var i = 0; i < images.length; i++) {
        var dt = new Date();
        var img = images[i];

        if (img.src.length >= 0 & img.id != 'idImageNoTimestamp') {
            img.src = img.src + "?" + dt.getTime();
            //javascript:alert(document.lastModified);
        }
    }
}
function encodeWhiteSpaces(str) {

    return str;
    //return str.split('').map(function (c) { return c === ' ' ? '&nbsp;' : c }).join('');
}
function Headers(list, selector) {
    var columns = [];
    var header = $('<tr/>');

    for (var i = 0; i < list.length; i++) {
        var row = list[i];

        for (var k in row) {
            if ($.inArray(k, columns) == -1) {
                columns.push(k);

                // Creating the header
                header.append($('<th/>').html(k));
            }
        }
    }

    // Appending the header to the table
    $(selector).append(header);
    return columns;
}

function emptyTable(tableId) {
    $('#' + tableId).empty();

}


/////////////////////////--------LetzPLAY - CUSTOM PAGINATION PLUGIN START (28DEC2022-Anil Prajapati)--------\\\\\\\\\\\\\\\\\\\\\\\\\\
var pagifyFlg = true; var pageSize = 100; var pageNumber = 1; var totalPages = 0; var clearFlg = true; var exportTable = false;
var pagiFyObj = null; var pagifyAction = null; var pagiFyTable = null; pagiFyController = null; var _controller = null; var _action = null;
var nextBtn = '<span id="next" class="btn btn-primary next">Next</span>';
var backBtn = '<span id="back" class="btn btn-primary back">Previous</span>&nbsp;';
function JSONToCSVConvertor(JSONData, ReportTitle, ShowLabel) {
    //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
    var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;

    var CSV = '';
    //Set Report title in first row or line

    CSV += ReportTitle;

    //This condition will generate the Label/Header
    if (ShowLabel) {
        var row = "";

        //This loop will extract the label from 1st index of on array
        for (var index in arrData[0]) {

            //Now convert each value to string and comma-seprated
            row += index + ',';
        }

        row = row.slice(0, -1);

        //append Label row with line break
        CSV += row + '\r\n';
    }

    //1st loop is to extract each row
    for (var i = 0; i < arrData.length; i++) {
        var row = "";

        //2nd loop will extract each column and convert it in string comma-seprated
        for (var index in arrData[i]) {
            var html = $.parseHTML('<p>' + arrData[i][index] + '</p>');
            // console.log($(html).text().trim());
            if ($(html).text() != 'null') {

                row += '"' + $(html).text() + '",';
            } else {
                row += '-,';
            }

        }

        row.slice(0, row.length - 1);

        //add a line break after each row
        CSV += row + '\r\n';
    }

    if (CSV == '') {
        //alert("Invalid data");
        return;
    }

    //Generate a file name
    var fileName = "MyReport_";
    //this will remove the blank-spaces from the title and replace it with an underscore
    fileName += ReportTitle.replace(/ /g, "_");

    //Initialize file format you want csv or xls
    var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

    // Now the little tricky part.
    // you can use either>> window.open(uri);
    // but this will not work in some browsers
    // or you will not get the correct file extension    

    //this trick will generate a temp <a /> tag
    var link = document.createElement("a");
    link.href = uri;

    //set the visibility hidden so it will not effect on your web-layout
    link.style = "visibility:hidden";
    link.download = "Report.csv";

    //this part will append the anchor tag and remove it after automatic click
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
function getTotalPageCount(rowCount) {
    // debugger
    if ((rowCount / pageSize).toString().indexOf('.') > 0) {
        return parseInt((rowCount / pageSize)) + 1;
    } else if ((rowCount / pageSize).toString().indexOf('.') < 0) {
        return rowCount / pageSize;
    }
    return 1;
}
function generateExportButton(tableId) {
    $('.export-container').remove();
    $(tableId).parent('.table-container').before('<div class="export-container"><button class="btn btn-success right-0 export-table">Export in CSV</button></div>');
}
function pagination(objs, ControllerName, actionName, mainTableId, pageNumber, pageSize, exportFlg) {
   
    showLoader();
    var aa;

    if (objs != null) {
        aa = JSON.stringify(objs).slice(0, -1);
        aa = '{"search":"' + objs + '","pageNumber":"' + pageNumber + '","pageSize":"' + pageSize + '"}';
        console.log(aa);
    } else {
        aa = '{"pageNumber":"' + pageNumber + '","pageSize":"' + pageSize + '"}';
    }

    setTimeout(() => {

        $.ajax({
            type: "POST",
            url: "/" + ControllerName + '/' + actionName + '/',
            data: JSON.parse(aa),
            dataType: 'JSON',
            async: false,
            beforeSend: function (msg) {
                showLoader();
            },
            success: function (response) {
                _controller = ControllerName; _action = 'export_' + actionName;
                pagiFyObj = objs; pagiFyController = ControllerName; pagifyAction = actionName; pagiFyTable = mainTableId;
                if (response != "") {

                    if ((response)['Table1'].length > 0) {

                        emptyTable(mainTableId);

                        constructTable('#' + mainTableId, (response)['Table1']);
                        if (exportTable == true) {
                            generateExportButton('#' + mainTableId);

                        }
                        if (pagifyFlg && ((response)['Table2']) !== undefined) {

                            if ((response)['Table2'].length == 0) {

                                totalPages = 1;
                            }
                            else {
                                totalPages = getTotalPageCount((response)['Table2'][0].RowCount);
                            }
                            generatePaginationButtons(totalPages, mainTableId);
                            pagifyFlg = false;

                            console.log(totalPages);

                        }
                    } else {
                        removePaginationButtons(mainTableId);
                        $('#' + mainTableId).html('<tr><td>No Records Found!</td></tr>');
                    }
                }
                else {
                    removePaginationButtons(mainTableId);
                    $('#' + mainTableId).html('<tr><td>No Records Found!</td></tr>');

                    //OpenpageNumber = OpenpageNumber - 1;
                    //showNotification('error', 'Oops', "No Records Founds", true, 'red', false, null);
                }

            },
            error: (e) => {
                console.log();
                hideLoader();
                //setTimeout(function () { hideLoader() }, 1000);
            },

        });
        hideLoader();
    }, 500);


}
function letzPlayPagination(tableId) {
    if ($(document).find('#' + tableId + '_letzPlayPagination').length == 0) {
        $(document).find('#' + tableId).parent('div').parent('div').append('<div id=' + tableId + '_letzPlayPagination class= "text-center mt-2 letzPlayPagination" ></div > ');
    }
}
function generatePaginationButtons(totalPages, tableId) {
    letzPlayPagination(tableId);
    var buttons = '';
    for (var i = 1; i <= (totalPages < 5 ? totalPages : 5); i++) {
        if (i == 1) {
            buttons = buttons + '<a id="' + tableId + '_letzPlayPagination_' + i + '" class="a-pagination btn btn-primary btn-active">' + i + '</a>&nbsp;';
        } else {
            buttons = buttons + '<a id="' + tableId + '_letzPlayPagination_' + i + '" class="a-pagination btn btn-primary">' + i + '</a>&nbsp;';
        }
    }
    $(document).find('#' + tableId + '_letzPlayPagination').html(backBtn + buttons + nextBtn);
    //debugger
    if ($('.btn-active').text().trim() == '1') {
        $('#back').addClass('disabled');
    } else {
        $('#back').removeClass('disabled');
    }
    if ($('.btn-active').text().trim() == totalPages) {
        $('#next').addClass('disabled');
    } else {
        $('#next').removeClass('disabled');
    }


}
function pagify(PagifypageNumber, btnId) {

    showLoader();
    $('.btn-active').removeClass('btn-active');
    $('#' + btnId).addClass('btn-active');




    pagination(pagiFyObj, pagiFyController, pagifyAction, pagiFyTable, PagifypageNumber, pageSize);

}
function removePaginationButtons(tableId) {
    $('#' + tableId + '_letzPlayPagination').closest('div').empty();
}

$(document).ready(() => {

    $(document).on('click', '.a-pagination', (e) => {
        debugger
        pagify(e.currentTarget.text, e.currentTarget.attributes['id'].value);



        if ($('.btn-active').text().trim() == '1') {
            $('#back').addClass('disabled');
        }
        else {
            $('#back').removeClass('disabled');
        }


        if ($('.btn-active').text().trim() == totalPages) {
            $('#next').addClass('disabled');
        } else {
            $('#next').removeClass('disabled');
        }


    })
    //NAVIGATION BUTTONS
    $(document).on('click', '.back', (e) => {
        debugger
        var mainDiv = $(e.currentTarget.closest('.letzPlayPagination').closest('.letzPlayPagination'));
        var firstPageNumber = mainDiv.find('a:first').text();
        var activePageNumber = mainDiv.find('.btn-active').text();
        if (firstPageNumber == activePageNumber) {
            var pgCnt = parseInt(firstPageNumber);
            var buttons = '';
            if (pgCnt == 1) {
                showNotification('error', 'Oops', "No Records Founds", true, 'red', false, null);
            }
            else if (pgCnt != 1 && pgCnt > 0) {
                var arr = [];

                for (var i = pgCnt - 1; i >= ((pgCnt) - 5); i--) {

                    if (i === 0) {
                        break;
                    }
                    buttons = '<a id="' + mainDiv.attr('id') + '_' + i + '" class="a-pagination btn btn-primary" > ' + i + '</a>&nbsp;';
                    arr.push(buttons);
                }

                mainDiv.html(backBtn + arr.reverse().toString().replaceAll(",", "") + nextBtn);
                $(mainDiv).find('a:last').addClass('btn-active');
                pagify($(mainDiv).find('a:last').text(), $(mainDiv).find('a:last').attr('id'));
            }
        }
        else {
            var btnId = $(mainDiv).find('.btn-active').attr('id');
            pagify((parseInt(activePageNumber) - 1), btnId.split('_')[0] + '_' + btnId.split('_')[1] + '_' + (parseInt(activePageNumber) - 1));
        }


        if ($('.btn-active').text().trim() == '1') {
            $('#back').addClass('disabled');
        } else {
            $('#back').removeClass('disabled');
        }



        if ($('.btn-active').text().trim() == totalPages) {
            $('#next').addClass('disabled');
        } else {
            $('#next').removeClass('disabled');
        }
    });
    $(document).on('click', '.next', (e) => {
        // debugger
        var mainDiv = $(e.currentTarget.closest('.letzPlayPagination').closest('.letzPlayPagination'));
        var activePageNumber = mainDiv.find('.btn-active').text();
        var lastPageNumber = mainDiv.find('a:last').text();
        var pgCnt = parseInt(lastPageNumber);

        if (parseInt(activePageNumber) + 1 > pgCnt) {



            var buttons = '';
            if (totalPages > pgCnt) {
                //debugger
                var inPgnCnt = 0;
                if (totalPages >= (pgCnt + 5)) {
                    inPgnCnt = 5;
                }
                else if (totalPages < (pgCnt + 5)) {
                    inPgnCnt = totalPages - pgCnt;

                }
                else {

                    inPgnCnt = totalPages - pgCnt;
                }
                for (var i = pgCnt + 1; i < pgCnt + 1 + inPgnCnt; i++) {

                    buttons = buttons + '<a id="' + mainDiv.attr('id') + '_' + i + '"  class="a-pagination btn btn-primary">' + i + '</a>&nbsp;';
                }
                $(mainDiv).html(backBtn + buttons + nextBtn);
                $(mainDiv).find('a:first').addClass('btn-active');
                pagify($(mainDiv).find('a:first').text(), $(mainDiv).find('a:first').attr('id'));
            } else {
                showNotification('error', 'Oops', 'No Records Found !', true, 'red', null, null);

            }

        } else {
            var btnId = $(mainDiv).find('.btn-active').attr('id');
            pagify((parseInt(activePageNumber) + 1), btnId.split('_')[0] + '_' + btnId.split('_')[1] + '_' + (parseInt(activePageNumber) + 1));
        }




        if ($('.btn-active').text().trim() == '1') {
            $('#back').addClass('disabled');
        } else {
            $('#back').removeClass('disabled');
        }



        if ($('.btn-active').text().trim() == totalPages) {
            $('#next').addClass('disabled');
        } else {
            $('#next').removeClass('disabled');
        }
    });


    $(document).on('click', '#Search', (e) => {
        debugger
        pagifyFlg = true;

    });


    $(document).on('click', '#Clear', (e) => {
        debugger

        generatePaginationButtons(totalPages, pagiFyTable);

    });

    $(document).on('click', '.export-table', (e) => {
        showLoader();
        var aa = '{"pageNumber":"1","pageSize":"2147483647"}';

        $.ajax({
            type: "POST",
            url: _controller + '/' + _action + '/',
            data: JSON.parse(aa),
            dataType: 'JSON',
            async: true,
            beforeSend: function (msg) {
                showLoader();
            },
            success: function (response) {
                hideLoader();
                JSONToCSVConvertor((response)['Table'], "", true);
                console.log(response);
            }
        });

        //JSONToCSVConvertor();

    });

});



/////////////////////////--------LetzPLAY - CUSTOM PAGINATION PLUGIN END (28DEC2022-Anil Prajapati)--------\\\\\\\\\\\\\\\\\\\\\\\\\\




function getActionNameFromURL() {
    var currentUrl = window.location.pathname.split('/');
    return currentUrl[2];
}

function getControllerNameFromURL() {
    var currentUrl = window.location.pathname.split('/');
    return currentUrl[1];
}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function startWebcam() {
    const video = document.getElementById('webcam');
    const canvas = document.getElementById('canvas');

    $('#show_img').removeClass('d-none');
    $('#webcam').addClass('d-none');
    $('#overlay').addClass('d-none');
    let constraints;
    // Check if the device is a mobile device
    const isMobileDevice = /Mobi|Android/i.test(navigator.userAgent);

    if (isMobileDevice) {
        // For mobile devices, use default constraints (best resolution)
        constraints = {
            audio: false,
            video: {
                width: { ideal: 720 },
                height: { ideal: 1080 }
            }
        };
    } else {
        constraints = {
            audio: false,
            video: {
                width: { ideal: 1080 },
                height: { ideal: 720 }
            }
        };
    }

    //const ctx = canvas.getContext('2d');
    navigator.mediaDevices.getUserMedia(constraints)
        .then((stream) => {
            setTimeout(() => {
                $('#webcam').removeClass('d-none');
                $('#faceCapture').html('');
            }, 500);
            video.srcObject = stream;
            video.addEventListener('play', function () {

            });
        })
        .catch((error) => {
            hideLoader();
            if (error.name === 'NotAllowedError' || error.name === 'PermissionDeniedError') {
                $('#faceCapture').html('Permission to access the webcam was denied. Please grant permission to use the webcam.');
            } else if (error.name === 'NotFoundError' || error.name === 'DevicesNotFoundError') {
                $('#faceCapture').html('No webcam found. Please make sure a webcam is connected and try again.');
            } else if (error.name === 'NotReadableError' || error.name === 'TrackStartError') {
                $('#faceCapture').html('The webcam is already in use or could not be started. Please close any other applications using the webcam.');
            } else {
                $('#faceCapture').html('An error occurred while trying to access the webcam: ' + error.message);
            }

        });

}
async function isCameraOn() {
    try {
        let constraints;
        const isMobileDevice = /Mobi|Android/i.test(navigator.userAgent);

        if (isMobileDevice) {
            // For mobile devices, use default constraints (best resolution)
            constraints = {
                audio: false,
                video: {
                    width: { ideal: 720 },
                    height: { ideal: 1080 }
                }
            };
        } else {
            constraints = {
                audio: false,
                video: {
                    width: { ideal: 1080 },
                    height: { ideal: 720 }
                }
            };
        }
        const stream = await navigator.mediaDevices.getUserMedia(constraints);
        const video = document.getElementById('webcam');
        video.srcObject = stream;

        var isVideoPlaying = !video.paused && !video.ended && video.readyState > 2;

        if (!isVideoPlaying) {
            video.srcObject = stream;
            await video.play();
            isVideoPlaying = !video.paused && !video.ended && video.readyState > 2;
        }
        hideLoader();
        return isVideoPlaying;
    }
    catch (error) {
        hideLoader();
        return false;
    }
}