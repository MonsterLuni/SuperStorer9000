function connect(name,password){
    const net = require('net');
    const client = new net.Socket();
    const port = 5555;
    const host = '127.0.0.1';
    client.connect(port, host, () => {
        console.log('Verbunden mit dem Server');
        client.write(name + " " + password);
    });

    client.on('data', (data) => {
        console.log('Empfangene Daten vom Server: ' + data);
    })
    client.on('close', () => {
        console.log('Verbindung geschlossen');
    });

    client.on('error', (err) => {
        console.error('Fehler: ' + err.message);
    });
}

const express = require('express');
const cors = require('cors');
const app = express();
const port = 3000;

app.use(cors());
app.use(express.urlencoded({ extended: true }));

app.post('/action',(req, res) => {
    console.log(req.body);
    connect(req.body['username'],req.body['password']);
});

app.listen(port, () => {
    console.log(`Server listening at http://localhost:${port}`);
});
