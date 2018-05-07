import ko from 'knockout';

export default function ImageModel(name, type) {
    const self = this;

    self.name = ko.observable(name);
    self.type = ko.observable(type);
}