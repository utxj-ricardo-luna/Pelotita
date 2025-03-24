const mysql = require('mysql2');

const pool = mysql.createPool({
    host: process.env.HOST,
    user: process.env.USER,      // tu usuario de MySQL
    password: process.env.PASSWORD,      // tu contraseña de MySQL
    database: process.env.DATABASE,
    port: process.env.PORT
});

module.exports = pool.promise();
