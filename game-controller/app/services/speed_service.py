import json
from app.core.client import send_command

def set_game_speed(value: float):
    data = {
        "action": "game_speed",
        "value": value
    }
    send_command(json.dumps(data))


def set_player_speed(value: float):
    data = {
        "action": "player_speed",
        "value": value
    }
    send_command(json.dumps(data))

def set_auto_farm(value: float):
    data = {
        "action": "auto_farm",
        "value": value
    }
    send_command(json.dumps(data))