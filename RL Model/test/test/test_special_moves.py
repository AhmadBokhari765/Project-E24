from origins_env import OriginsEnv

def test_fire_spread_mechanic():
    env = OriginsEnv()
    env.board[1][1] = "Neutral"
    env.move_piece((0, 2), (1, 1))  # Fire moves to (1,1)
    assert "Creationist_Fire" in [env.board[1][0], env.board[1][2]], "Fire should spread to adjacent neutrals"
    print("✓ Fire spread mechanic test passed - Flames expand correctly")

def test_water_flow_blocking():
    env = OriginsEnv()
    env.board[1][1] = "Evolutionist_Water"
    env.board[2][2] = "Creationist_Earth"
    moves = env.get_valid_moves(1, 1)
    assert (2, 2) not in moves, "Water shouldn't flow against Earth"
    print("✓ Water flow blocking test passed - Earth stops Water")

def test_air_jump_over_obstacles():
    env = OriginsEnv()
    env.board[1][1] = "Creationist_Air"
    env.board[2][2] = "Evolutionist_Fire"
    moves = env.get_valid_moves(1, 1)
    assert (3, 3) in moves, "Air should jump over Fire"
    print("✓ Air jump mechanic test passed - Floats over obstacles")

if __name__ == "__main__":
    test_fire_spread_mechanic()
    test_water_flow_blocking()
    test_air_jump_over_obstacles()
    print("✅ All special movement tests passed!")