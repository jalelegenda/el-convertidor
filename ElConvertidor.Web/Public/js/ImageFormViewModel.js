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
        let files = element.files,
            formData = new FormData(),
            tempImages = [];

        // prevent request when page is loaded
        if(files.length < 1) {
            return;
        }

        for(let i = 0; i < files.length; i++) {
            if(mimeTypes.indexOf(files[i].type) > -1) {
                formData.append(`images[${i}].file`, files[i]);
                tempImages.push(new ImageModel(files[i]));
            }
        }
        console.log(tempImages);
        console.log(self.images)
        fetch('/Home/AddImagesToSession', {
            body: formData, 
            method: 'POST',
            cache: 'no-cache'
        })
        .then(
            (res) => { self.images(self.images().concat(tempImages)); },
            (err) => { alert(err); }
        );
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