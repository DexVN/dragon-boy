import tkinter as tk
from tkinter import ttk
from app.services.speed_service import (
    set_game_speed, 
    set_player_speed,
    set_auto_farm
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
    root.geometry("360x500")
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

        set_auto_farm(value)

        if is_auto_farm:
            btn_auto.config(text="Auto Farm: ON", bg=SUCCESS, fg="black")
        else:
            btn_auto.config(text="Auto Farm: OFF", bg=ERROR, fg="white")


    btn_auto = tk.Button(
        container,  # 👈 sửa root → container
        text="Auto Farm: OFF",
        bg=ERROR,
        fg="white",
        command=toggle_auto_farm
    )

    btn_auto.pack(pady=10, fill="x")

    
    root.mainloop()


if __name__ == "__main__":
    run_app()
