from origins_env import OriginsEnv

def test_creationist_destination_timing():
    env = OriginsEnv()
    env.move_piece((0, 5), (6, 5))  # Creationist man moves to row 6
    assert env.creationist_male_arrived, "Creationist man should arrive at row 6"
    print("✓ Creationist destination timing test passed - 6 'days' confirmed")

def test_evolutionist_destination_timing():
    env = OriginsEnv()
    env.turn = "Evolutionist"
    env.move_piece((7, 5), (1, 5))  # Evolutionist man moves to row 1
    assert env.evolutionist_male_arrived, "Evolutionist man should arrive at row 1"
    print("✓ Evolutionist destination timing test passed - 6M 'years' confirmed")

def test_stalemate_after_max_turns():
    env = OriginsEnv()
    for _ in range(100):  # Simulate 100 turns
        env.turn = "Creationist" if env.turn == "Evolutionist" else "Evolutionist"
    assert env.check_game_over(), "Game should end in stalemate after max turns"
    print("✓ Turn limit stalemate test passed")

if __name__ == "__main__":
    test_creationist_destination_timing()
    test_evolutionist_destination_timing()
    test_stalemate_after_max_turns()
    print("✅ All time mechanics tests passed!")