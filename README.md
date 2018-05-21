# El convertidor

## Introduction

A simple image converter, with the ability to convert .jpeg, .gif and .bmp image formats into a multipage .tiff file.

## Tech stack

This project uses a modest tech stack:

* vanilla CSS for style (frameworkless, transpilerless)
* npm for front-end package management
* webpack for bundling
* knockout.js for front-end databinding and UI
* asp.net MVC 5 for back-end http request handling
* entity framework 6 for data persistence
* autofac for glue

## Design

The back end of the app is DDD inspired.

* Core assembly contains blueprints
* Data assembly implements EF for persistence control
* Business assembly exposes service implementations
* Web implements MVC for request handling and is also the composition root
* Single controller handles all requests
* Single view is rendered, making this a SPA
* fetch API is used for AJAX calls
