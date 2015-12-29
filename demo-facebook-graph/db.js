var pg = require('pg');

// var createTable = "CREATE TABLE boxes
// (
//   handle text NOT NULL,
//   payload json,
//   CONSTRAINT pk_boxes PRIMARY KEY (handle)
// )";


var name = process.env.LOGNAME;
var conString = "postgres://" + name + ":@localhost/fbharvest";

module.exports = function(callback){
  var client = new pg.Client(conString);
  client.connect(function(err) {
    if(err) {
      return console.error('PG: could not connect to postgres', err);
    }

    callback(client);

    // console.log("CLOSING CONNECTION");
    //client.end();
  });
}
