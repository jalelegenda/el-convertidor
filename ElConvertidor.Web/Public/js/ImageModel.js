const ko = require('knockout');

module.exports =
    function ImageModel(file) {
        const self = this;

        self.file = file;
        self.name = ko.observable(file.name);
    }