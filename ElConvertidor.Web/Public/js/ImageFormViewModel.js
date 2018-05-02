import ko from 'knockout';
import ajax from './Ajax';
import ImageModel from './ImageModel';

export default function ImageFormViewModel() {
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
                formData.append(`images[${i}].File`, files[i]);
                tempImages.push(new ImageModel(files[i]));
            }
        }

        ajax('/Home/AddImages', formData,
            res => { self.images(self.images().concat(tempImages)); },
            err => { aler(err); });
    }

    self.removeImage = function (image) {
        let formData = new FormData();
        formData.append('image.File', image.file)
        ajax('/Home/RemoveImage', formData,
            res => { self.images.remove(image); },
            err => { aler(err); });
    }

    self.uploadImages = function() {
        if(self.images().length < 1){
            return;
        }
        ajax('/Home/UploadImages', null,
            res => { self.images.remove(image); },
            err => { aler(err); });
    }

    self.hasImages = ko.computed(() => {
        return self.images().length < 1 ? true : false;
    });
}

ko.applyBindings(new ImageFormViewModel);