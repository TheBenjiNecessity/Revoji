module.exports = {
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
        },{
            test: /(components|shared).+\.html$/,
            use: { loader: 'html-loader' }
        }]
	},
	resolve: {
		extensions: [ '.ts', '.js', '.po', '.scss' ]
	},
	devServer: {
         host: '0.0.0.0',
         https: true,
         port: 8000,
         proxy: {
            '/api': {
               target: 'http://localhost:5001',
               pathRewrite: { '^/api': '' },
               changeOrigin: true
            }
         }
	}
};
//HtmlWebpackPlugin