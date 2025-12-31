$(document).ready(function () {

    var SessBranchID
    {
        
        //set { ViewState["SessBranchID"] = value; }
    }
  
    var doughnut = function doughnut() {
        $.ajax({
            type: "POST",
            url: "WebService.asmx/ChartBranchVsSex",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                var myData = JSON.parse(data.d);
                new OnSuccess(myData);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Error:" + jqXHR + textStatus + errorThrown);
            }
        });



        function OnSuccess(data) {


            var ReligonName = data.map(function (val) {
                return "\"" + val.rel_name + "\"";
            }).join(',');

            var ReligonName1 = JSON.parse("[" + ReligonName + "]");

            console.log(ReligonName1);

            var jsonsReligonName = ReligonName1.map(JSON.stringify);

            // ...or for older browsers
            var jsonsReligonName = [];
            for (var i = ReligonName1.length; i--;) {
                jsonsReligonName[i] = ReligonName1[i]
            };


            var Male = data.map(function (val) {
                return val.Male;
            }).join(',');

            var Male1 = JSON.parse("[" + Male + "]");

            console.log(Male1);

            var jsonsMale = Male1.map(JSON.stringify);

            // ...or for older browsers
            var jsonsMale = [];
            for (var i = Male1.length; i--;) {
                jsonsMale[i] = Male1[i]
            };


            var Female = data.map(function (val) {
                return val.Female;
            }).join(',');

            var Female1 = JSON.parse("[" + Female + "]");

            console.log(Female1);

            var jsonsFemale = Female1.map(JSON.stringify);

            // ...or for older browsers
            var jsonsFemale = [];
            for (var i = Female1.length; i--;) {
                jsonsFemale[i] = Female1[i]
            };


            var color = Chart.helpers.color;

          

            var barChartData = {
                labels: jsonsReligonName,
                //datasets: [
                //    {
                //        label: 'Male',                       
                //        data: 10,
                //        backgroundColor: cornflowerblue,
                //        hoverBackgroundColor: lightskyblue
                //    },
                //    {
                //        label: 'Female',                     
                //        data: 20,
                //        backgroundColor: orange,
                //        hoverBackgroundColor: darkorange
                //    }],

                datasets: [{
                    data: [jsonsMale, jsonsFemale],
                    backgroundColor: ["cornflowerblue", "lightgreen"],
                    hoverBackgroundColor: ["lightskyblue", "yellow"]
                }],
                labels: ["Male", "Female"]

            };
            //var data = {
            //    datasets: [{
            //        data: [270, 50, 40],
            //        backgroundColor: ["cornflowerblue", "lightgreen", "orange"],
            //        hoverBackgroundColor: ["lightskyblue", "yellow", "darkorange"]
            //    }],
            //    labels: ["corn flower blue", "lightgreen", "Orange"]
            //};
            console.log('doughnut');
            var ctx = document.getElementById("doughnut").getContext("2d");
            var myBar = new Chart(ctx, {
                type: 'doughnut',
                data: barChartData
            });
        }

    }

    var bar = function bar() {
        $.ajax({
            type: "POST",
            url: "WebService.asmx/ChartAnggotaBaru",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var myData = JSON.parse(data.d);
                //console.log(myData);
                new OnSuccess(myData);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Error:" + jqXHR + textStatus + errorThrown);
            }
        });



        function OnSuccess(data) {


            var ReligonName = data.map(function (val) {
                return "\"" + val.rel_name + "\"";
            }).join(',');

            var ReligonName1 = JSON.parse("[" + ReligonName + "]");

            console.log(ReligonName1);

            var jsonsReligonName = ReligonName1.map(JSON.stringify);

            // ...or for older browsers
            var jsonsReligonName = [];
            for (var i = ReligonName1.length; i--;) {
                jsonsReligonName[i] = ReligonName1[i]
            };

             var bln1 = data.map(function (val) {
                            return val.bln1;
                        }).join(',');
                        var xbln1 = JSON.parse("[" + bln1 + "]");  
                        var jsonsbln1 = xbln1.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln1 = [];
                        for (var i = xbln1.length; i--;) {
                            jsonsbln1[i] = xbln1[i]
                        };


                        var bln2 = data.map(function (val) {
                            return val.bln2;
                        }).join(',');
                        var xbln2 = JSON.parse("[" + bln2 + "]");
                        var jsonsbln2 = xbln2.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln2 = [];
                        for (var i = xbln2.length; i--;) {
                            jsonsbln2[i] = xbln2[i]
                        };

                        var bln3 = data.map(function (val) {
                            return val.bln3;
                        }).join(',');
                        var xbln3 = JSON.parse("[" + bln3 + "]");
                        var jsonsbln3 = xbln3.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln3 = [];
                        for (var i = xbln3.length; i--;) {
                            jsonsbln3[i] = xbln3[i]
                        };

                        var bln4 = data.map(function (val) {
                            return val.bln4;
                        }).join(',');
                        var xbln4 = JSON.parse("[" + bln4 + "]");
                        var jsonsbln4 = xbln4.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln4 = [];
                        for (var i = xbln4.length; i--;) {
                            jsonsbln4[i] = xbln4[i]
                        };

                        var bln5 = data.map(function (val) {
                            return val.bln5;
                        }).join(',');
                        var xbln5 = JSON.parse("[" + bln5 + "]");
                        var jsonsbln5 = xbln5.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln5 = [];
                        for (var i = xbln5.length; i--;) {
                            jsonsbln5[i] = xbln5[i]
                        };


                        var bln6 = data.map(function (val) {
                            return val.bln6;
                        }).join(',');
                        var xbln6 = JSON.parse("[" + bln6 + "]");
                        var jsonsbln6 = xbln6.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln6 = [];
                        for (var i = xbln6.length; i--;) {
                            jsonsbln6[i] = xbln6[i]
                        };
                        var bln7 = data.map(function (val) {
                            return val.bln7;
                        }).join(',');
                        var xbln7 = JSON.parse("[" + bln7 + "]");
                        var jsonsbln7 = xbln7.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln7 = [];
                        for (var i = xbln7.length; i--;) {
                            jsonsbln7[i] = xbln7[i]
                        };
                        var bln8 = data.map(function (val) {
                            return val.bln8;
                        }).join(',');
                        var xbln8 = JSON.parse("[" + bln8 + "]");
                        var jsonsbln8 = xbln8.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln8 = [];
                        for (var i = xbln8.length; i--;) {
                            jsonsbln8[i] = xbln8[i]
                        };
                        var bln9 = data.map(function (val) {
                            return val.bln9;
                        }).join(',');
                        var xbln9 = JSON.parse("[" + bln9 + "]");
                        var jsonsbln9 = xbln9.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln9 = [];
                        for (var i = xbln9.length; i--;) {
                            jsonsbln9[i] = xbln9[i]
                        };
                        var bln10 = data.map(function (val) {
                            return val.bln10;
                        }).join(',');
                        var xbln10 = JSON.parse("[" + bln10 + "]");
                        var jsonsbln10 = xbln10.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln10 = [];
                        for (var i = xbln10.length; i--;) {
                            jsonsbln10[i] = xbln10[i]
                        };
                        var bln11 = data.map(function (val) {
                            return val.bln11;
                        }).join(',');
                        var xbln11 = JSON.parse("[" + bln11 + "]");
                        var jsonsbln11 = xbln11.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln11 = [];
                        for (var i = xbln11.length; i--;) {
                            jsonsbln11[i] = xbln11[i]
                        };
                        var bln12 = data.map(function (val) {
                            return val.bln12;
                        }).join(',');
                        var xbln12 = JSON.parse("[" + bln12 + "]");
                        var jsonsbln12 = xbln12.map(JSON.stringify);
                        // ...or for older browsers
                        var jsonsbln12 = [];
                        for (var i = xbln12.length; i--;) {
                            jsonsbln12[i] = xbln12[i]
                        };


            var color = Chart.helpers.color;
            console.log(jsonsbln10);
            console.log(jsonsbln11);

            var barChartData = {
                labels: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
                datasets: [{
                    label: 'Anggota',
                    //backgroundColor: '#6610f2',
                    backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
                    borderColor: window.chartColors.blue,
                    borderWidth: 1,                    
                    data: [jsonsbln1, jsonsbln2, jsonsbln3, jsonsbln4, jsonsbln5, jsonsbln6, jsonsbln7, jsonsbln8, jsonsbln9, jsonsbln10, jsonsbln11, jsonsbln12]
                   
                }],            
            };


            //var barChartData = {
            //    labels: jsonsReligonName,
            //    datasets: [
            //        {
            //            label: 'Male',
            //            backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
            //            borderColor: window.chartColors.blue,
            //            borderWidth: 1,
            //            data: jsonsMale
            //        },
            //        {
            //            label: 'Female',
            //            backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
            //            borderColor: window.chartColors.red,
            //            borderWidth: 1,
            //            data: jsonsFemale
            //        }],
            //};

            var ctx = document.getElementById("bar").getContext("2d");
            var myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData              
            });
        }

    }


    var line = function line() {
        $.ajax({
            type: "POST",
            url: "WebService.asmx/ChartOrder",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var myData = JSON.parse(data.d);
                //console.log(myData);
                new OnSuccess(myData);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Error:" + jqXHR + textStatus + errorThrown);
            }
        });



        function OnSuccess(data) {


            var ReligonName = data.map(function (val) {
                return "\"" + val.NamaCabang + "\"";
            }).join(',');

            var ReligonName1 = JSON.parse("[" + ReligonName + "]");

            console.log(ReligonName1);

            var jsonsReligonName = ReligonName1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsReligonName = [];
            for (var i = ReligonName1.length; i--;) {
                jsonsReligonName[i] = ReligonName1[i]
            };


            //var bln1 = data.map(function (val) {
            //    return val.bln1;
            //}).join(',');
            //var xbln1 = JSON.parse("[" + bln1 + "]");  
            //var jsonsbln1 = xbln1.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln1 = [];
            //for (var i = xbln1.length; i--;) {
            //    jsonsbln1[i] = xbln1[i]
            //};


            //var bln2 = data.map(function (val) {
            //    return val.bln2;
            //}).join(',');
            //var xbln2 = JSON.parse("[" + bln2 + "]");
            //var jsonsbln2 = xbln2.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln2 = [];
            //for (var i = xbln2.length; i--;) {
            //    jsonsbln2[i] = xbln2[i]
            //};

            //var bln3 = data.map(function (val) {
            //    return val.bln3;
            //}).join(',');
            //var xbln3 = JSON.parse("[" + bln3 + "]");
            //var jsonsbln3 = xbln3.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln3 = [];
            //for (var i = xbln3.length; i--;) {
            //    jsonsbln3[i] = xbln3[i]
            //};

            //var bln4 = data.map(function (val) {
            //    return val.bln4;
            //}).join(',');
            //var xbln4 = JSON.parse("[" + bln4 + "]");
            //var jsonsbln4 = xbln4.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln4 = [];
            //for (var i = xbln4.length; i--;) {
            //    jsonsbln4[i] = xbln4[i]
            //};

            //var bln5 = data.map(function (val) {
            //    return val.bln5;
            //}).join(',');
            //var xbln5 = JSON.parse("[" + bln5 + "]");
            //var jsonsbln5 = xbln5.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln5 = [];
            //for (var i = xbln5.length; i--;) {
            //    jsonsbln5[i] = xbln5[i]
            //};


            //var bln6 = data.map(function (val) {
            //    return val.bln6;
            //}).join(',');
            //var xbln6 = JSON.parse("[" + bln6 + "]");
            //var jsonsbln6 = xbln6.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln6 = [];
            //for (var i = xbln6.length; i--;) {
            //    jsonsbln6[i] = xbln6[i]
            //};
            //var bln7 = data.map(function (val) {
            //    return val.bln7;
            //}).join(',');
            //var xbln7 = JSON.parse("[" + bln7 + "]");
            //var jsonsbln7 = xbln7.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln7 = [];
            //for (var i = xbln7.length; i--;) {
            //    jsonsbln7[i] = xbln7[i]
            //};
            //var bln8 = data.map(function (val) {
            //    return val.bln8;
            //}).join(',');
            //var xbln8 = JSON.parse("[" + bln8 + "]");
            //var jsonsbln8 = xbln8.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln8 = [];
            //for (var i = xbln8.length; i--;) {
            //    jsonsbln8[i] = xbln8[i]
            //};
            //var bln9 = data.map(function (val) {
            //    return val.bln9;
            //}).join(',');
            //var xbln9 = JSON.parse("[" + bln9 + "]");
            //var jsonsbln9 = xbln9.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln9 = [];
            //for (var i = xbln9.length; i--;) {
            //    jsonsbln9[i] = xbln9[i]
            //};
            //var bln10 = data.map(function (val) {
            //    return val.bln10;
            //}).join(',');
            //var xbln10 = JSON.parse("[" + bln10 + "]");
            //var jsonsbln10 = xbln10.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln10 = [];
            //for (var i = xbln10.length; i--;) {
            //    jsonsbln10[i] = xbln10[i]
            //};
            //var bln11 = data.map(function (val) {
            //    return val.bln11;
            //}).join(',');
            //var xbln11 = JSON.parse("[" + bln11 + "]");
            //var jsonsbln11 = xbln11.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln11 = [];
            //for (var i = xbln11.length; i--;) {
            //    jsonsbln11[i] = xbln11[i]
            //};
            //var bln12 = data.map(function (val) {
            //    return val.bln12;
            //}).join(',');
            //var xbln12 = JSON.parse("[" + bln12 + "]");
            //var jsonsbln12 = xbln12.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln12 = [];
            //for (var i = xbln12.length; i--;) {
            //    jsonsbln12[i] = xbln12[i]
            //};



            var blnp1 = data.map(function (val) {
                return val.blnp1;
            }).join(',');
            var xblnp1 = JSON.parse("[" + blnp1 + "]");
            var jsonsblnp1 = xblnp1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp1 = [];
            for (var i = xblnp1.length; i--;) {
                jsonsblnp1[i] = xblnp1[i]
            };


            var blnp2 = data.map(function (val) {
                return val.blnp2;
            }).join(',');
            var xblnp2 = JSON.parse("[" + blnp2 + "]");
            var jsonsblnp2 = xblnp2.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp2 = [];
            for (var i = xblnp2.length; i--;) {
                jsonsblnp2[i] = xblnp2[i]
            };

            var blnp3 = data.map(function (val) {
                return val.blnp3;
            }).join(',');
            var xblnp3 = JSON.parse("[" + blnp3 + "]");
            var jsonsblnp3 = xblnp3.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp3 = [];
            for (var i = xblnp3.length; i--;) {
                jsonsblnp3[i] = xblnp3[i]
            };

            var blnp4 = data.map(function (val) {
                return val.blnp4;
            }).join(',');
            var xblnp4 = JSON.parse("[" + blnp4 + "]");
            var jsonsblnp4 = xblnp4.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp4 = [];
            for (var i = xblnp4.length; i--;) {
                jsonsblnp4[i] = xblnp4[i]
            };

            var blnp5 = data.map(function (val) {
                return val.blnp5;
            }).join(',');
            var xblnp5 = JSON.parse("[" + blnp5 + "]");
            var jsonsblnp5 = xblnp5.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp5 = [];
            for (var i = xblnp5.length; i--;) {
                jsonsblnp5[i] = xblnp5[i]
            };


            var blnp6 = data.map(function (val) {
                return val.blnp6;
            }).join(',');
            var xblnp6 = JSON.parse("[" + blnp6 + "]");
            var jsonsblnp6 = xblnp6.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp6 = [];
            for (var i = xblnp6.length; i--;) {
                jsonsblnp6[i] = xblnp6[i]
            };
            var blnp7 = data.map(function (val) {
                return val.blnp7;
            }).join(',');
            var xblnp7 = JSON.parse("[" + blnp7 + "]");
            var jsonsblnp7 = xblnp7.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp7 = [];
            for (var i = xblnp7.length; i--;) {
                jsonsblnp7[i] = xblnp7[i]
            };
            var blnp8 = data.map(function (val) {
                return val.blnp8;
            }).join(',');
            var xblnp8 = JSON.parse("[" + blnp8 + "]");
            var jsonsblnp8 = xblnp8.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp8 = [];
            for (var i = xblnp8.length; i--;) {
                jsonsblnp8[i] = xblnp8[i]
            };
            var blnp9 = data.map(function (val) {
                return val.blnp9;
            }).join(',');
            var xblnp9 = JSON.parse("[" + blnp9 + "]");
            var jsonsblnp9 = xblnp9.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp9 = [];
            for (var i = xblnp9.length; i--;) {
                jsonsblnp9[i] = xblnp9[i]
            };
            var blnp10 = data.map(function (val) {
                return val.blnp10;
            }).join(',');
            var xblnp10 = JSON.parse("[" + blnp10 + "]");
            var jsonsblnp10 = xblnp10.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp10 = [];
            for (var i = xblnp10.length; i--;) {
                jsonsblnp10[i] = xblnp10[i]
            };
            var blnp11 = data.map(function (val) {
                return val.blnp11;
            }).join(',');
            var xblnp11 = JSON.parse("[" + blnp11 + "]");
            var jsonsblnp11 = xblnp11.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp11 = [];
            for (var i = xblnp11.length; i--;) {
                jsonsblnp11[i] = xblnp11[i]
            };
            var blnp12 = data.map(function (val) {
                return val.blnp12;
            }).join(',');
            var xblnp12 = JSON.parse("[" + blnp12 + "]");
            var jsonsblnp12 = xblnp12.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp12 = [];
            for (var i = xblnp12.length; i--;) {
                jsonsblnp12[i] = xblnp12[i]
            };


            var blnb1 = data.map(function (val) {
                return val.blnb1;
            }).join(',');
            var xblnb1 = JSON.parse("[" + blnb1 + "]");
            var jsonsblnb1 = xblnb1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb1 = [];
            for (var i = xblnb1.length; i--;) {
                jsonsblnb1[i] = xblnb1[i]
            };


            var blnb2 = data.map(function (val) {
                return val.blnb2;
            }).join(',');
            var xblnb2 = JSON.parse("[" + blnb2 + "]");
            var jsonsblnb2 = xblnb2.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb2 = [];
            for (var i = xblnb2.length; i--;) {
                jsonsblnb2[i] = xblnb2[i]
            };

            var blnb3 = data.map(function (val) {
                return val.blnb3;
            }).join(',');
            var xblnb3 = JSON.parse("[" + blnb3 + "]");
            var jsonsblnb3 = xblnb3.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb3 = [];
            for (var i = xblnb3.length; i--;) {
                jsonsblnb3[i] = xblnb3[i]
            };

            var blnb4 = data.map(function (val) {
                return val.blnb4;
            }).join(',');
            var xblnb4 = JSON.parse("[" + blnb4 + "]");
            var jsonsblnb4 = xblnb4.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb4 = [];
            for (var i = xblnb4.length; i--;) {
                jsonsblnb4[i] = xblnb4[i]
            };

            var blnb5 = data.map(function (val) {
                return val.blnb5;
            }).join(',');
            var xblnb5 = JSON.parse("[" + blnb5 + "]");
            var jsonsblnb5 = xblnb5.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb5 = [];
            for (var i = xblnb5.length; i--;) {
                jsonsblnb5[i] = xblnb5[i]
            };


            var blnb6 = data.map(function (val) {
                return val.blnb6;
            }).join(',');
            var xblnb6 = JSON.parse("[" + blnb6 + "]");
            var jsonsblnb6 = xblnb6.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb6 = [];
            for (var i = xblnb6.length; i--;) {
                jsonsblnb6[i] = xblnb6[i]
            };
            var blnb7 = data.map(function (val) {
                return val.blnb7;
            }).join(',');
            var xblnb7 = JSON.parse("[" + blnb7 + "]");
            var jsonsblnb7 = xblnb7.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb7 = [];
            for (var i = xblnb7.length; i--;) {
                jsonsblnb7[i] = xblnb7[i]
            };
            var blnb8 = data.map(function (val) {
                return val.blnb8;
            }).join(',');
            var xblnb8 = JSON.parse("[" + blnb8 + "]");
            var jsonsblnb8 = xblnb8.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb8 = [];
            for (var i = xblnb8.length; i--;) {
                jsonsblnb8[i] = xblnb8[i]
            };
            var blnb9 = data.map(function (val) {
                return val.blnb9;
            }).join(',');
            var xblnb9 = JSON.parse("[" + blnb9 + "]");
            var jsonsblnb9 = xblnb9.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb9 = [];
            for (var i = xblnb9.length; i--;) {
                jsonsblnb9[i] = xblnb9[i]
            };
            var blnb10 = data.map(function (val) {
                return val.blnb10;
            }).join(',');
            var xblnb10 = JSON.parse("[" + blnb10 + "]");
            var jsonsblnb10 = xblnb10.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb10 = [];
            for (var i = xblnb10.length; i--;) {
                jsonsblnb10[i] = xblnb10[i]
            };
            var blnb11 = data.map(function (val) {
                return val.blnb11;
            }).join(',');
            var xblnb11 = JSON.parse("[" + blnb11 + "]");
            var jsonsblnb11 = xblnb11.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb11 = [];
            for (var i = xblnb11.length; i--;) {
                jsonsblnb11[i] = xblnb11[i]
            };
            var blnb12 = data.map(function (val) {
                return val.blnb12;
            }).join(',');
            var xblnb12 = JSON.parse("[" + blnb12 + "]");
            var jsonsblnb12 = xblnb12.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb12 = [];
            for (var i = xblnb12.length; i--;) {
                jsonsblnb12[i] = xblnb12[i]
            };




            var blnc1 = data.map(function (val) {
                return val.blnc1;
            }).join(',');
            var xblnc1 = JSON.parse("[" + blnc1 + "]");
            var jsonsblnc1 = xblnc1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc1 = [];
            for (var i = xblnc1.length; i--;) {
                jsonsblnc1[i] = xblnc1[i]
            };


            var blnc2 = data.map(function (val) {
                return val.blnc2;
            }).join(',');
            var xblnc2 = JSON.parse("[" + blnc2 + "]");
            var jsonsblnc2 = xblnc2.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc2 = [];
            for (var i = xblnc2.length; i--;) {
                jsonsblnc2[i] = xblnc2[i]
            };

            var blnc3 = data.map(function (val) {
                return val.blnc3;
            }).join(',');
            var xblnc3 = JSON.parse("[" + blnc3 + "]");
            var jsonsblnc3 = xblnc3.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc3 = [];
            for (var i = xblnc3.length; i--;) {
                jsonsblnc3[i] = xblnc3[i]
            };

            var blnc4 = data.map(function (val) {
                return val.blnc4;
            }).join(',');
            var xblnc4 = JSON.parse("[" + blnc4 + "]");
            var jsonsblnc4 = xblnc4.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc4 = [];
            for (var i = xblnc4.length; i--;) {
                jsonsblnc4[i] = xblnc4[i]
            };

            var blnc5 = data.map(function (val) {
                return val.blnc5;
            }).join(',');
            var xblnc5 = JSON.parse("[" + blnc5 + "]");
            var jsonsblnc5 = xblnc5.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc5 = [];
            for (var i = xblnc5.length; i--;) {
                jsonsblnc5[i] = xblnc5[i]
            };


            var blnc6 = data.map(function (val) {
                return val.blnc6;
            }).join(',');
            var xblnc6 = JSON.parse("[" + blnc6 + "]");
            var jsonsblnc6 = xblnc6.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc6 = [];
            for (var i = xblnc6.length; i--;) {
                jsonsblnc6[i] = xblnc6[i]
            };
            var blnc7 = data.map(function (val) {
                return val.blnc7;
            }).join(',');
            var xblnc7 = JSON.parse("[" + blnc7 + "]");
            var jsonsblnc7 = xblnc7.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc7 = [];
            for (var i = xblnc7.length; i--;) {
                jsonsblnc7[i] = xblnc7[i]
            };
            var blnc8 = data.map(function (val) {
                return val.blnc8;
            }).join(',');
            var xblnc8 = JSON.parse("[" + blnc8 + "]");
            var jsonsblnc8 = xblnc8.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc8 = [];
            for (var i = xblnc8.length; i--;) {
                jsonsblnc8[i] = xblnc8[i]
            };
            var blnc9 = data.map(function (val) {
                return val.blnc9;
            }).join(',');
            var xblnc9 = JSON.parse("[" + blnc9 + "]");
            var jsonsblnc9 = xblnc9.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc9 = [];
            for (var i = xblnc9.length; i--;) {
                jsonsblnc9[i] = xblnc9[i]
            };
            var blnc10 = data.map(function (val) {
                return val.blnc10;
            }).join(',');
            var xblnc10 = JSON.parse("[" + blnc10 + "]");
            var jsonsblnc10 = xblnc10.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc10 = [];
            for (var i = xblnc10.length; i--;) {
                jsonsblnc10[i] = xblnc10[i]
            };
            var blnc11 = data.map(function (val) {
                return val.blnc11;
            }).join(',');
            var xblnc11 = JSON.parse("[" + blnc11 + "]");
            var jsonsblnc11 = xblnc11.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc11 = [];
            for (var i = xblnc11.length; i--;) {
                jsonsblnc11[i] = xblnc11[i]
            };
            console.log(jsonsblnc10);
            console.log(jsonsblnc11);
            var blnc12 = data.map(function (val) {
                return val.blnc12;
            }).join(',');
            var xblnc12 = JSON.parse("[" + blnc12 + "]");
            var jsonsblnc12 = xblnc12.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnc12 = [];
            for (var i = xblnc12.length; i--;) {
                jsonsblnc12[i] = xblnc12[i]
            };
            //var Female = data.map(function (val) {
            //    return val.Female;
            //}).join(',');

            //var Female1 = JSON.parse("[" + Female + "]");

            //console.log(Female1);

            //var jsonsFemale = Female1.map(JSON.stringify);

            //// ...or for older browsers
            //var jsonsFemale = [];
            //for (var i = Female1.length; i--;) {
            //    jsonsFemale[i] = Female1[i]
            //};







            var color = Chart.helpers.color;

            //var bln1 = 20;
            //var bln2 = 10;
            //var bln3 = 50;
            //var bln4 = 5;
            //var bln5 = 30;
            //var bln6 = 20;
            //var bln7 = 80;
            //var bln8 = 30;
            //var bln9 = 70;
            //var bln10 = 60;
            //var bln11= 30;
            //var bln12 = 50;



            var barChartData = {
                labels: ['01', '02', '03', '04', '05', '06', '07','08','09','10','11','12'],
                datasets: [{
                    label: 'Permohonan Kredit',
                    //backgroundColor: '#6610f2',
                    borderColor: '#6610f2',
                    data: [jsonsblnp1, jsonsblnp2, jsonsblnp3, jsonsblnp4, jsonsblnp5, jsonsblnp6, jsonsblnp7, jsonsblnp8, jsonsblnp9, jsonsblnp10, jsonsblnp11, jsonsblnp12],
                    order: 1
                    },
                    {
                        label: 'Pembatalan Kredit',
                        //backgroundColor: '#20c997',
                       
                        borderColor: '#ffc107',
                        data: [jsonsblnb1, jsonsblnb2, jsonsblnb3, jsonsblnb4, jsonsblnb5, jsonsblnb6, jsonsblnb7, jsonsblnb8, jsonsblnb9, jsonsblnb10, jsonsblnb11, jsonsblnb12],
                        order: 2
                    },
                    {
                        label: 'Pencairan Kredit',
                        //backgroundColor: '#dc3545',
                        
                        borderColor: '#dc3545',
                        data: [jsonsblnc1, jsonsblnc2, jsonsblnc3, jsonsblnc4, jsonsblnc5, jsonsblnc6, jsonsblnc7, jsonsblnc8, jsonsblnc9, jsonsblnc10, jsonsblnc11, jsonsblnc12],
                        order: 3
                    }],
            
            };

            var ctx = document.getElementById("line").getContext("2d");
            var myBar = new Chart(ctx, {
                type: 'line',
                data: barChartData
            });
        }

    }

    var linepermohonan = function linepermohonan() {
        $.ajax({
            type: "POST",
            url: "WebService.asmx/ChartPermohonan",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var myData = JSON.parse(data.d);
                //console.log(myData);
                new OnSuccess(myData);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Error:" + jqXHR + textStatus + errorThrown);
            }
        });



        function OnSuccess(data) {


            var ReligonName = data.map(function (val) {
                return "\"" + val.unitbisnis + "\"";
            }).join(',');

            var ReligonName1 = JSON.parse("[" + ReligonName + "]");

            console.log(ReligonName1);

            var jsonsReligonName = ReligonName1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsReligonName = [];
            for (var i = ReligonName1.length; i--;) {
                jsonsReligonName[i] = ReligonName1[i]
            };


            //var bln1 = data.map(function (val) {
            //    return val.bln1;
            //}).join(',');
            //var xbln1 = JSON.parse("[" + bln1 + "]");  
            //var jsonsbln1 = xbln1.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln1 = [];
            //for (var i = xbln1.length; i--;) {
            //    jsonsbln1[i] = xbln1[i]
            //};


            //var bln2 = data.map(function (val) {
            //    return val.bln2;
            //}).join(',');
            //var xbln2 = JSON.parse("[" + bln2 + "]");
            //var jsonsbln2 = xbln2.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln2 = [];
            //for (var i = xbln2.length; i--;) {
            //    jsonsbln2[i] = xbln2[i]
            //};

            //var bln3 = data.map(function (val) {
            //    return val.bln3;
            //}).join(',');
            //var xbln3 = JSON.parse("[" + bln3 + "]");
            //var jsonsbln3 = xbln3.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln3 = [];
            //for (var i = xbln3.length; i--;) {
            //    jsonsbln3[i] = xbln3[i]
            //};

            //var bln4 = data.map(function (val) {
            //    return val.bln4;
            //}).join(',');
            //var xbln4 = JSON.parse("[" + bln4 + "]");
            //var jsonsbln4 = xbln4.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln4 = [];
            //for (var i = xbln4.length; i--;) {
            //    jsonsbln4[i] = xbln4[i]
            //};

            //var bln5 = data.map(function (val) {
            //    return val.bln5;
            //}).join(',');
            //var xbln5 = JSON.parse("[" + bln5 + "]");
            //var jsonsbln5 = xbln5.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln5 = [];
            //for (var i = xbln5.length; i--;) {
            //    jsonsbln5[i] = xbln5[i]
            //};


            //var bln6 = data.map(function (val) {
            //    return val.bln6;
            //}).join(',');
            //var xbln6 = JSON.parse("[" + bln6 + "]");
            //var jsonsbln6 = xbln6.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln6 = [];
            //for (var i = xbln6.length; i--;) {
            //    jsonsbln6[i] = xbln6[i]
            //};
            //var bln7 = data.map(function (val) {
            //    return val.bln7;
            //}).join(',');
            //var xbln7 = JSON.parse("[" + bln7 + "]");
            //var jsonsbln7 = xbln7.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln7 = [];
            //for (var i = xbln7.length; i--;) {
            //    jsonsbln7[i] = xbln7[i]
            //};
            //var bln8 = data.map(function (val) {
            //    return val.bln8;
            //}).join(',');
            //var xbln8 = JSON.parse("[" + bln8 + "]");
            //var jsonsbln8 = xbln8.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln8 = [];
            //for (var i = xbln8.length; i--;) {
            //    jsonsbln8[i] = xbln8[i]
            //};
            //var bln9 = data.map(function (val) {
            //    return val.bln9;
            //}).join(',');
            //var xbln9 = JSON.parse("[" + bln9 + "]");
            //var jsonsbln9 = xbln9.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln9 = [];
            //for (var i = xbln9.length; i--;) {
            //    jsonsbln9[i] = xbln9[i]
            //};
            //var bln10 = data.map(function (val) {
            //    return val.bln10;
            //}).join(',');
            //var xbln10 = JSON.parse("[" + bln10 + "]");
            //var jsonsbln10 = xbln10.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln10 = [];
            //for (var i = xbln10.length; i--;) {
            //    jsonsbln10[i] = xbln10[i]
            //};
            //var bln11 = data.map(function (val) {
            //    return val.bln11;
            //}).join(',');
            //var xbln11 = JSON.parse("[" + bln11 + "]");
            //var jsonsbln11 = xbln11.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln11 = [];
            //for (var i = xbln11.length; i--;) {
            //    jsonsbln11[i] = xbln11[i]
            //};
            //var bln12 = data.map(function (val) {
            //    return val.bln12;
            //}).join(',');
            //var xbln12 = JSON.parse("[" + bln12 + "]");
            //var jsonsbln12 = xbln12.map(JSON.stringify);
            //// ...or for older browsers
            //var jsonsbln12 = [];
            //for (var i = xbln12.length; i--;) {
            //    jsonsbln12[i] = xbln12[i]
            //};



            var blnp1 = data.map(function (val) {
                return val.blnp1;
            }).join(',');
            var xblnp1 = JSON.parse("[" + blnp1 + "]");
            var jsonsblnp1 = xblnp1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp1 = [];
            for (var i = xblnp1.length; i--;) {
                jsonsblnp1[i] = xblnp1[i]
            };


            var blnp2 = data.map(function (val) {
                return val.blnp2;
            }).join(',');
            var xblnp2 = JSON.parse("[" + blnp2 + "]");
            var jsonsblnp2 = xblnp2.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp2 = [];
            for (var i = xblnp2.length; i--;) {
                jsonsblnp2[i] = xblnp2[i]
            };

            var blnp3 = data.map(function (val) {
                return val.blnp3;
            }).join(',');
            var xblnp3 = JSON.parse("[" + blnp3 + "]");
            var jsonsblnp3 = xblnp3.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp3 = [];
            for (var i = xblnp3.length; i--;) {
                jsonsblnp3[i] = xblnp3[i]
            };

            var blnp4 = data.map(function (val) {
                return val.blnp4;
            }).join(',');
            var xblnp4 = JSON.parse("[" + blnp4 + "]");
            var jsonsblnp4 = xblnp4.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp4 = [];
            for (var i = xblnp4.length; i--;) {
                jsonsblnp4[i] = xblnp4[i]
            };

            var blnp5 = data.map(function (val) {
                return val.blnp5;
            }).join(',');
            var xblnp5 = JSON.parse("[" + blnp5 + "]");
            var jsonsblnp5 = xblnp5.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp5 = [];
            for (var i = xblnp5.length; i--;) {
                jsonsblnp5[i] = xblnp5[i]
            };


            var blnp6 = data.map(function (val) {
                return val.blnp6;
            }).join(',');
            var xblnp6 = JSON.parse("[" + blnp6 + "]");
            var jsonsblnp6 = xblnp6.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp6 = [];
            for (var i = xblnp6.length; i--;) {
                jsonsblnp6[i] = xblnp6[i]
            };
            var blnp7 = data.map(function (val) {
                return val.blnp7;
            }).join(',');
            var xblnp7 = JSON.parse("[" + blnp7 + "]");
            var jsonsblnp7 = xblnp7.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp7 = [];
            for (var i = xblnp7.length; i--;) {
                jsonsblnp7[i] = xblnp7[i]
            };
            var blnp8 = data.map(function (val) {
                return val.blnp8;
            }).join(',');
            var xblnp8 = JSON.parse("[" + blnp8 + "]");
            var jsonsblnp8 = xblnp8.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp8 = [];
            for (var i = xblnp8.length; i--;) {
                jsonsblnp8[i] = xblnp8[i]
            };
            var blnp9 = data.map(function (val) {
                return val.blnp9;
            }).join(',');
            var xblnp9 = JSON.parse("[" + blnp9 + "]");
            var jsonsblnp9 = xblnp9.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp9 = [];
            for (var i = xblnp9.length; i--;) {
                jsonsblnp9[i] = xblnp9[i]
            };
            var blnp10 = data.map(function (val) {
                return val.blnp10;
            }).join(',');
            var xblnp10 = JSON.parse("[" + blnp10 + "]");
            var jsonsblnp10 = xblnp10.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp10 = [];
            for (var i = xblnp10.length; i--;) {
                jsonsblnp10[i] = xblnp10[i]
            };
            var blnp11 = data.map(function (val) {
                return val.blnp11;
            }).join(',');
            var xblnp11 = JSON.parse("[" + blnp11 + "]");
            var jsonsblnp11 = xblnp11.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp11 = [];
            for (var i = xblnp11.length; i--;) {
                jsonsblnp11[i] = xblnp11[i]
            };
            var blnp12 = data.map(function (val) {
                return val.blnp12;
            }).join(',');
            var xblnp12 = JSON.parse("[" + blnp12 + "]");
            var jsonsblnp12 = xblnp12.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp12 = [];
            for (var i = xblnp12.length; i--;) {
                jsonsblnp12[i] = xblnp12[i]
            };


            var blnb1 = data.map(function (val) {
                return val.blnb1;
            }).join(',');
            var xblnb1 = JSON.parse("[" + blnb1 + "]");
            var jsonsblnb1 = xblnb1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb1 = [];
            for (var i = xblnb1.length; i--;) {
                jsonsblnb1[i] = xblnb1[i]
            };


            var blnb2 = data.map(function (val) {
                return val.blnb2;
            }).join(',');
            var xblnb2 = JSON.parse("[" + blnb2 + "]");
            var jsonsblnb2 = xblnb2.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb2 = [];
            for (var i = xblnb2.length; i--;) {
                jsonsblnb2[i] = xblnb2[i]
            };

            var blnb3 = data.map(function (val) {
                return val.blnb3;
            }).join(',');
            var xblnb3 = JSON.parse("[" + blnb3 + "]");
            var jsonsblnb3 = xblnb3.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb3 = [];
            for (var i = xblnb3.length; i--;) {
                jsonsblnb3[i] = xblnb3[i]
            };

            var blnb4 = data.map(function (val) {
                return val.blnb4;
            }).join(',');
            var xblnb4 = JSON.parse("[" + blnb4 + "]");
            var jsonsblnb4 = xblnb4.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb4 = [];
            for (var i = xblnb4.length; i--;) {
                jsonsblnb4[i] = xblnb4[i]
            };

            var blnb5 = data.map(function (val) {
                return val.blnb5;
            }).join(',');
            var xblnb5 = JSON.parse("[" + blnb5 + "]");
            var jsonsblnb5 = xblnb5.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb5 = [];
            for (var i = xblnb5.length; i--;) {
                jsonsblnb5[i] = xblnb5[i]
            };


            var blnb6 = data.map(function (val) {
                return val.blnb6;
            }).join(',');
            var xblnb6 = JSON.parse("[" + blnb6 + "]");
            var jsonsblnb6 = xblnb6.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb6 = [];
            for (var i = xblnb6.length; i--;) {
                jsonsblnb6[i] = xblnb6[i]
            };
            var blnb7 = data.map(function (val) {
                return val.blnb7;
            }).join(',');
            var xblnb7 = JSON.parse("[" + blnb7 + "]");
            var jsonsblnb7 = xblnb7.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb7 = [];
            for (var i = xblnb7.length; i--;) {
                jsonsblnb7[i] = xblnb7[i]
            };
            var blnb8 = data.map(function (val) {
                return val.blnb8;
            }).join(',');
            var xblnb8 = JSON.parse("[" + blnb8 + "]");
            var jsonsblnb8 = xblnb8.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb8 = [];
            for (var i = xblnb8.length; i--;) {
                jsonsblnb8[i] = xblnb8[i]
            };
            var blnb9 = data.map(function (val) {
                return val.blnb9;
            }).join(',');
            var xblnb9 = JSON.parse("[" + blnb9 + "]");
            var jsonsblnb9 = xblnb9.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb9 = [];
            for (var i = xblnb9.length; i--;) {
                jsonsblnb9[i] = xblnb9[i]
            };
            var blnb10 = data.map(function (val) {
                return val.blnb10;
            }).join(',');
            var xblnb10 = JSON.parse("[" + blnb10 + "]");
            var jsonsblnb10 = xblnb10.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb10 = [];
            for (var i = xblnb10.length; i--;) {
                jsonsblnb10[i] = xblnb10[i]
            };
            var blnb11 = data.map(function (val) {
                return val.blnb11;
            }).join(',');
            var xblnb11 = JSON.parse("[" + blnb11 + "]");
            var jsonsblnb11 = xblnb11.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb11 = [];
            for (var i = xblnb11.length; i--;) {
                jsonsblnb11[i] = xblnb11[i]
            };
            var blnb12 = data.map(function (val) {
                return val.blnb12;
            }).join(',');
            var xblnb12 = JSON.parse("[" + blnb12 + "]");
            var jsonsblnb12 = xblnb12.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb12 = [];
            for (var i = xblnb12.length; i--;) {
                jsonsblnb12[i] = xblnb12[i]
            };

           
       





            var color = Chart.helpers.color;

          

            var barChartData;
            if (ReligonName1 == 'MK') {
                barChartData = {
                    labels: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
                    datasets: [{
                        label: 'Produk Bulanan',
                        backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                        borderColor: window.chartColors.red,
                        data: [jsonsblnp1, jsonsblnp2, jsonsblnp3, jsonsblnp4, jsonsblnp5, jsonsblnp6, jsonsblnp7, jsonsblnp8, jsonsblnp9, jsonsblnp10, jsonsblnp11, jsonsblnp12],
                        order: 1
                    },
                    {
                        label: 'Produk Mingguan',
                        backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
                        borderColor: window.chartColors.blue,
                        data: [jsonsblnb1, jsonsblnb2, jsonsblnb3, jsonsblnb4, jsonsblnb5, jsonsblnb6, jsonsblnb7, jsonsblnb8, jsonsblnb9, jsonsblnb10, jsonsblnb11, jsonsblnb12],
                        order: 2
                    }],

                };
            }
            else {
                barChartData = {
                    labels: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
                    datasets: [{                    
                        label: 'Produk Gadai',
                        backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
                        borderColor: window.chartColors.blue,
                        data: [jsonsblnp1, jsonsblnp2, jsonsblnp3, jsonsblnp4, jsonsblnp5, jsonsblnp6, jsonsblnp7, jsonsblnp8, jsonsblnp9, jsonsblnp10, jsonsblnp11, jsonsblnp12],
                        order: 1
                   }],

                };
            }
         

            var ctx = document.getElementById("linepermohonan").getContext("2d");
            var myBar = new Chart(ctx, {
                type: 'line',
                data: barChartData
            });
        }

    }

    var linepencairan = function linepencairan() {
        $.ajax({
            type: "POST",
            url: "WebService.asmx/ChartPencairan",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                var myData = JSON.parse(data.d);
                //console.log(myData);
                new OnSuccess(myData);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("Error:" + jqXHR + textStatus + errorThrown);
            }
        });



        function OnSuccess(data) {


            var ReligonName = data.map(function (val) {
                return "\"" + val.unitbisnis + "\"";
            }).join(',');

            var ReligonName1 = JSON.parse("[" + ReligonName + "]");

            console.log(ReligonName1);

            var jsonsReligonName = ReligonName1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsReligonName = [];
            for (var i = ReligonName1.length; i--;) {
                jsonsReligonName[i] = ReligonName1[i]
            };


            var blnp1 = data.map(function (val) {
                return val.blnp1;
            }).join(',');
            var xblnp1 = JSON.parse("[" + blnp1 + "]");
            var jsonsblnp1 = xblnp1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp1 = [];
            for (var i = xblnp1.length; i--;) {
                jsonsblnp1[i] = xblnp1[i]
            };


            var blnp2 = data.map(function (val) {
                return val.blnp2;
            }).join(',');
            var xblnp2 = JSON.parse("[" + blnp2 + "]");
            var jsonsblnp2 = xblnp2.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp2 = [];
            for (var i = xblnp2.length; i--;) {
                jsonsblnp2[i] = xblnp2[i]
            };

            var blnp3 = data.map(function (val) {
                return val.blnp3;
            }).join(',');
            var xblnp3 = JSON.parse("[" + blnp3 + "]");
            var jsonsblnp3 = xblnp3.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp3 = [];
            for (var i = xblnp3.length; i--;) {
                jsonsblnp3[i] = xblnp3[i]
            };

            var blnp4 = data.map(function (val) {
                return val.blnp4;
            }).join(',');
            var xblnp4 = JSON.parse("[" + blnp4 + "]");
            var jsonsblnp4 = xblnp4.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp4 = [];
            for (var i = xblnp4.length; i--;) {
                jsonsblnp4[i] = xblnp4[i]
            };

            var blnp5 = data.map(function (val) {
                return val.blnp5;
            }).join(',');
            var xblnp5 = JSON.parse("[" + blnp5 + "]");
            var jsonsblnp5 = xblnp5.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp5 = [];
            for (var i = xblnp5.length; i--;) {
                jsonsblnp5[i] = xblnp5[i]
            };


            var blnp6 = data.map(function (val) {
                return val.blnp6;
            }).join(',');
            var xblnp6 = JSON.parse("[" + blnp6 + "]");
            var jsonsblnp6 = xblnp6.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp6 = [];
            for (var i = xblnp6.length; i--;) {
                jsonsblnp6[i] = xblnp6[i]
            };
            var blnp7 = data.map(function (val) {
                return val.blnp7;
            }).join(',');
            var xblnp7 = JSON.parse("[" + blnp7 + "]");
            var jsonsblnp7 = xblnp7.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp7 = [];
            for (var i = xblnp7.length; i--;) {
                jsonsblnp7[i] = xblnp7[i]
            };
            var blnp8 = data.map(function (val) {
                return val.blnp8;
            }).join(',');
            var xblnp8 = JSON.parse("[" + blnp8 + "]");
            var jsonsblnp8 = xblnp8.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp8 = [];
            for (var i = xblnp8.length; i--;) {
                jsonsblnp8[i] = xblnp8[i]
            };
            var blnp9 = data.map(function (val) {
                return val.blnp9;
            }).join(',');
            var xblnp9 = JSON.parse("[" + blnp9 + "]");
            var jsonsblnp9 = xblnp9.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp9 = [];
            for (var i = xblnp9.length; i--;) {
                jsonsblnp9[i] = xblnp9[i]
            };
            var blnp10 = data.map(function (val) {
                return val.blnp10;
            }).join(',');
            var xblnp10 = JSON.parse("[" + blnp10 + "]");
            var jsonsblnp10 = xblnp10.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp10 = [];
            for (var i = xblnp10.length; i--;) {
                jsonsblnp10[i] = xblnp10[i]
            };
            var blnp11 = data.map(function (val) {
                return val.blnp11;
            }).join(',');
            var xblnp11 = JSON.parse("[" + blnp11 + "]");
            var jsonsblnp11 = xblnp11.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp11 = [];
            for (var i = xblnp11.length; i--;) {
                jsonsblnp11[i] = xblnp11[i]
            };
            var blnp12 = data.map(function (val) {
                return val.blnp12;
            }).join(',');
            var xblnp12 = JSON.parse("[" + blnp12 + "]");
            var jsonsblnp12 = xblnp12.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnp12 = [];
            for (var i = xblnp12.length; i--;) {
                jsonsblnp12[i] = xblnp12[i]
            };


            var blnb1 = data.map(function (val) {
                return val.blnb1;
            }).join(',');
            var xblnb1 = JSON.parse("[" + blnb1 + "]");
            var jsonsblnb1 = xblnb1.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb1 = [];
            for (var i = xblnb1.length; i--;) {
                jsonsblnb1[i] = xblnb1[i]
            };


            var blnb2 = data.map(function (val) {
                return val.blnb2;
            }).join(',');
            var xblnb2 = JSON.parse("[" + blnb2 + "]");
            var jsonsblnb2 = xblnb2.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb2 = [];
            for (var i = xblnb2.length; i--;) {
                jsonsblnb2[i] = xblnb2[i]
            };

            var blnb3 = data.map(function (val) {
                return val.blnb3;
            }).join(',');
            var xblnb3 = JSON.parse("[" + blnb3 + "]");
            var jsonsblnb3 = xblnb3.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb3 = [];
            for (var i = xblnb3.length; i--;) {
                jsonsblnb3[i] = xblnb3[i]
            };

            var blnb4 = data.map(function (val) {
                return val.blnb4;
            }).join(',');
            var xblnb4 = JSON.parse("[" + blnb4 + "]");
            var jsonsblnb4 = xblnb4.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb4 = [];
            for (var i = xblnb4.length; i--;) {
                jsonsblnb4[i] = xblnb4[i]
            };

            var blnb5 = data.map(function (val) {
                return val.blnb5;
            }).join(',');
            var xblnb5 = JSON.parse("[" + blnb5 + "]");
            var jsonsblnb5 = xblnb5.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb5 = [];
            for (var i = xblnb5.length; i--;) {
                jsonsblnb5[i] = xblnb5[i]
            };


            var blnb6 = data.map(function (val) {
                return val.blnb6;
            }).join(',');
            var xblnb6 = JSON.parse("[" + blnb6 + "]");
            var jsonsblnb6 = xblnb6.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb6 = [];
            for (var i = xblnb6.length; i--;) {
                jsonsblnb6[i] = xblnb6[i]
            };
            var blnb7 = data.map(function (val) {
                return val.blnb7;
            }).join(',');
            var xblnb7 = JSON.parse("[" + blnb7 + "]");
            var jsonsblnb7 = xblnb7.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb7 = [];
            for (var i = xblnb7.length; i--;) {
                jsonsblnb7[i] = xblnb7[i]
            };
            var blnb8 = data.map(function (val) {
                return val.blnb8;
            }).join(',');
            var xblnb8 = JSON.parse("[" + blnb8 + "]");
            var jsonsblnb8 = xblnb8.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb8 = [];
            for (var i = xblnb8.length; i--;) {
                jsonsblnb8[i] = xblnb8[i]
            };
            var blnb9 = data.map(function (val) {
                return val.blnb9;
            }).join(',');
            var xblnb9 = JSON.parse("[" + blnb9 + "]");
            var jsonsblnb9 = xblnb9.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb9 = [];
            for (var i = xblnb9.length; i--;) {
                jsonsblnb9[i] = xblnb9[i]
            };
            var blnb10 = data.map(function (val) {
                return val.blnb10;
            }).join(',');
            var xblnb10 = JSON.parse("[" + blnb10 + "]");
            var jsonsblnb10 = xblnb10.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb10 = [];
            for (var i = xblnb10.length; i--;) {
                jsonsblnb10[i] = xblnb10[i]
            };
            var blnb11 = data.map(function (val) {
                return val.blnb11;
            }).join(',');
            var xblnb11 = JSON.parse("[" + blnb11 + "]");
            var jsonsblnb11 = xblnb11.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb11 = [];
            for (var i = xblnb11.length; i--;) {
                jsonsblnb11[i] = xblnb11[i]
            };
            var blnb12 = data.map(function (val) {
                return val.blnb12;
            }).join(',');
            var xblnb12 = JSON.parse("[" + blnb12 + "]");
            var jsonsblnb12 = xblnb12.map(JSON.stringify);
            // ...or for older browsers
            var jsonsblnb12 = [];
            for (var i = xblnb12.length; i--;) {
                jsonsblnb12[i] = xblnb12[i]
            };








            var color = Chart.helpers.color;



            var barChartData;
            if (ReligonName1 == 'MK') {
                barChartData = {
                    labels: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
                    datasets: [{
                        label: 'Produk Bulanan',
                        backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                        borderColor: window.chartColors.red,
                        data: [jsonsblnp1, jsonsblnp2, jsonsblnp3, jsonsblnp4, jsonsblnp5, jsonsblnp6, jsonsblnp7, jsonsblnp8, jsonsblnp9, jsonsblnp10, jsonsblnp11, jsonsblnp12],
                        order: 1
                    },
                    {
                        label: 'Produk Mingguan',
                        backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
                        borderColor: window.chartColors.blue,
                        data: [jsonsblnb1, jsonsblnb2, jsonsblnb3, jsonsblnb4, jsonsblnb5, jsonsblnb6, jsonsblnb7, jsonsblnb8, jsonsblnb9, jsonsblnb10, jsonsblnb11, jsonsblnb12],
                        order: 2
                    }],

                };
            }
            else {
                barChartData = {
                    labels: ['01', '02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12'],
                    datasets: [{
                        label: 'Produk Gadai',
                        backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
                        borderColor: window.chartColors.blue,
                        data: [jsonsblnp1, jsonsblnp2, jsonsblnp3, jsonsblnp4, jsonsblnp5, jsonsblnp6, jsonsblnp7, jsonsblnp8, jsonsblnp9, jsonsblnp10, jsonsblnp11, jsonsblnp12],
                        order: 1
                    }],

                };
            }


            var ctx = document.getElementById("linepencairan").getContext("2d");
            var myBar = new Chart(ctx, {
                type: 'line',
                data: barChartData
            });
        }

    }

  
    function start() {
        //doughnut();
        bar();       
        line();
        linepermohonan();
        linepencairan();
         } start();

});