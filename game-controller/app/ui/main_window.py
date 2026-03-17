import tkinter as tk
import json
from app.core.client import send_command

def run_app():
    root = tk.Tk()
    root.title("Game Controller")

    # 👇 kích thước form
    root.geometry("320x720")
    root.resizable(False, False)

    # ===== UI =====
    tk.Label(root, text="Game Speed").pack(pady=5)

    entry_game_speed = tk.Entry(root)
    entry_game_speed.pack(pady=5)

    status_label = tk.Label(root, text="", fg="green")
    status_label.pack(pady=5)

    tk.Label(root, text="Player Speed").pack(pady=5)

    entry_player_speed = tk.Entry(root)
    entry_player_speed.pack(pady=5)

    status_label = tk.Label(root, text="", fg="green")
    status_label.pack(pady=5)




    # ===== ACTIONS =====
    def set_game_speed():
        try:
            value = float(entry_game_speed.get())

            data = {
                "action": "game_speed",
                "value": value
            }

            send_command(json.dumps(data))
            status_label.config(text=f"Sent game speed = {value}")

        except ValueError:
            status_label.config(text="Invalid number!", fg="red")

        except Exception as e:
            status_label.config(text=str(e), fg="red")

    def set_player_speed():
        try:
            value = float(entry_player_speed.get())

            data = {
                "action": "player_speed",
                "value": value
            }

            send_command(json.dumps(data))
            status_label.config(text=f"Send player speed = {value}")
        except ValueError:
            status_label.config(text="Invalid number!", fg="red")

        except Exception as e:
            status_label.config(text=str(e), fg="red")
            
    # ===== BUTTON =====
    btn_game_speed = tk.Button(root, text="Game Speed", command=set_game_speed)
    btn_game_speed.pack(pady=10, fill="x", padx=20)

    btn_player_speed = tk.Button(root, text="Player Speed", command=set_player_speed)
    btn_player_speed.pack(pady=10, fill="x", padx=20)

    root.mainloop()