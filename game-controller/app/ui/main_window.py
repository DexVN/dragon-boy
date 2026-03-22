import tkinter as tk
from tkinter import ttk
from app.services.speed_service import (
    set_game_speed, 
    set_player_speed,
    set_auto_farm,
    set_auto_login,
    set_auto_pilgrimage
)

# ===== THEME COLORS (Dracula-inspired) =====
BG = "#282a36"
FG = "#f8f8f2"
ACCENT = "#bd93f9"
BTN = "#44475a"
SUCCESS = "#50fa7b"
ERROR = "#ff5555"


def run_app():
    root = tk.Tk()
    root.title("Game Controller")
    root.geometry("360x600")
    root.configure(bg=BG)
    root.resizable(False, False)

    # ===== STYLE =====
    style = ttk.Style()
    style.theme_use("default")

    style.configure("TLabel", background=BG, foreground=FG, font=("Segoe UI", 10))
    style.configure("Header.TLabel", font=("Segoe UI", 14, "bold"))

    style.configure("TButton",
                    background=BTN,
                    foreground=FG,
                    padding=6)

    # ===== FRAME =====
    container = tk.Frame(root, bg=BG)
    container.pack(fill="both", expand=True, padx=20, pady=20)

    # ===== HEADER =====
    ttk.Label(container, text="Game Controller", style="Header.TLabel").pack(pady=10)

    # ===== GAME SPEED CARD =====
    game_frame = tk.Frame(container, bg=BTN, bd=0)
    game_frame.pack(fill="x", pady=10)

    tk.Label(game_frame, text="Game Speed", bg=BTN, fg=FG).pack(pady=(10, 0))

    entry_game = tk.Entry(game_frame, justify="center", width=1)
    entry_game.pack(pady=5, padx=10, fill="x")

    game_status = tk.Label(game_frame, text="", bg=BTN, fg=SUCCESS)
    game_status.pack(pady=5)

    def handle_game():
        try:
            value = float(entry_game.get())
            set_game_speed(value)
            game_status.config(text=f"✔ Game speed = {value}", fg=SUCCESS)
        except ValueError:
            game_status.config(text="Invalid number!", fg=ERROR)
        except Exception as e:
            game_status.config(text=str(e), fg=ERROR)

    tk.Button(game_frame, text="Apply", bg=ACCENT, fg="black",
              command=handle_game).pack(pady=10, padx=10, fill="x")

    # ===== PLAYER SPEED CARD =====
    player_frame = tk.Frame(container, bg=BTN, bd=0)
    player_frame.pack(fill="x", pady=10)

    tk.Label(player_frame, text="Player Speed", bg=BTN, fg=FG).pack(pady=(10, 0))

    entry_player = tk.Entry(player_frame, justify="center", width=6)
    entry_player.pack(pady=5, padx=10, fill="x")

    player_status = tk.Label(player_frame, text="", bg=BTN, fg=SUCCESS)
    player_status.pack(pady=5)

    def handle_player():
        try:
            value = float(entry_player.get())
            set_player_speed(value)
            player_status.config(text=f"✔ Player speed = {value}", fg=SUCCESS)
        except ValueError:
            player_status.config(text="Invalid number!", fg=ERROR)
        except Exception as e:
            player_status.config(text=str(e), fg=ERROR)

    tk.Button(player_frame, text="Apply", bg=ACCENT, fg="black",
              command=handle_player).pack(pady=10, padx=10, fill="x")

    # ===== AUTO FARM =====
    is_auto_farm = False

    def toggle_auto_farm():
        nonlocal is_auto_farm

        is_auto_farm = not is_auto_farm

        value = 0 if is_auto_farm else 1

        try:
            set_auto_farm(value)
        except Exception as e:
            player_status.config(text=str(e), fg=ERROR)
            

        if is_auto_farm:
            btn_auto_farm.config(text="Auto Farm: ON", bg=SUCCESS, fg="black")
        else:
            btn_auto_farm.config(text="Auto Farm: OFF", bg=ERROR, fg="white")


    btn_auto_farm = tk.Button(
        container,  # 👈 sửa root → container
        text="Auto Farm: OFF",
        bg=ERROR,
        fg="white",
        command=toggle_auto_farm
    )

    btn_auto_farm.pack(pady=10, fill="x")

    # ===== AUTO LOGIN =====
    is_auto_login = False

    def toggle_auto_login():
        nonlocal is_auto_login

        is_auto_login = not is_auto_login

        value = 0 if is_auto_login else 1

        try:
            set_auto_login(value)
        except Exception as e:
            player_status.config(text=str(e), fg=ERROR)

        if is_auto_login:
            btn_auto_login.config(text="Auto Login: ON", bg=SUCCESS, fg="black")
        else:
            btn_auto_login.config(text="Auto Login: OFF", bg=ERROR, fg="white")


    btn_auto_login = tk.Button(
        container,  # 👈 sửa root → container
        text="Auto Login: OFF",
        bg=ERROR,
        fg="white",
        command=toggle_auto_login
    )

    btn_auto_login.pack(pady=10, fill="x")

    # ===== AUTO PILGRIMAGE =====
    is_auto_pilgrimage = False

    def toggle_auto_pilgrimage():
        nonlocal is_auto_pilgrimage

        is_auto_pilgrimage = not is_auto_pilgrimage

        value = 0 if is_auto_pilgrimage else 1

        try:
            set_auto_pilgrimage(value)
        except Exception as e:
            player_status.config(text=str(e), fg=ERROR)

        if is_auto_pilgrimage:
            btn_auto_pilgrimage.config(text="Auto Pilgrimage: ON", bg=SUCCESS, fg="black")
        else:
            btn_auto_pilgrimage.config(text="Auto Pilgrimage: OFF", bg=ERROR, fg="white")


    btn_auto_pilgrimage = tk.Button(
        container,  # 👈 sửa root → container
        text="Auto Pilgrimage: OFF",
        bg=ERROR,
        fg="white",
        command=toggle_auto_pilgrimage
    )

    btn_auto_pilgrimage.pack(pady=10, fill="x")

    root.mainloop()


if __name__ == "__main__":
    run_app()
