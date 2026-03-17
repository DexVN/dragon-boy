id="core1"
import socket

HOST = "127.0.0.1"
PORT = 12345

def send_command(cmd: str):
    try:
        s = socket.socket()
        s.connect((HOST, PORT))
        s.send(cmd.encode())
        s.close()
    except Exception as e:
        print("Error:", e)