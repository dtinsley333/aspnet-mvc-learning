
$(document).ready(function () {

    // hides the slickbox as soon as the DOM is ready

    $('#divToOpen').hide();
 


    // toggles the slickbox on clicking the noted link  

    $('#opener').click(function () {

        $('#divToOpen').toggle(400);

        return false;

    });


    $('#divToOpen').click(function () {

        $('#divToOpen').hide();


        return false;

    });


});

