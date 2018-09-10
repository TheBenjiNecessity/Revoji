module.exports = {
    mode: 'development',
    entry: {
        app: './src/main.ts'
    },
    output: {
        filename: 'bundle.js'
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
        headers: {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Methods": "GET",
            "Access-Control-Allow-Headers": "Content-Type",
            "Access-Control-Request-Headers": "origin"
        },
        proxy: {
            '/api': {
                target: 'http://localhost:5001',
                changeOrigin: true
            },
            '/service-api': {
                target: 'http://localhost:5001',
                changeOrigin: true
            }
        }
	}
};
//HtmlWebpackPlugin