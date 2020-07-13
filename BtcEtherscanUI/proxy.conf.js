// For more settings: https://github.com/chimurai/http-proxy-middleware

// Filter to match anything that isn't the current app (hardcoded)
var filter = function(pathname, req) {
  return pathname.match('^(?!/etherscan).?');
};

// Config to route local development API and all other environment apis
const PROXY_CONFIG = [
  {
    context: '/etherscan/api/',
    target: 'https://localhost:5001',
    secure: false,
    //logLevel: 'debug',
    pathRewrite: {
      '^/etherscan': ''
    }
  },
  {
    context: filter,
    target: 'http://localhost:4200/etherscan/',
    secure: false,
    //logLevel: 'debug',
    changeOrigin: true,
    xfwd: true // Add the headers to say what address this was forwarded for
    // onProxyReq: function(proxyReq, req, res) {
    //   // Print out some debug information for each request
    //   console.log('========CURRENT REQUEST HEADERS========');
    //   console.log(proxyReq);
    // }
  }
];

module.exports = PROXY_CONFIG;

