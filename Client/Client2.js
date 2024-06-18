const net = require('net');

const client = new net.Socket();
const port = 5555;
const host = '127.0.0.1';

client.connect(port, host, () => {
    console.log('Verbunden mit dem Server');
    client.write('Schmuerzuu HASHED_PASSWORD');
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
