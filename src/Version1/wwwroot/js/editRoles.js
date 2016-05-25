// =================== View: EditRoles ===================

//// Post back to formEditRole.
//$("#formEditRole").click(function (event) {
//    var form = $(this);
//    var postURL = "Volunteer/EditUserRole";
//    $.post(postURL, form.serialize());
//});
//});

// Switch value and class by click the button .btneditRole.

$(document).ready(function () {
    $('.btneditRole').click(function (event) {
        event.preventDefault();
        if ($(this).attr('value') == 'true') {
            $(this).attr('value', 'false');
            $(this).removeClass('glyphicon-ok').addClass('glyphicon-remove');
        }
        else if ($(this).attr('value') == 'false') {
            $(this).attr('value', 'true');
            $(this).removeClass('glyphicon-remove').addClass('glyphicon-ok');
        }
    });

    $('#formEditRole').submit(function (event) {
        event.preventDefault();

        // Create an array for value of the dict. 
        var listForChecks;

        // Create an dict for post back to view model.
        var dict = new Object();

        // Select rows in a table except the header
        //var rows = $('table tbody tr');

        // The list of roles that will be posted back. It stores the order of roles.
        var roleNames = new Object();
        $('tr:first th:gt(0)').children().each(function (index) {
            roleNames[index] = $(this).val();
        });

        $('table tbody tr').each(function (index) {
            // New list for each row.
            listForChecks = new Object();

            // Insert value to list
            $(this).children().children('button').each(function (index) {
                listForChecks[index] = $(this).val();
            });

            var userId = $(this).children().children('input').val();

            // Store all in dict.
            dict[userId] = listForChecks;
        });
        var postUrl = '/Volunteer/EditRoles';
        $.post(postUrl, { PostBackDict: dict, RoleNames: roleNames });
    });
});

//var dict = new Object();
//dict['13'] = 9;
//dict['14'] = 10;
//dict['2'] = 5;

//$.post('controller.mvc/Test', { 'kvPairs': dict }, function (obj) { $('#output').html(obj.Count); });

//=================== View: EditRoles ===================

