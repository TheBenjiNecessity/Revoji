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
        }]
	},
	resolve: {
		extensions: [ '.ts', '.js', '.po', '.scss' ]
	}
};
//HtmlWebpackPlugin