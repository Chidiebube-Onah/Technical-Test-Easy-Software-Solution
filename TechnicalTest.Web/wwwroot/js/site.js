// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#patients').DataTable({
        select: true,
        responsive: true,
        autoWidth: false,
        "bFilter": true,
        language: {
            search: "",
            searchPlaceholder: "Search",
            sLengthMenu: "_MENU_items"
        },

        ajax: {
            url: "/home/getallpatients",
            dataSrc: ''
        },
        columns: [
            {
                data: "fullName",
                render: function (data, type, patient) {

                    return '</a><a href="/Home/UpdatePatient/' + patient.id + '">' + data + '</a>';
                }

            },
            { data: "cardNo" },

            {
                data: "id",
                render: function (data, type, patient) {

                    return '</a><a href="/Home/AddPatientVisit/' + data + '"> Add Visit </a>';
                }

            }
        ]
          
    });
});


$(document).ready(function () {
    $('#visits').DataTable({
        select: true,
        responsive: true,
        autoWidth: false,
        "bFilter": true,
        language: {
            search: "",
            searchPlaceholder: "Search",
            sLengthMenu: "_MENU_items"
        },

        ajax: {
            url: "/home/getallvisits",
            dataSrc: ''
        },
        columns: [
            {
                data: "patientFullname",
                render: function (data, type, visit) {

                    return '</a><a href="/Home/UpdatePatient/' + visit.patientId + '">' + data + '</a>';
                }

            },
            { data: "patientCardNo" },
            { data: "cameToSee" },
            { data: "reason" },
            { data: "signIn" },
            { data: "signOut" },

            {
                data: "id",
                render: function (data, type, patient) {
                    if (patient.signOut !== null) {
                        return '</a><a href="/Home/UpdatePatientVisit/' + data + '"> Update Visit </a>';
                    }

                    return '</a><a href="/Home/UpdatePatientVisit/' + data + '"> Update Visit </a> | </a><a href="/Home/SignOutPatientVisit/' + data + '"> Signout Visit </a>';
                   
                }

            }
        ]

    });
});


