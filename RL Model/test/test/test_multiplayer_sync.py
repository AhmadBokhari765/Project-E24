from origins_env import OriginsEnv

def test_turn_switching():
    env = OriginsEnv()
    original_turn = env.turn
    env.move_piece((0,0), (1,0))
    assert env.turn != original_turn, "Turn should switch after move"
    print("✓ Turn switching test passed")

def test_consecutive_passes():
    env = OriginsEnv()
    env.board = [["Neutral"]*10 for _ in range(8)]  # Empty board
    env.turn = "Creationist"
    assert env.check_game_over(), "Game should end when no valid moves"
    print("✓ Consecutive pass stalemate test passed")

if __name__ == "__main__":
    test_turn_switching()
    test_consecutive_passes()
    print("✅ All multiplayer sync tests passed!")