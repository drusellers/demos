var request = require('request');

var url = "http://graph.facebook.com/",
    fields = "?fields=likes,checkins,location,link,were_here_count,talking_about_count,name";


module.exports = function(boxAlias, callback){
  request(url + boxAlias + fields, function(err, response, body){
    if(err){
      return console.log("FB:", err);
    }

    callback(body);
  });
}
