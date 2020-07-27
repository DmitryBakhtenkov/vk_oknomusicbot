var select = myForm.select


var request = new XMLHttpRequest();
request.open("GET", "http://188.227.84.24/api/settings", false);
request.send();
    var objects = JSON.parse(request.responseText)
    for (index in objects) {
        select.options[index] = new Option(objects[index].trigger, objects[index].trigger)
    }
    