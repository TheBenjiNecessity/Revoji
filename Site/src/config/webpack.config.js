module.exports = {
    mode: 'development',
    entry: {
        app: './src/main.ts'
    },
    output: {
        filename: 'bundle.js',
        publicPath: '/'
    },
    module: {
        rules: [{
            test: /\.ts$/,
            use: 'ts-loader'
        }, {
            test: /(components|shared).+\.html$/,//TODO
            use: { loader: 'html-loader' }
        }, {
            test: /\.scss$/,
            use: [
                { loader: "style-loader" },
                { loader: "css-loader" },
                { loader: "sass-loader" }
            ]
        }]
    },
    resolve: {
        extensions: [ '.ts', '.js', '.scss' ]
    },
    devServer: {
        host: '0.0.0.0',
        https: true,
        port: 8000,
        historyApiFallback: true,
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET",
            "Access-Control-Allow-Headers": "Content-Type",
            "Access-Control-Request-Headers": "origin"
        },
        proxy: [{
            context: ['/api', '/service-api', '/connect/token'],
            target: 'http://localhost:5001',
            secure: false
        }]
	}
};
//HtmlWebpackPlugin