const path = require('path');
const UglifyJS = require('uglifyjs-webpack-plugin');
const MiniCssPlugin = require('mini-css-extract-plugin');
const OptimizeCSS = require('optimize-css-assets-webpack-plugin');

module.exports = {
    entry: ['./js/ImageFormViewModel.js', './css/main.css'],
    output: {   
        path: path.resolve(__dirname, 'dist'),
        filename: '[name].js'
    },
    optimization: {
        minimizer: [
            new UglifyJS({
                cache: true,
                parallel: true,
                sourceMap: true
            }),
            new OptimizeCSS()
        ]
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: 'babel-loader',
                    //options: {
                    //    presets: ['@babel/preset-env']
                    //}
                }
            },
            {
                test: /\.css$/,
                use: [
                    MiniCssPlugin.loader,
                    'css-loader'
                ]
            }
        ]
    },
    plugins: [
        new MiniCssPlugin({
            filename: '[name].css'
        }),
    ]
}