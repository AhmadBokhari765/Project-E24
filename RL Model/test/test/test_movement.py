from origins_env import OriginsEnv

def test_creationist_earth_moves():
    env = OriginsEnv()
    moves = env.get_valid_moves(0, 0)
    assert (1, 0) in moves, "Earth should move downward"
    assert (0, 1) in moves, "Earth should move right"
    print("✓ Creationist Earth movement test passed")

def test_evolutionist_man_moves():
    env = OriginsEnv()
    env.turn = "Evolutionist"
    moves = env.get_valid_moves(7, 5)
    assert (6, 5) in moves, "Evolutionist man should move up"
    assert (7, 6) in moves, "Evolutionist man should move right"
    print("✓ Evolutionist man movement test passed")

def test_blocked_movement():
    env = OriginsEnv()
    env.board[1][0] = "Creationist_Water"
    assert (1, 0) not in env.get_valid_moves(0, 0), "Earth shouldn't move through same-team Water"
    print("✓ Blocked movement test passed")

if __name__ == "__main__":
    test_creationist_earth_moves()
    test_evolutionist_man_moves()
    test_blocked_movement()
    print("✅ All movement tests passed!")