const ko = require('knockout');

module.exports =
    function ImageModel(name) {
        this.name = ko.observable(name);
    }