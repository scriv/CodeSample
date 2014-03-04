var lastVisibleInfoBox = null;
var map;

// Initialise the page
$(function () {
    // Initialise the Bing map
    map = new Microsoft.Maps.Map(document.getElementById("CatchMap"), {
        credentials: 'AuId-ysksHct80P8RnjD_bEDmGl6OvDvGUQvboU2I1ue0X7IOXOkvvA6tv1SWG0q',
        center: new Microsoft.Maps.Location(-38.093498, 144.876022),
        mapTypeId: Microsoft.Maps.MapTypeId.road,
        zoom: 10,
        showMapTypeSelector: false,
        disableZooming: true
    });

    wireEvents();
    loadCatches();
})

function wireEvents() {
    Microsoft.Maps.Events.addHandler(map, 'dblclick', beginAddCatch);
    Microsoft.Maps.Events.addHandler(map, 'viewchange', hideActiveInfoBox);
}

// Load all catches from the API and display them
function loadCatches() {
    $.getJSON($('#CatchMap').data('source'))
    .done(placePins)
    .fail(function () {
        // Add the failure message to the first container
        $($('.container.body-content')[0]).append($('<div></div>').loadTemplate('#CatchesLoadFailedTemplate'));
    });
}

function placePins(catches) {
    // Remove existing pins
    map.entities.clear();

    var catchPins = new Microsoft.Maps.EntityCollection();

    // Add a push pin and associated info box for each catch
    $.each(catches, function (index, catchItem) {
        var location = new Microsoft.Maps.Location(catchItem.Latitude, catchItem.Longitude);
        var infoBox = new Microsoft.Maps.Infobox(location,
            {
                visible: false,
                offset: new Microsoft.Maps.Point(0, 15),
                height: 100,
                width: 300,
                showCloseButton: false,
                showPointer: false
            });

        // Get the info box content from a template
        var infoBoxHtml = $('<div></div>');
        infoBoxHtml.loadTemplate('#CatchInfoBoxTemplate', catchItem);
        infoBox.setHtmlContent(infoBoxHtml.html());

        var pin = new Microsoft.Maps.Pushpin(location, { icon: $('#CatchMap').data('pinIcon'), width: 50, height: 23, infobox: infoBox });

        catchPins.push(pin);
        catchPins.push(infoBox);

        Microsoft.Maps.Events.addHandler(pin, 'click', toggleInfoBox);
    });

    map.entities.push(catchPins);
}

// Loads the New Catch modal
function beginAddCatch(e) {
    var point = new Microsoft.Maps.Point(e.getX(), e.getY());
    var location = e.target.tryPixelToLocation(point);

    $('#Latitude').val(location.latitude);
    $('#Longitude').val(location.longitude);
    var mapUrl = $('#CreateCatchForm img').data('srcTemplate').replace(/LATITUDE/g, location.latitude).replace(/LONGITUDE/g, location.longitude);
    $('#CreateCatchForm img').attr('src', mapUrl);

    $('#CreateCatchForm').modal({ show: true })

    $('#NewCatch').click(addCatch);
}

// Adds a catch to the system
function addCatch(e) {
    var form = $('#CreateCatchForm form')
    var isValid = form.valid();

    if (isValid) {
        $.post(form.attr('action'), form.serialize(), function (data) {
            // Reload the catches, hide the modal and clear the form
            loadCatches();
            $('#CreateCatchForm').modal('hide');
            form[0].reset();
        });
    }
}

function catchLiked(e) {
    var button = $(e.target);
    var urlTemplate = button.data('url');
    urlTemplate = urlTemplate.replace('TEMP', button.data('id'));

    button.data('url', urlTemplate);

    $.post(urlTemplate, function (data) {
        // Change the button text and disable it
        button.text(button.data('altText'));
        button.prop('disabled', true);
    });
}

// Toggles the visibility of the info box associated with the target push pin
function toggleInfoBox(e) {
    // Hide any existing info boxes
    if (lastVisibleInfoBox && e.target._infobox != lastVisibleInfoBox) {
        lastVisibleInfoBox.setOptions({ visible: false });
    }

    if (e.target._infobox._visible) {
        $('#CatchMap .panel-footer button').unbind('click');
        e.target._infobox.setOptions({ visible: false });
    } else {
        e.target._infobox.setOptions({ visible: true });
        lastVisibleInfoBox = e.target._infobox;

        $('#CatchMap .panel-footer button').click(catchLiked);
    }
}

// Hides the active info box (if one exists)
function hideActiveInfoBox(e) {
    if (lastVisibleInfoBox) {
        lastVisibleInfoBox.setOptions({ visible: false });
    }
}