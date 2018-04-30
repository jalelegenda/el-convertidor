const path = require('path');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const webpack = require('webpack');

module.exports = {
    entry: ['./js/ImageFormViewModel.js'],
    output: {   
        path: path.resolve(__dirname, 'dist'),
        filename: '[name].js'
    },
    //module: {
    //    rules: [
    //        {
    //            test: /\.css$/,
    //            use: ExtractTextPlugin.extract({
    //                fallback: 'style-loader',
    //                use: 'css-loader'
    //            })
    //        }
    //    ]
    //},
    plugins: [
        //new ExtractTextPlugin('dist/style.css')
    ]
}