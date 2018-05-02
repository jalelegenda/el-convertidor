import ko from 'knockout';

export default function ImageModel(file) {
    const self = this;

    self.file = file;
    self.name = ko.observable(file.name);
};