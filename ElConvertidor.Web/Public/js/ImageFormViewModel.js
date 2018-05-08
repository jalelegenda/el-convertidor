import ko from 'knockout';
import ImageModel from './ImageModel';
import ErrorModel from './ErrorModel';


export default function ImageFormViewModel() {
    const self = this;
    const mimeTypes = [
        'image/jpeg',
        'image/gif',
        'image/bmp'
    ];

    self.images = ko.observableArray();
    self.errors = ko.observableArray();

    /* session checker */

    document.addEventListener("DOMContentLoaded", () => {
        self.errors([]);

        fetch('/Home/GetSessionImages', {
            method: 'POST',
            credentials: 'include'
        })
            .then(handleErrors)
            .then( res => { return res.json(); })
            .then( data => { 
                if(data !== null) {
                    var arr = data.map(i => new ImageModel(i.Id, i.Name, i.Type));
                    self.images(arr);
                }});
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
                tempImages.push(new ImageModel(self.images().length,
                    files[i].name, files[i].type));
            }
        }

        fetch('/Home/AddImages', {
            body: formData,
            method: 'POST',
            credentials: 'include'
        })
            .then(handleErrors)
            .then(res => { self.images(self.images().concat(tempImages)); })
            .catch(addError);

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
        })
            .then(handleErrors)
            .then(res => { self.images.remove(image); })
            .catch(addError);
    };


    self.convertImages = () => {
        if(self.hasNoImages()){
            return;
        }
        fetch('Home/ConvertImages', {
            method: 'POST',
            credentials: 'include'
        })
            .then(handleErrors)
            .then(res => {
                self.images([]);
                return res.blob();
            })
            .then(blob => { return URL.createObjectURL(blob); })
            .then(url => { window.open(url, "_blank"); URL.revokeObjectURL(url); })
            .catch(addErrorAndReset);
    };


    self.clearImages = () => {
        fetch('Home/ClearImages', {
            method: 'POST',
            credentials: 'include'
        })
            .then(handleErrors)
            .then(res => { self.images([]); })
            .catch(addErrorAndReset);
    }


    self.hasNoImages = () => {
        return self.images().length < 1;
    }


    /* helpers */

    function handleErrors(res) {
        if(!res.ok){
            throw new Error(res.statusText);
        }
        return res;
    }

    function addError(err) {
        let em = new ErrorModel(err);
        self.errors.push(em);
        setTimeout(() => {
            self.errors.remove(em);
        }, 10000)
    }

    function addErrorAndReset(err) {
        addError(err);
        self.images([]);
    }
}

ko.applyBindings(new ImageFormViewModel);