function isnumerickey(e) {
    if (window.event)
        var txt = window.event.keyCode; // IE
    else
        var txt = e.which;

    if (txt > 31 && (txt < 48 || txt > 57)) {

        if (txt == 46) {
            return true;
        }
        else {
            //alert("Input is Number only")
            return false;
        }

    }
    return true;
}

function checkTextField(field) {
    if (field.value == '' || field.value == 0) {
        field.value = 1;
    }
}
function checkTextField1(field) {
    if (field.value == '' || field.value == 0 || field.value == '.') {
        field.value = 0;
    }


    var nilai = field.value.split(',').join('');
    var output = nilai;
    if (parseFloat(nilai)) {
        input = new String(nilai); // so you can perform string operations
        var parts = input.split("."); // remove the decimal part
        parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
        output = parts.join(".");
    }
    field.value = output;
}


function Comma(Num) { //function to add commas to textboxes

    Num += '';
    Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
    Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
    x = Num.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1))
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    return x1 + x2;
}



function ConfirmDeleteMessage(strMessage) {

    if (window.confirm(strMessage)) {

        return true;
    }
    else {

        return false;
    }

}

function date_time(id) {
    date = new Date;
    year = date.getFullYear();
    month = date.getMonth();
    months = new Array('January', 'February', 'March', 'April', 'May', 'June', 'Jully', 'August', 'September', 'October', 'November', 'December');
    d = date.getDate();
    day = date.getDay();
    days = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
    h = date.getHours();
    if (h < 10) {
        h = "0" + h;
    }
    m = date.getMinutes();
    if (m < 10) {
        m = "0" + m;
    }
    s = date.getSeconds();
    if (s < 10) {
        s = "0" + s;
    }
    result = '' + days[day] + ' ' + months[month] + ' ' + d + ' ' + year + ' ' + h + ':' + m + ':' + s;
    document.getElementById(id).innerHTML = result;
    setTimeout('date_time("' + id + '");', '1000');
    return true;
}
