const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');
const db = require('../config/db');

// Crear un nuevo jugador
exports.crearJugador = async (req, res) => {
    const { first_name, last_name, email, phone, username, password } = req.body;
    try {
        const hashedPassword = await bcrypt.hash(password, 10);
        const [result] = await db.execute(
            'INSERT INTO jugador (first_name, last_name, email, phone, username, password) VALUES (?, ?, ?, ?, ?, ?)',
            [first_name, last_name, email, phone, username, hashedPassword]
        );      
        res.status(201).json({ id: result.insertId, message: 'Jugador creado con éxito' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Obtener todos los jugadores
exports.obtenerJugadores = async (req, res) => {
    try {
        const [rows] = await db.execute('SELECT * FROM jugador');
        res.status(200).json(rows);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Obtener un jugador por ID
exports.obtenerJugadorPorId = async (req, res) => {
    const { id } = req.params;

    try {
        const [rows] = await db.execute('SELECT * FROM jugador WHERE id = ?', [id]);
        
        if (rows.length === 0) {
            return res.status(404).json({ message: 'Jugador no encontrado' });
        }

        res.status(200).json(rows[0]);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Actualizar datos de un jugador
exports.actualizarJugador = async (req, res) => {
    const { id } = req.params;
    const { first_name, last_name, email, phone, username, password } = req.body;

    try {
        const hashedPassword = password ? await bcrypt.hash(password, 10) : undefined;

        const [result] = await db.execute(
            'UPDATE jugador SET first_name = ?, last_name = ?, email = ?, phone = ?, username = ?, password = ? WHERE id = ?',
            [first_name, last_name, email, phone, username, hashedPassword || password, id]
        );

        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Jugador no encontrado' });
        }

        res.status(200).json({ message: 'Jugador actualizado con éxito' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Eliminar un jugador
exports.eliminarJugador = async (req, res) => {
    const { id } = req.params;

    try {
        const [result] = await db.execute('DELETE FROM jugador WHERE id = ?', [id]);

        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Jugador no encontrado' });
        }

        res.status(200).json({ message: 'Jugador eliminado con éxito' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Login de un jugador
exports.loginJugador = async (req, res) => {
    const { username, password } = req.body;

    try {
        const [rows] = await db.execute('SELECT * FROM jugador WHERE username = ?', [username]);

        if (rows.length === 0) {
            return res.status(400).json({ message: 'Credenciales incorrectas' });
        }

        const jugador = rows[0];

        const isMatch = await bcrypt.compare(password, jugador.password);

        if (!isMatch) {
            return res.status(400).json({ message: 'Credenciales incorrectas' });
        }

        const token = jwt.sign({ id: jugador.id, username: jugador.username }, 'tu_clave_secreta', {
            expiresIn: '1h'
        });

        res.status(200).json({ token,id:jugador.id,username: jugador.username });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};
