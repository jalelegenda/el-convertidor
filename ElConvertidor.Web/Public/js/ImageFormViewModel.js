const ko = require('knockout');
const ImageModel = require('./ImageModel');

function ImageFormViewModel() {
    const self = this;

    self.images = ko.observableArray([]);

    self.addImage = function () {
        self.images.push(new ImageModel("TEST"));
    }

    self.removeImage = function (image) {
        self.images.remove(image);
    }

    self.hasImages = ko.computed(() => {
        return self.images().length < 1 ? true : false;
    });
}

ko.applyBindings(new ImageFormViewModel);