var _ = require('underscore'),
    request = require('request'),
    with_fb = require('./fb.js'),
    boxes = require('./boxes.js'),
    with_db = require('./db.js'),
    rateLimiter = require('limiter').RateLimiter;

var limit = new rateLimiter(2, 1000);

_.each(boxes, function(box){

  limit.removeTokens(1, function(err, remainingRequests){

    with_fb(box, function(payload){

      with_db(function(db) {

        var sql = "INSERT INTO boxes (handle, payload) VALUES ('" + box + "', '" + payload + "')";

        db.query(sql, function(err, result) {
          if(err) {
            console.log("sql:", sql)
            return console.error('PG: error running query', err);
          }

          db.end();
          console.log('PG: entered data for ' + box)

        }); //end query

      });//end connection

    }); //end fb


  }); //end rate limiter

}); //end each

