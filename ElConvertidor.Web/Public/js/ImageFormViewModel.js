import ko from 'knockout';
import ImageModel from './ImageModel';


export default function ImageFormViewModel() {
    const self = this;
    const mimeTypes = [
        'image/jpeg',
        'image/gif',
        'image/bmp'
    ];

    self.images = ko.observableArray([]);

    /* session checker */

    document.addEventListener("DOMContentLoaded", () => {
        fetch('/Home/GetSessionImages', {
            Accept: 'application/json',
            method: 'POST',
            credentials: 'include'
        }).then(
        res => {
            return res.json();
        }).then(
        data => {
            if(data !== null) {
                var arr = data.map(i => new ImageModel(i.Id, i.Name, i.Type));
                self.images(arr);
            }
        }).catch(err => { });
    });


    /* handlers */

    self.addImages =  (element) => {
        let files = Array.from(element.files),
            formData = new FormData(),
            tempImages = [];

        // prevent request when page is loaded
        if(files.length < 1) {
            return;
        }

        for(let i = 0; i < files.length; i++) {
            if(mimeTypes.indexOf(files[i].type) > -1) {
                formData.append(`images[${i}].File`, files[i]);
                tempImages.push(new ImageModel(self.images().length, files[i].name, files[i].type));
            }
        }

        fetch('/Home/AddImages', {
            body: formData,
            method: 'POST',
            credentials: 'include'
        }).then(res => {
            self.images(self.images().concat(tempImages));
        }, err => {
            alert(err);
        });

        element.value = "";
    };


    self.removeImage = (image) => {
        let formData = new FormData();
        formData.append('image.id', image.id);
        formData.append('image.name', image.name);
        formData.append('image.type', image.type);
        fetch('/Home/RemoveImage', {
            body: formData,
            method: 'POST',
            credentials: 'include'
        }).then(res => {
            self.images.remove(image);
        }, err => { 
            alert(err);
        });
    };


    self.convertImages = () => {
        if(self.hasNoImages()){
            return;
        }
        fetch('Home/ConvertImages', {
            method: 'POST',
            credentials: 'include'
        }).then(res => {
            self.images([]);
            return res.blob();
        }).then(blob => {
            return URL.createObjectURL(blob)
        }).then(url => {
            window.open(url, "_blank");
            URL.revokeObjectURL(url);
        }).catch(err => {
            alert(err);
        });
    };


    self.clearImages = () => {
        fetch('Home/ClearImages', {
            method: 'POST',
            credentials: 'include'
        }).then(res => {
            self.images([]);
        }, err => {
            alert(err);
        });
    }


    self.hasNoImages = () => {
        return self.images().length < 1;
    }
}

ko.applyBindings(new ImageFormViewModel);