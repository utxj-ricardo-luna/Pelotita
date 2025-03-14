const mysql = require('mysql2');

const pool = mysql.createPool({
    host: '127.0.0.1',
    user: 'root',      // tu usuario de MySQL
    password: '1234',      // tu contraseña de MySQL
    database: 'dbjuego',
    port: '3307'
});

module.exports = pool.promise();
