//const ko = require('knockout');
import ko from 'knockout';
const ImageModel = require('./ImageModel');

function ImageFormViewModel() {
    const self = this;
    const mimeTypes = [
        'image/jpeg',
        'image/gif',
        'image/bmp'
    ];

    self.images = ko.observableArray([]);


    self.addImages = function (element) {
        let files = element.files;
        let formData = new FormData();

        for(let i = 0; i < files.length; i++) {
            if(mimeTypes.indexOf(files[i].type) > -1) {
                formData.append('image', files[i]);
                fetch('/Home/AddImageToSession', {
                    body: formData, 
                    method: 'POST'
                });
                self.images.push(new ImageModel(files[i]));
            }
        }
    }

    self.removeImage = function (image) {
        self.images.remove(image);
    }

    self.uploadImages = function() {

    }

    self.hasImages = ko.computed(() => {
        return self.images().length < 1 ? true : false;
    });
}

ko.applyBindings(new ImageFormViewModel);